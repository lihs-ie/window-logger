using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WindowLogger.Application.Workflows;
using WindowLogger.Infrastructure.Adapters;
using WindowLogger.Domain.Entities;

namespace WindowLogger.Presentation.Services;

public sealed class EventDrivenWindowActivityService : BackgroundService, IDisposable
{
    private readonly WindowActivityWorkflow _workflow;
    private readonly ILogger<EventDrivenWindowActivityService> _logger;
    private EventDrivenWindowActivityRecorder? _eventRecorder;
    private readonly Timer _heartbeatTimer;
    private bool _disposed;

    private const int HEARTBEAT_INTERVAL_MILLISECONDS = 5000; // 5秒間隔のハートビート

    public EventDrivenWindowActivityService(
        WindowActivityWorkflow workflow,
        ILogger<EventDrivenWindowActivityService> logger)
    {
        _workflow = workflow;
        _logger = logger;
        
        // ハートビートタイマー（システムが正常動作していることを確認）
        _heartbeatTimer = new Timer(OnHeartbeat, null, 
            HEARTBEAT_INTERVAL_MILLISECONDS, HEARTBEAT_INTERVAL_MILLISECONDS);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Event-driven window activity service started");

        try
        {
            // イベント駆動レコーダーを初期化
            _eventRecorder = new EventDrivenWindowActivityRecorder(
                _logger,
                OnWindowActivityDetected);

            // 初期状態を記録
            RecordCurrentActivity();

            // サービスが停止されるまで待機
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Event-driven window activity service stopped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Event-driven window activity service encountered an error");
        }
        finally
        {
            _eventRecorder?.Dispose();
        }
    }

    private void OnWindowActivityDetected(WindowActivityRecord record)
    {
        try
        {
            // ワークフローを通じて記録を保存
            _workflow.RecordActivity(record);
            
            _logger.LogDebug("Event-driven activity recorded: {Title}", 
                record.WindowTitle.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save event-driven activity record");
        }
    }

    private void OnHeartbeat(object? state)
    {
        try
        {
            // 定期的なハートビート記録（システム動作確認用）
            RecordCurrentActivity();
            
            _logger.LogTrace("Heartbeat: System monitoring active");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Heartbeat failed");
        }
    }

    private void RecordCurrentActivity()
    {
        try
        {
            if (_eventRecorder != null)
            {
                var record = _eventRecorder.RecordCurrentWindowActivity();
                _workflow.RecordActivity(record);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to record current activity");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event-driven window activity service is stopping");
        
        await _heartbeatTimer.DisposeAsync();
        _eventRecorder?.Dispose();
        
        await base.StopAsync(cancellationToken);
    }

    protected override void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _heartbeatTimer?.Dispose();
            _eventRecorder?.Dispose();
            _disposed = true;
        }
        
        base.Dispose(disposing);
    }
}