using System.Runtime.Versioning;
using System.Text;
using Microsoft.Extensions.Logging;
using Windows.Win32;
using Windows.Win32.Foundation;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Infrastructure.Adapters;

[SupportedOSPlatform("windows5.0")]
public sealed class WindowsApiWindowActivityRecorder : IWindowActivityRecorder
{
    private readonly ILogger<WindowsApiWindowActivityRecorder> _logger;
    private const string DEFAULT_WINDOW_TITLE = "デスクトップ";
    private const int WINDOW_TITLE_BUFFER_SIZE = 256;

    public WindowsApiWindowActivityRecorder(ILogger<WindowsApiWindowActivityRecorder>? logger = null)
    {
        _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<WindowsApiWindowActivityRecorder>.Instance;
    }

    public WindowActivityRecord RecordCurrentWindowActivity()
    {
        try
        {
            var windowHandle = PInvoke.GetForegroundWindow();
            var windowTitle = GetWindowTitle(windowHandle);
            
            var id = WindowActivityRecordIdentifier.NewId();
            var title = WindowTitle.Create(windowTitle);
            var recordedAt = RecordedAt.Now();
            
            _logger.LogDebug("Recorded window activity: {Title}", windowTitle);
            
            return WindowActivityRecord.Create(id, title, recordedAt);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to record window activity");
            
            // エラー時はデフォルトのレコードを返す
            var id = WindowActivityRecordIdentifier.NewId();
            var title = WindowTitle.Create(DEFAULT_WINDOW_TITLE);
            var recordedAt = RecordedAt.Now();
            
            return WindowActivityRecord.Create(id, title, recordedAt);
        }
    }

    private unsafe string GetWindowTitle(HWND windowHandle)
    {
        if (windowHandle.IsNull)
        {
            return DEFAULT_WINDOW_TITLE;
        }

        // Allocate buffer for window title
        var buffer = stackalloc char[WINDOW_TITLE_BUFFER_SIZE];
        var length = PInvoke.GetWindowText(windowHandle, buffer, WINDOW_TITLE_BUFFER_SIZE);
        
        if (length == 0)
        {
            return DEFAULT_WINDOW_TITLE;
        }

        var windowTitle = new string(buffer, 0, length);
        
        // 空文字列の場合はデフォルトタイトルを返す
        return string.IsNullOrWhiteSpace(windowTitle) ? DEFAULT_WINDOW_TITLE : windowTitle;
    }
}