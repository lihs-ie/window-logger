using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Windows.Win32;
using Windows.Win32.Foundation;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Infrastructure.Adapters;

[SupportedOSPlatform("windows5.0")]
public sealed class EventDrivenWindowActivityRecorder : IWindowActivityRecorder, IDisposable
{
    private readonly ILogger<EventDrivenWindowActivityRecorder> _logger;
    private readonly Action<WindowActivityRecord>? _onActivityRecorded;
    private bool _disposed;
    
    private const string DEFAULT_WINDOW_TITLE = "デスクトップ";
    private const int WINDOW_TITLE_BUFFER_SIZE = 256;

    public EventDrivenWindowActivityRecorder(
        ILogger<EventDrivenWindowActivityRecorder>? logger = null,
        Action<WindowActivityRecord>? onActivityRecorded = null)
    {
        _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<EventDrivenWindowActivityRecorder>.Instance;
        _onActivityRecorded = onActivityRecorded;
        
        _logger.LogInformation("Enhanced window monitoring initialized");
    }

    public WindowActivityRecord RecordCurrentWindowActivity()
    {
        try
        {
            var windowHandle = PInvoke.GetForegroundWindow();
            return CreateActivityRecord(windowHandle, "Enhanced Record");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to record current window activity");
            return CreateDefaultRecord();
        }
    }

    private unsafe WindowActivityRecord CreateActivityRecord(HWND windowHandle, string eventType)
    {
        var windowTitle = GetDetailedWindowTitle(windowHandle, eventType);
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create(windowTitle);
        var recordedAt = RecordedAt.Now();
        
        return WindowActivityRecord.Create(id, title, recordedAt);
    }

    private unsafe string GetDetailedWindowTitle(HWND windowHandle, string eventType)
    {
        if (windowHandle.IsNull)
        {
            return $"{DEFAULT_WINDOW_TITLE} [{eventType}]";
        }

        try
        {
            // ウィンドウタイトルを取得
            var buffer = stackalloc char[WINDOW_TITLE_BUFFER_SIZE];
            var length = PInvoke.GetWindowText(windowHandle, buffer, WINDOW_TITLE_BUFFER_SIZE);
            var windowTitle = length > 0 ? new string(buffer, 0, length) : "";

            // 詳細情報を含むタイトルを生成（利用可能なAPIのみ使用）
            var detailedTitle = string.IsNullOrWhiteSpace(windowTitle) 
                ? DEFAULT_WINDOW_TITLE
                : windowTitle;
                
            return $"{detailedTitle} [{eventType}]";
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to get detailed window info");
            return $"{DEFAULT_WINDOW_TITLE} [{eventType}]";
        }
    }

    private WindowActivityRecord CreateDefaultRecord()
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create($"{DEFAULT_WINDOW_TITLE} [ERROR]");
        var recordedAt = RecordedAt.Now();
        
        return WindowActivityRecord.Create(id, title, recordedAt);
    }

    public void Dispose()
    {
        if (_disposed) return;

        _logger.LogInformation("Enhanced window monitor disposed");
        _disposed = true;
    }
}