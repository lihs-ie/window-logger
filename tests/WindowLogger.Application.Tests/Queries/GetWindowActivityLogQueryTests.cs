using FluentAssertions;
using WindowLogger.Application.Ports;
using WindowLogger.Application.Queries;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Queries;

public sealed class GetWindowActivityLogQueryTests
{
    [Fact]
    public void Execute_ShouldReturnWindowActivityLog()
    {
        // Arrange
        var repository = new TestWindowActivityRepository();
        var query = new GetWindowActivityLogQuery(repository);
        
        // Act
        var result = query.Execute();
        
        // Assert
        result.Should().NotBeNull();
        result.RecordCount.Should().Be(0);
    }

    [Fact]
    public void Execute_WithExistingRecords_ShouldReturnLogWithRecords()
    {
        // Arrange
        var repository = new TestWindowActivityRepository();
        var log = WindowActivityLog.Create();
        var record = CreateTestRecord();
        log.AddRecord(record);
        repository.SaveWindowActivityLog(log);
        
        var query = new GetWindowActivityLogQuery(repository);
        
        // Act
        var result = query.Execute();
        
        // Assert
        result.Should().NotBeNull();
        result.RecordCount.Should().Be(1);
        result.Records.First().WindowTitle.Value.Should().Be("Test Window");
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
}