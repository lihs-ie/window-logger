using FluentAssertions;
using WindowLogger.Application.Services;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Services;

public sealed class HtmlExportServiceTests
{
    [Fact]
    public void ExportToHtml_WithEmptyLog_ShouldReturnValidHtml()
    {
        // Arrange
        var service = new HtmlExportService();
        var log = WindowActivityLog.Create();
        
        // Act
        var result = service.ExportToHtml(log);
        
        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain("<!DOCTYPE html>");
        result.Should().Contain("<html lang=\"ja\">");
        result.Should().Contain("<head>");
        result.Should().Contain("<title>Window Activity Log</title>");
        result.Should().Contain("<body>");
        result.Should().Contain("</html>");
    }

    [Fact]
    public void ExportToHtml_WithSingleRecord_ShouldIncludeRecordData()
    {
        // Arrange
        var service = new HtmlExportService();
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord("Visual Studio Code", DateTime.UtcNow);
        log.AddRecord(record);
        
        // Act
        var result = service.ExportToHtml(log);
        
        // Assert
        result.Should().Contain("Visual Studio Code");
        result.Should().Contain("Visual Studio Code"); // Application name should also be displayed
        result.Should().Contain("<tr>");
        result.Should().Contain("<td>");
    }

    [Fact]
    public void ExportToHtml_WithMultipleRecords_ShouldDisplayInTimeOrder()
    {
        // Arrange
        var service = new HtmlExportService();
        var log = WindowActivityLog.Create();
        
        var baseTime = DateTime.UtcNow.AddHours(-2);
        var record1 = CreateTestRecord("Window 1", baseTime);
        var record2 = CreateTestRecord("Window 2", baseTime.AddHours(1));
        var record3 = CreateTestRecord("Window 3", baseTime.AddHours(2));
        
        log.AddRecord(record3); // Add in reverse order
        log.AddRecord(record1);
        log.AddRecord(record2);
        
        // Act
        var result = service.ExportToHtml(log);
        
        // Assert
        result.Should().Contain("Window 1");
        result.Should().Contain("Window 2");
        result.Should().Contain("Window 3");
        
        // Should be sorted by time (oldest first)
        var window1Index = result.IndexOf("Window 1");
        var window2Index = result.IndexOf("Window 2");
        var window3Index = result.IndexOf("Window 3");
        
        window1Index.Should().BeLessThan(window2Index);
        window2Index.Should().BeLessThan(window3Index);
    }

    [Fact]
    public void ExportToHtml_ShouldEscapeHtmlCharacters()
    {
        // Arrange
        var service = new HtmlExportService();
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord("<script>alert('test')</script>", DateTime.UtcNow);
        log.AddRecord(record);
        
        // Act
        var result = service.ExportToHtml(log);
        
        // Assert
        result.Should().Contain("&lt;script&gt;");
        result.Should().Contain("&lt;/script&gt;");
        result.Should().NotContain("<script>alert('test')</script>");
    }

    private static WindowActivityRecord CreateTestRecord(string title, DateTime recordedAt)
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var windowTitle = WindowTitle.Create(title);
        var recordedAtValue = RecordedAt.Create(recordedAt);
        
        return WindowActivityRecord.Create(id, windowTitle, recordedAtValue);
    }
}