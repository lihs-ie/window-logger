using Microsoft.Extensions.Logging;
using WindowLogger.Application.Workflows;
using WindowLogger.Infrastructure.Exporters;
using WindowLogger.Presentation.Constants;

namespace WindowLogger.Presentation.Forms;

public partial class MainForm : Form
{
    private readonly WindowActivityWorkflow _workflow;
    private readonly FileHtmlExporter _htmlExporter;
    private readonly ILogger<MainForm> _logger;
    private readonly System.Windows.Forms.Timer _refreshTimer;

    // UI Components
    private DataGridView _activityGrid = null!;
    private StatusStrip _statusStrip = null!;
    private ToolStripStatusLabel _recordCountLabel = null!;
    private ToolStripStatusLabel _lastUpdateLabel = null!;
    private MenuStrip _menuStrip = null!;
    private ToolStrip _toolStrip = null!;

    private const string FORM_TITLE = "WindowLogger - アクティビティ監視";
    private const int REFRESH_INTERVAL_MS = 2000; // 2秒間隔でUI更新

    public MainForm(
        WindowActivityWorkflow workflow,
        FileHtmlExporter htmlExporter,
        ILogger<MainForm> logger)
    {
        _workflow = workflow;
        _htmlExporter = htmlExporter;
        _logger = logger;

        _refreshTimer = new System.Windows.Forms.Timer();
        
        InitializeComponent();
        SetupRefreshTimer();
        
        _logger.LogInformation("Main form initialized");
    }

    private void InitializeComponent()
    {
        // フォーム基本設定
        Text = FORM_TITLE;
        Size = new Size(1000, 600);
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(800, 400);
        
        // アイコン設定（実際のアプリケーションではアイコンファイルを使用）
        ShowIcon = true;
        
        // ウィンドウを閉じてもタスクトレイに残す
        FormClosing += OnFormClosing;

        CreateMenuStrip();
        CreateToolStrip();
        CreateDataGridView();
        CreateStatusStrip();
        
        // レイアウト設定
        Controls.Add(_activityGrid);
        Controls.Add(_toolStrip);
        Controls.Add(_menuStrip);
        Controls.Add(_statusStrip);
        
        MainMenuStrip = _menuStrip;
    }

    private void CreateMenuStrip()
    {
        _menuStrip = new MenuStrip();

        // ファイルメニュー
        var fileMenu = new ToolStripMenuItem("ファイル(&F)");
        
        var exportMenuItem = new ToolStripMenuItem("HTMLエクスポート(&E)");
        exportMenuItem.Click += OnExportClicked;
        exportMenuItem.ShortcutKeys = Keys.Control | Keys.E;
        fileMenu.DropDownItems.Add(exportMenuItem);
        
        fileMenu.DropDownItems.Add(new ToolStripSeparator());
        
        var hideMenuItem = new ToolStripMenuItem("タスクトレイに最小化(&H)");
        hideMenuItem.Click += OnHideClicked;
        hideMenuItem.ShortcutKeys = Keys.Control | Keys.H;
        fileMenu.DropDownItems.Add(hideMenuItem);
        
        var exitMenuItem = new ToolStripMenuItem("終了(&X)");
        exitMenuItem.Click += OnExitClicked;
        exitMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
        fileMenu.DropDownItems.Add(exitMenuItem);

        // 表示メニュー
        var viewMenu = new ToolStripMenuItem("表示(&V)");
        
        var refreshMenuItem = new ToolStripMenuItem("更新(&R)");
        refreshMenuItem.Click += OnRefreshClicked;
        refreshMenuItem.ShortcutKeys = Keys.F5;
        viewMenu.DropDownItems.Add(refreshMenuItem);
        
        var clearMenuItem = new ToolStripMenuItem("クリア(&C)");
        clearMenuItem.Click += OnClearClicked;
        clearMenuItem.ShortcutKeys = Keys.Control | Keys.Delete;
        viewMenu.DropDownItems.Add(clearMenuItem);

        _menuStrip.Items.Add(fileMenu);
        _menuStrip.Items.Add(viewMenu);
    }

    private void CreateToolStrip()
    {
        _toolStrip = new ToolStrip { Dock = DockStyle.Top };

        var exportButton = new ToolStripButton("HTMLエクスポート");
        exportButton.Click += OnExportClicked;
        exportButton.ToolTipText = "アクティビティログをHTMLファイルにエクスポートします";
        _toolStrip.Items.Add(exportButton);

        _toolStrip.Items.Add(new ToolStripSeparator());

        var refreshButton = new ToolStripButton("更新");
        refreshButton.Click += OnRefreshClicked;
        refreshButton.ToolTipText = "ログを最新の状態に更新します";
        _toolStrip.Items.Add(refreshButton);

        var clearButton = new ToolStripButton("クリア");
        clearButton.Click += OnClearClicked;
        clearButton.ToolTipText = "すべてのアクティビティログを削除します（復元不可）";
        _toolStrip.Items.Add(clearButton);
    }

