using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WindowLogger.Application.Ports;
using WindowLogger.Application.Workflows;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using WindowLogger.Infrastructure.Exporters;
using WindowLogger.Presentation.Forms;
using WindowLogger.Presentation.Services;
using Xunit;

namespace WindowLogger.Presentation.Tests.Forms;

public sealed class MainFormTests : IDisposable
{
    private readonly Mock<IWindowActivityWorkflow> _mockWorkflow;
    private readonly FileHtmlExporter _htmlExporter;
    private readonly Mock<ILogger<MainForm>> _mockLogger;
    private readonly Mock<IDialogService> _mockDialogService;
    private MainForm? _mainForm;

    public MainFormTests()
    {
        // インターフェースを使用してMockを作成
        _mockWorkflow = new Mock<IWindowActivityWorkflow>();
        _htmlExporter = new FileHtmlExporter(); // 実際のインスタンスを使用（テスト用の一時ディレクトリ）
        _mockLogger = new Mock<ILogger<MainForm>>();
        _mockDialogService = new Mock<IDialogService>();
    }

    [Fact]
    [STAThread]
    public void Constructor_ShouldInitializeFormCorrectly()
    {
        // Arrange & Act
        CreateMainForm();

        // Assert
        _mainForm.Should().NotBeNull();
        _mainForm!.Text.Should().Be("WindowLogger - アクティビティ監視");
        _mainForm.Size.Width.Should().Be(1000);
        _mainForm.Size.Height.Should().Be(600);
        _mainForm.StartPosition.Should().Be(FormStartPosition.CenterScreen);
        _mainForm.MinimumSize.Width.Should().Be(800);
        _mainForm.MinimumSize.Height.Should().Be(400);
    }

    [Fact]
    [STAThread]
    public void Constructor_ShouldCreateUIComponents()
    {
        // Arrange & Act
        CreateMainForm();

        // Assert
        _mainForm.Should().NotBeNull();
        _mainForm!.Controls.Count.Should().BeGreaterThan(0);
        
        // メニューストリップが作成されているかチェック
        var menuStrip = _mainForm.Controls.OfType<MenuStrip>().FirstOrDefault();
        menuStrip.Should().NotBeNull();
        menuStrip!.Items.Count.Should().Be(2); // ファイルメニューと表示メニュー
        
        // ツールストリップが作成されているかチェック
        var toolStrip = _mainForm.Controls.OfType<ToolStrip>().FirstOrDefault();
        toolStrip.Should().NotBeNull();
        
        // DataGridViewが作成されているかチェック
        var dataGridView = _mainForm.Controls.OfType<DataGridView>().FirstOrDefault();
        dataGridView.Should().NotBeNull();
        dataGridView!.ReadOnly.Should().BeTrue();
        dataGridView.AllowUserToAddRows.Should().BeFalse();
        dataGridView.AllowUserToDeleteRows.Should().BeFalse();
        
        // ステータスストリップが作成されているかチェック
        var statusStrip = _mainForm.Controls.OfType<StatusStrip>().FirstOrDefault();
        statusStrip.Should().NotBeNull();
    }

    [Fact]
    [STAThread]
    public void RefreshActivityGrid_WithEmptyLog_ShouldDisplayCorrectly()
    {
        // Arrange
        var emptyLog = WindowActivityLog.Create();
        _mockWorkflow.Setup(w => w.GetAllActivities()).Returns(emptyLog);
        
        CreateMainForm();
        var dataGridView = _mainForm!.Controls.OfType<DataGridView>().First();

        // Act
        _mainForm.RefreshActivityGrid(); // 直接メソッドを呼び出し

        // Assert
        dataGridView.DataSource.Should().NotBeNull();
        var dataSource = dataGridView.DataSource as List<object>;
        dataSource.Should().NotBeNull();
        dataSource!.Should().HaveCount(0);
    }

    [Fact]
    [STAThread]
    public void RefreshActivityGrid_WithActivities_ShouldDisplayData()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record1 = WindowActivityRecord.Create(
            WindowActivityRecordIdentifier.NewId(),
            WindowTitle.Create("Test Window 1"),
            RecordedAt.Create(DateTime.Now.AddMinutes(-2)));
        var record2 = WindowActivityRecord.Create(
            WindowActivityRecordIdentifier.NewId(),
            WindowTitle.Create("Test Window 2"),
            RecordedAt.Create(DateTime.Now.AddMinutes(-1)));
        
        log.AddRecord(record1);
        log.AddRecord(record2);
        
        _mockWorkflow.Setup(w => w.GetAllActivities()).Returns(log);
        
        CreateMainForm();
        var dataGridView = _mainForm!.Controls.OfType<DataGridView>().First();

        // Act
        _mainForm.RefreshActivityGrid(); // 直接メソッドを呼び出し

        // Assert
        dataGridView.DataSource.Should().NotBeNull();
        var dataSource = dataGridView.DataSource as List<object>;
        dataSource.Should().NotBeNull();
        dataSource!.Count.Should().Be(2);
    }

    [Fact]
    [STAThread]
    public void Show_ShouldCallRefreshActivityGrid()
    {
        // Arrange
        var emptyLog = WindowActivityLog.Create();
        _mockWorkflow.Setup(w => w.GetAllActivities()).Returns(emptyLog);
        
        CreateMainForm();

        // Act
        _mainForm!.Show();

        // Assert
        _mockWorkflow.Verify(w => w.GetAllActivities(), Times.AtLeastOnce);
    }

    [Fact]
    [STAThread]
    public void ClearButton_ShouldClearAllActivitiesWhenClicked()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record = WindowActivityRecord.Create(
            WindowActivityRecordIdentifier.NewId(),
            WindowTitle.Create("Test Window"),
            RecordedAt.Now());
        log.AddRecord(record);
        
        _mockWorkflow.Setup(w => w.GetAllActivities()).Returns(log);
        _mockWorkflow.Setup(w => w.ClearAllActivities()).Verifiable();
        _mockDialogService.Setup(d => d.ShowQuestion(It.IsAny<string>(), It.IsAny<string>()))
                         .Returns(UserDialogResult.Yes); // ユーザーが「はい」を選択したことをシミュレート
        
        CreateMainForm();
        var toolStrip = _mainForm!.Controls.OfType<ToolStrip>().First();
        var clearButton = toolStrip.Items.OfType<ToolStripButton>().FirstOrDefault(b => b.Text == "クリア");
        
        clearButton.Should().NotBeNull();

        // Act
        clearButton!.PerformClick();

        // Assert
        _mockWorkflow.Verify(w => w.ClearAllActivities(), Times.Once);
        _mockDialogService.Verify(d => d.ShowQuestion(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _mockDialogService.Verify(d => d.ShowInformation(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    private void CreateMainForm()
    {
        _mainForm?.Dispose();
        _mainForm = new MainForm(_mockWorkflow.Object, _htmlExporter, _mockLogger.Object, _mockDialogService.Object);
    }

    public void Dispose()
    {
        _mainForm?.Dispose();
    }
}