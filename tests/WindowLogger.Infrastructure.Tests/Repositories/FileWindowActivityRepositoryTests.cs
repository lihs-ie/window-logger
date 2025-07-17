using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using WindowLogger.Infrastructure.Repositories;
using Xunit;

namespace WindowLogger.Infrastructure.Tests.Repositories;

public sealed class FileWindowActivityRepositoryTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly FileWindowActivityRepository _repository;

    public FileWindowActivityRepositoryTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_testDirectory);
        
        var logger = new Mock<ILogger<FileWindowActivityRepository>>();
        _repository = new FileWindowActivityRepository(_testDirectory, logger.Object);
    }

    [Fact]
    public void SaveWindowActivityLog_ShouldCreateJsonFile()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        
        // Act
        _repository.SaveWindowActivityLog(log);
        
        // Assert
        var files = Directory.GetFiles(_testDirectory, "*.json");
        files.Should().HaveCount(1);
        
        var fileContent = File.ReadAllText(files[0]);
        fileContent.Should().Contain("Test Window");
    }

    [Fact]
    public void LoadWindowActivityLog_ShouldRestoreFromJsonFile()
    {
        // Arrange
        var originalLog = WindowActivityLog.Create();
        var record = CreateTestRecord();
        originalLog.AddRecord(record);
        _repository.SaveWindowActivityLog(originalLog);
        
        // Act
        var loadedLog = _repository.LoadWindowActivityLog();
        
        // Assert
        loadedLog.RecordCount.Should().Be(1);
        loadedLog.Records.First().WindowTitle.Value.Should().Be("Test Window");
    }

    [Fact]
    public void LoadWindowActivityLog_WhenNoFileExists_ShouldReturnEmptyLog()
    {
        // Arrange - no file exists
        
        // Act
        var result = _repository.LoadWindowActivityLog();
        
        // Assert
        result.Should().NotBeNull();
        result.RecordCount.Should().Be(0);
    }

    [Fact]
    public void SaveWindowActivityLog_ShouldOverwriteExistingFile()
    {
        // Arrange
        var firstLog = WindowActivityLog.Create();
        var firstRecord = CreateTestRecord("First Window");
        firstLog.AddRecord(firstRecord);
        _repository.SaveWindowActivityLog(firstLog);
        
        var secondLog = WindowActivityLog.Create();
        var secondRecord = CreateTestRecord("Second Window");
        secondLog.AddRecord(secondRecord);
        
        // Act
        _repository.SaveWindowActivityLog(secondLog);
        
        // Assert
        var loadedLog = _repository.LoadWindowActivityLog();
        loadedLog.RecordCount.Should().Be(1);
        loadedLog.Records.First().WindowTitle.Value.Should().Be("Second Window");
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