    private void CreateDataGridView()
    {
        _activityGrid = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };

        // 列の設定
        var timeColumn = new DataGridViewTextBoxColumn
        {
            Name = "Time",
            HeaderText = "記録日時",
            DataPropertyName = "RecordedTime",
            FillWeight = 20
        };

        var appColumn = new DataGridViewTextBoxColumn
        {
            Name = "Application",
            HeaderText = "アプリケーション",
            DataPropertyName = "ApplicationName",
            FillWeight = 25
        };

        var titleColumn = new DataGridViewTextBoxColumn
        {
            Name = "WindowTitle",
            HeaderText = "ウィンドウタイトル",
            DataPropertyName = "WindowTitle",
            FillWeight = 55
        };

        _activityGrid.Columns.AddRange(timeColumn, appColumn, titleColumn);
    }

    private void CreateStatusStrip()
    {
        _statusStrip = new StatusStrip();

        _recordCountLabel = new ToolStripStatusLabel("記録数: 0 件") { Spring = false };

        _lastUpdateLabel = new ToolStripStatusLabel("最終更新: 未更新")
        {
            Spring = true,
            TextAlign = ContentAlignment.MiddleRight
        };

        _statusStrip.Items.Add(_recordCountLabel);
        _statusStrip.Items.Add(_lastUpdateLabel);
    }

    private void SetupRefreshTimer()
    {
        _refreshTimer.Interval = REFRESH_INTERVAL_MS;
        _refreshTimer.Tick += OnRefreshTimer;
        _refreshTimer.Start();
    }

    private void OnRefreshTimer(object? _, EventArgs __)
    {
        RefreshActivityGrid();
    }

    private void RefreshActivityGrid()
    {
        try
        {
            var log = _workflow.GetAllActivities();
            var activities = log.Records
                .OrderByDescending(r => r.RecordedAt.Value)
                .Take(1000) // 最新1000件のみ表示
                .Select(r => new
                {
                    RecordedTime = r.RecordedAt.Value.ToString("yyyy/MM/dd HH:mm:ss"),
                    ApplicationName = r.ApplicationName,
                    WindowTitle = r.WindowTitle.Value
                })
                .ToList();

            _activityGrid.DataSource = activities;
            
            _recordCountLabel.Text = $"記録数: {log.RecordCount} 件";
            _lastUpdateLabel.Text = $"最終更新: {DateTime.Now:HH:mm:ss}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh activity grid");
        }
    }

    private void OnFormClosing(object? sender, FormClosingEventArgs e)
    {
        // ウィンドウを閉じる代わりに非表示にする
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            Hide();
            _logger.LogDebug("Main form hidden to system tray");
        }
    }

    private void OnExportClicked(object? sender, EventArgs e)
    {
        try
        {
            var log = _workflow.GetAllActivities();
            if (log.RecordCount == 0)
            {
                MessageBox.Show(
                    UserMessages.EXPORT_NO_RECORDS_MESSAGE, 
                    UserMessages.EXPORT_NO_RECORDS_TITLE, 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }

            var filePath = _htmlExporter.ExportToHtmlFile(log);
            
            var result = MessageBox.Show(
                string.Format(UserMessages.EXPORT_SUCCESS_MESSAGE_FORMAT, filePath), 
                UserMessages.EXPORT_SUCCESS_TITLE, 
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
            MessageBox.Show(
                UserMessages.EXPORT_ERROR_MESSAGE, 
                UserMessages.EXPORT_ERROR_TITLE, 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error);
        }
    }

    private void OnRefreshClicked(object? _, EventArgs __)
    {
        RefreshActivityGrid();
    }

    private void OnClearClicked(object? _, EventArgs __)
    {
        var result = MessageBox.Show(
            UserMessages.CLEAR_CONFIRMATION_MESSAGE,
            UserMessages.CLEAR_CONFIRMATION_TITLE,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            try
            {
                _workflow.ClearAllActivities();
                RefreshActivityGrid();
                _logger.LogInformation("All activity logs cleared by user");
                
                MessageBox.Show(
                    UserMessages.CLEAR_SUCCESS_MESSAGE,
                    UserMessages.CLEAR_SUCCESS_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to clear activity logs");
                MessageBox.Show(
                    UserMessages.CLEAR_ERROR_MESSAGE,
                    UserMessages.CLEAR_ERROR_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void OnHideClicked(object? _, EventArgs __)
    {
        Hide();
    }

    private void OnExitClicked(object? _, EventArgs __)
    {
        _logger.LogInformation("User requested application exit from main form");
        System.Windows.Forms.Application.Exit();
    }

    public new void Show()
    {
        base.Show();
        RefreshActivityGrid();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
        }
        base.Dispose(disposing);
    }
}