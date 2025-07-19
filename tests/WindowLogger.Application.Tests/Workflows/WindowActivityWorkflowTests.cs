using FluentAssertions;
using WindowLogger.Application.Ports;
using WindowLogger.Application.Workflows;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Workflows;

public sealed class WindowActivityWorkflowTests
{
    [Fact]
    public void RecordCurrentActivity_ShouldRecordAndSaveActivity()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        var repository = new TestWindowActivityRepository();
        var workflow = new WindowActivityWorkflow(recorder, repository);
        
        // Act
        workflow.RecordCurrentActivity();
        
        // Assert
        var log = repository.LoadWindowActivityLog();
        log.RecordCount.Should().Be(1);
        log.Records.First().WindowTitle.Value.Should().Be("Test Window");
    }

    [Fact]
    public void GetAllActivities_ShouldReturnAllRecords()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        var repository = new TestWindowActivityRepository();
        var workflow = new WindowActivityWorkflow(recorder, repository);
        
        // Act
        workflow.RecordCurrentActivity();
        workflow.RecordCurrentActivity();
        var result = workflow.GetAllActivities();
        
        // Assert
        result.RecordCount.Should().Be(2);
    }

    [Fact]
    public void GetActivitiesByTimeRange_ShouldReturnFilteredRecords()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        var repository = new TestWindowActivityRepository();
        var workflow = new WindowActivityWorkflow(recorder, repository);
        
        var baseTime = DateTime.UtcNow.AddHours(-1);
        
        // Act
        workflow.RecordCurrentActivity();
        var result = workflow.GetActivitiesByTimeRange(baseTime, DateTime.UtcNow);
        
        // Assert
        result.Should().HaveCount(1);
    }

    [Fact]
    public void ExportToHtml_ShouldGenerateHtmlContent()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        var repository = new TestWindowActivityRepository();
        var exporter = new TestHtmlExporter();
        var workflow = new WindowActivityWorkflow(recorder, repository, exporter);
        
        // Act
        workflow.RecordCurrentActivity();
        var result = workflow.ExportToHtml();
        
        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain("<!DOCTYPE html>");
        result.Should().Contain("Test Window");
    }

    [Fact]
    public void ClearAllActivities_ShouldRemoveAllRecords()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        var repository = new TestWindowActivityRepository();
        var workflow = new WindowActivityWorkflow(recorder, repository);
        
        // 複数の記録を追加
        workflow.RecordCurrentActivity();
        workflow.RecordCurrentActivity();
        workflow.RecordCurrentActivity();
        
        var logBefore = workflow.GetAllActivities();
        logBefore.RecordCount.Should().Be(3);
        
        // Act
        workflow.ClearAllActivities();
        
        // Assert
        var logAfter = workflow.GetAllActivities();
        logAfter.RecordCount.Should().Be(0);
        logAfter.Records.Should().BeEmpty();
    }

    private sealed class TestWindowActivityRecorder : IWindowActivityRecorder
    {
        public WindowActivityRecord RecordCurrentWindowActivity()
        {
            var id = WindowActivityRecordIdentifier.NewId();
            var title = WindowTitle.Create("Test Window");
            var recordedAt = RecordedAt.Now();
            
            return WindowActivityRecord.Create(id, title, recordedAt);
        }
    }

    private sealed class TestWindowActivityRepository : IWindowActivityRepository
    {
        private WindowActivityLog _log = WindowActivityLog.Create();

        public void SaveWindowActivityLog(WindowActivityLog log)
        {
            _log = log;
        }

        public WindowActivityLog LoadWindowActivityLog()
        {
            return _log;
        }
    }

    private sealed class TestHtmlExporter : IHtmlExporter
    {
        public string ExportToHtml(WindowActivityLog log)
        {
            const string HTML_HEADER = "<!DOCTYPE html><html><head><title>Window Activity Log</title></head><body>";
            const string HTML_FOOTER = "</body></html>";
            
            var content = HTML_HEADER;
            
            foreach (var record in log.Records)
            {
                content += $"<div>{record.WindowTitle.Value} - {record.RecordedAt.ToIso8601String()}</div>";
            }
            
            content += HTML_FOOTER;
            
            return content;
        }
    }
}