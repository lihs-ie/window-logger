using FluentAssertions;
using WindowLogger.Application.Ports;
using WindowLogger.Application.Queries;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Queries;

public sealed class GetWindowActivityRecordsByTimeRangeQueryTests
{
    [Fact]
    public void Execute_ShouldReturnRecordsWithinTimeRange()
    {
        // Arrange
        var repository = new TestWindowActivityRepository();
        var log = WindowActivityLog.Create();
        
        var baseTime = DateTime.UtcNow.AddHours(-2);
        var record1 = CreateTestRecord("Window 1", baseTime);
        var record2 = CreateTestRecord("Window 2", baseTime.AddHours(1));
        var record3 = CreateTestRecord("Window 3", baseTime.AddHours(2));
        
        log.AddRecord(record1);
        log.AddRecord(record2);
        log.AddRecord(record3);
        repository.SaveWindowActivityLog(log);
        
        var query = new GetWindowActivityRecordsByTimeRangeQuery(repository);
        
        // Act
        var result = query.Execute(baseTime.AddMinutes(30), baseTime.AddMinutes(90));
        
        // Assert
        result.Should().HaveCount(1);
        result.First().WindowTitle.Value.Should().Be("Window 2");
    }

    [Fact]
    public void Execute_WithNoRecordsInRange_ShouldReturnEmptyCollection()
    {
        // Arrange
        var repository = new TestWindowActivityRepository();
        var query = new GetWindowActivityRecordsByTimeRangeQuery(repository);
        
        // Act
        var result = query.Execute(DateTime.UtcNow.AddHours(-1), DateTime.UtcNow);
        
        // Assert
        result.Should().BeEmpty();
    }

    private static WindowActivityRecord CreateTestRecord(string title, DateTime recordedAt)
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var windowTitle = WindowTitle.Create(title);
        var recordedAtValue = RecordedAt.Create(recordedAt);
        
        return WindowActivityRecord.Create(id, windowTitle, recordedAtValue);
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