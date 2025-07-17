using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WindowLogger.Application.Workflows;

namespace WindowLogger.Presentation.Services;

public sealed class WindowActivityBackgroundService : BackgroundService
{
    private readonly WindowActivityWorkflow _workflow;
    private readonly ILogger<WindowActivityBackgroundService> _logger;
    
    private const int POLLING_INTERVAL_MILLISECONDS = 1000; // 1秒間隔

    public WindowActivityBackgroundService(
        WindowActivityWorkflow workflow,
        ILogger<WindowActivityBackgroundService> logger)
    {
        _workflow = workflow;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Window activity background service started");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // ウィンドウアクティビティを記録
                    _workflow.RecordCurrentActivity();
                    
                    _logger.LogDebug("Recorded window activity");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to record window activity");
                }

                // 指定された間隔で待機
                await Task.Delay(POLLING_INTERVAL_MILLISECONDS, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Window activity background service stopped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Window activity background service encountered an error");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Window activity background service is stopping");
        await base.StopAsync(cancellationToken);
    }
}