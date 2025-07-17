using FluentAssertions;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Ports;

public sealed class IHtmlExporterTests
{
    [Fact]
    public void ExportToHtml_ShouldReturnHtmlContent()
    {
        // Arrange
        var exporter = new TestHtmlExporter();
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        
        // Act
        var result = exporter.ExportToHtml(log);
        
        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain("<!DOCTYPE html>");
        result.Should().Contain("Test Window");
    }

    [Fact]
    public void ExportToHtml_WithEmptyLog_ShouldReturnValidHtml()
    {
        // Arrange
        var exporter = new TestHtmlExporter();
        var log = WindowActivityLog.Create();
        
        // Act
        var result = exporter.ExportToHtml(log);
        
        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain("<!DOCTYPE html>");
    }

    private static WindowActivityRecord CreateTestRecord()
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Test Window");
        var recordedAt = RecordedAt.Now();
        
        return WindowActivityRecord.Create(id, title, recordedAt);
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