using FluentAssertions;
using WindowLogger.Application.Commands;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Commands;

public sealed class RecordWindowActivityCommandTests
{
    [Fact]
    public void Execute_ShouldRecordWindowActivity()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        var repository = new TestWindowActivityRepository();
        var command = new RecordWindowActivityCommand(recorder, repository);
        
        // Act
        command.Execute();
        
        // Assert
        var log = repository.LoadWindowActivityLog();
        log.RecordCount.Should().Be(1);
        log.Records.First().WindowTitle.Value.Should().Be("Test Window");
    }

    [Fact]
    public void Execute_MultipleCallsRecord_ShouldAddMultipleRecords()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        var repository = new TestWindowActivityRepository();
        var command = new RecordWindowActivityCommand(recorder, repository);
        
        // Act
        command.Execute();
        command.Execute();
        
        // Assert
        var log = repository.LoadWindowActivityLog();
        log.RecordCount.Should().Be(2);
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
}