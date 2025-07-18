using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WindowLogger.Application.Workflows;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using WindowLogger.Infrastructure.Exporters;
using WindowLogger.Presentation.Forms;
using Xunit;

namespace WindowLogger.Presentation.Tests.Forms;

public sealed class MainFormTests : IDisposable
{
    private readonly Mock<WindowActivityWorkflow> _mockWorkflow;
    private readonly Mock<FileHtmlExporter> _mockHtmlExporter;
    private readonly Mock<ILogger<MainForm>> _mockLogger;
    private MainForm? _mainForm;

    public MainFormTests()
    {
        _mockWorkflow = new Mock<WindowActivityWorkflow>();
        _mockHtmlExporter = new Mock<FileHtmlExporter>();
        _mockLogger = new Mock<ILogger<MainForm>>();
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
        _mainForm.Show(); // これによりRefreshActivityGridが呼ばれる

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
        _mainForm.Show(); // これによりRefreshActivityGridが呼ばれる

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

    private void CreateMainForm()
    {
        _mainForm?.Dispose();
        _mainForm = new MainForm(_mockWorkflow.Object, _mockHtmlExporter.Object, _mockLogger.Object);
    }

    public void Dispose()
    {
        _mainForm?.Dispose();
    }
}