using FluentAssertions;
using WindowLogger.Application.Ports;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;
using Xunit;

namespace WindowLogger.Application.Tests.Ports;

public sealed class IWindowActivityRecorderTests
{
    [Fact]
    public void RecordCurrentWindowActivity_ShouldReturnWindowActivityRecord()
    {
        // Arrange
        var recorder = new TestWindowActivityRecorder();
        
        // Act
        var result = recorder.RecordCurrentWindowActivity();
        
        // Assert
        result.Should().NotBeNull();
        result.WindowTitle.Value.Should().NotBeNullOrEmpty();
        result.RecordedAt.Value.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(1));
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
}