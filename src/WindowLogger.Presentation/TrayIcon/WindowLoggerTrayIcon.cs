using System.ComponentModel;
using Microsoft.Extensions.Logging;
using WindowLogger.Application.Workflows;
using WindowLogger.Infrastructure.Exporters;
using WindowLogger.Presentation.Forms;

namespace WindowLogger.Presentation.TrayIcon;

public sealed class WindowLoggerTrayIcon : IDisposable
{
    private readonly NotifyIcon _notifyIcon;
    private readonly WindowActivityWorkflow _workflow;
    private readonly FileHtmlExporter _htmlExporter;
    private readonly MainForm _mainForm;
    private readonly ILogger<WindowLoggerTrayIcon> _logger;
    private bool _disposed;

    private const string TRAY_ICON_TEXT = "WindowLogger - アクティビティ記録中";
    private const string CONTEXT_MENU_SHOW_MAIN = "メインウィンドウ表示(&M)";
    private const string CONTEXT_MENU_VIEW_LOG = "ログを表示(&V)";
    private const string CONTEXT_MENU_EXPORT_HTML = "HTML出力(&E)";
    private const string CONTEXT_MENU_EXIT = "終了(&X)";

    public WindowLoggerTrayIcon(
        WindowActivityWorkflow workflow,
        FileHtmlExporter htmlExporter,
        MainForm mainForm,
        ILogger<WindowLoggerTrayIcon> logger)
    {
        _workflow = workflow;
        _htmlExporter = htmlExporter;
        _mainForm = mainForm;
        _logger = logger;

        _notifyIcon = new NotifyIcon();
        InitializeNotifyIcon();

        _logger.LogInformation("Window Logger tray icon initialized");
    }

    private void InitializeNotifyIcon()
    {
        // アイコンを設定（実際のアプリケーションではアイコンファイルを使用）
        _notifyIcon.Icon = SystemIcons.Application;
        _notifyIcon.Text = TRAY_ICON_TEXT;
        _notifyIcon.Visible = true;

        // コンテキストメニューを作成
        var contextMenu = new ContextMenuStrip();
        
        var showMainMenuItem = new ToolStripMenuItem(CONTEXT_MENU_SHOW_MAIN);
        showMainMenuItem.Click += OnShowMainClicked;
        contextMenu.Items.Add(showMainMenuItem);

        contextMenu.Items.Add(new ToolStripSeparator());

        var viewLogMenuItem = new ToolStripMenuItem(CONTEXT_MENU_VIEW_LOG);
        viewLogMenuItem.Click += OnViewLogClicked;
        contextMenu.Items.Add(viewLogMenuItem);

        var exportHtmlMenuItem = new ToolStripMenuItem(CONTEXT_MENU_EXPORT_HTML);
        exportHtmlMenuItem.Click += OnExportHtmlClicked;
        contextMenu.Items.Add(exportHtmlMenuItem);

        contextMenu.Items.Add(new ToolStripSeparator());

        var exitMenuItem = new ToolStripMenuItem(CONTEXT_MENU_EXIT);
        exitMenuItem.Click += OnExitClicked;
        contextMenu.Items.Add(exitMenuItem);

        _notifyIcon.ContextMenuStrip = contextMenu;

        // ダブルクリックでメインウィンドウ表示
        _notifyIcon.DoubleClick += OnShowMainClicked;
    }

    private void OnShowMainClicked(object? sender, EventArgs e)
    {
        try
        {
            _mainForm.Show();
            _mainForm.WindowState = FormWindowState.Normal;
            _mainForm.BringToFront();
            _mainForm.Activate();
            
            _logger.LogDebug("Main window displayed from tray icon");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to show main window");
            MessageBox.Show("メインウィンドウの表示中にエラーが発生しました。", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnViewLogClicked(object? sender, EventArgs e)
    {
        try
        {
            var log = _workflow.GetAllActivities();
            var recordCount = log.RecordCount;

            var message = recordCount > 0
                ? $"記録されたアクティビティ: {recordCount} 件\n\n最新の記録:\n{GetLatestRecordsText(log)}"
                : "まだアクティビティが記録されていません。";

            MessageBox.Show(message, "WindowLogger - アクティビティログ", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            _logger.LogDebug("Displayed activity log with {RecordCount} records", recordCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to display activity log");
            MessageBox.Show("ログの表示中にエラーが発生しました。", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnExportHtmlClicked(object? sender, EventArgs e)
    {
        try
        {
            var log = _workflow.GetAllActivities();
            if (log.RecordCount == 0)
            {
                MessageBox.Show("エクスポートする記録がありません。", "WindowLogger", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var filePath = _htmlExporter.ExportToHtmlFile(log);
            
            var result = MessageBox.Show(
                $"HTMLファイルを出力しました:\n{filePath}\n\nファイルを開きますか？", 
                "エクスポート完了", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }

            _logger.LogInformation("Exported activity log to HTML file: {FilePath}", filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to export activity log to HTML");
            MessageBox.Show("HTMLエクスポート中にエラーが発生しました。", "エラー", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OnExitClicked(object? sender, EventArgs e)
    {
        _logger.LogInformation("User requested application exit");
        System.Windows.Forms.Application.Exit();
    }

    private static string GetLatestRecordsText(WindowLogger.Domain.Aggregates.WindowActivityLog log)
    {
        const int MAX_DISPLAY_RECORDS = 5;
        
        var latestRecords = log.Records
            .OrderByDescending(r => r.RecordedAt.Value)
            .Take(MAX_DISPLAY_RECORDS)
            .Select(r => $"• {r.WindowTitle.Value} ({r.ApplicationName}) - {r.RecordedAt.Value:HH:mm:ss}")
            .ToArray();

        var text = string.Join("\n", latestRecords);
        
        if (log.RecordCount > MAX_DISPLAY_RECORDS)
        {
            text += $"\n\n... 他 {log.RecordCount - MAX_DISPLAY_RECORDS} 件";
        }

        return text;
    }

    public void Dispose()
    {
        if (_disposed) return;

        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        _disposed = true;

        _logger.LogInformation("Window Logger tray icon disposed");
    }
}