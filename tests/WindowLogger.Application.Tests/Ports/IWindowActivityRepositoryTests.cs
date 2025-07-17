using FluentAssertions;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Ports;

public sealed class IWindowActivityRepositoryTests
{
    [Fact]
    public void SaveWindowActivityLog_ShouldNotThrow()
    {
        // Arrange
        var repository = new TestWindowActivityRepository();
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        
        // Act & Assert
        var action = () => repository.SaveWindowActivityLog(log);
        action.Should().NotThrow();
    }

    [Fact]
    public void LoadWindowActivityLog_ShouldReturnWindowActivityLog()
    {
        // Arrange
        var repository = new TestWindowActivityRepository();
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        repository.SaveWindowActivityLog(log);
        
        // Act
        var result = repository.LoadWindowActivityLog();
        
        // Assert
        result.Should().NotBeNull();
        result.RecordCount.Should().Be(1);
    }

    private static WindowActivityRecord CreateTestRecord()
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Test Window");
        var recordedAt = RecordedAt.Now();
        
        return WindowActivityRecord.Create(id, title, recordedAt);
    }

    private sealed class TestWindowActivityRepository : IWindowActivityRepository
    {
        private WindowActivityLog? _log;

        public void SaveWindowActivityLog(WindowActivityLog log)
        {
            _log = log;
        }

        public WindowActivityLog LoadWindowActivityLog()
        {
            return _log ?? WindowActivityLog.Create();
        }
    }
}