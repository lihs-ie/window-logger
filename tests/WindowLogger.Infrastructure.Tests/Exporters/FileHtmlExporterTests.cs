using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using WindowLogger.Infrastructure.Exporters;
using Xunit;

namespace WindowLogger.Infrastructure.Tests.Exporters;

public sealed class FileHtmlExporterTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly FileHtmlExporter _exporter;

    public FileHtmlExporterTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_testDirectory);
        
        var logger = new Mock<ILogger<FileHtmlExporter>>();
        _exporter = new FileHtmlExporter(_testDirectory, logger.Object);
    }

    [Fact]
    public void ExportToHtml_ShouldCreateHtmlFile()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        
        // Act
        var htmlContent = _exporter.ExportToHtml(log);
        
        // Assert
        htmlContent.Should().NotBeNullOrEmpty();
        htmlContent.Should().Contain("<!DOCTYPE html>");
        htmlContent.Should().Contain("Test Window");
    }

    [Fact]
    public void ExportToHtmlFile_ShouldSaveHtmlFileToDirectory()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        
        // Act
        var filePath = _exporter.ExportToHtmlFile(log);
        
        // Assert
        File.Exists(filePath).Should().BeTrue();
        filePath.Should().EndWith(".html");
        
        var fileContent = File.ReadAllText(filePath);
        fileContent.Should().Contain("<!DOCTYPE html>");
        fileContent.Should().Contain("Test Window");
    }

    [Fact]
    public void ExportToHtmlFile_ShouldGenerateUniqueFileNames()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        
        // Act
        var filePath1 = _exporter.ExportToHtmlFile(log);
        var filePath2 = _exporter.ExportToHtmlFile(log);
        
        // Assert
        filePath1.Should().NotBe(filePath2);
        File.Exists(filePath1).Should().BeTrue();
        File.Exists(filePath2).Should().BeTrue();
    }

    private static WindowActivityRecord CreateTestRecord(string title = "Test Window")
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var windowTitle = WindowTitle.Create(title);
        var recordedAt = RecordedAt.Now();
        
        return WindowActivityRecord.Create(id, windowTitle, recordedAt);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, recursive: true);
        }
    }
}