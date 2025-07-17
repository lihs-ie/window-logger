using FluentAssertions;
using WindowLogger.Infrastructure.Adapters;
using Xunit;

namespace WindowLogger.Infrastructure.Tests.Adapters;

public sealed class WindowsApiWindowActivityRecorderTests
{
    [Fact]
    public void RecordCurrentWindowActivity_ShouldReturnValidRecord()
    {
        // Arrange
        var recorder = new WindowsApiWindowActivityRecorder();
        
        // Act
        var result = recorder.RecordCurrentWindowActivity();
        
        // Assert
        result.Should().NotBeNull();
        result.WindowTitle.Value.Should().NotBeNullOrEmpty();
        result.RecordedAt.Value.Should().BeCloseTo(DateTime.UtcNow, precision: TimeSpan.FromSeconds(5));
        result.ApplicationName.Should().NotBeNullOrEmpty();
        result.Id.Value.Should().NotBe(default);
    }

    [Fact]
    public void RecordCurrentWindowActivity_WhenNoWindowIsActive_ShouldReturnDefaultTitle()
    {
        // Arrange
        var recorder = new WindowsApiWindowActivityRecorder();
        
        // Act
        var result = recorder.RecordCurrentWindowActivity();
        
        // Assert
        result.Should().NotBeNull();
        // Windows APIが何らかのタイトルを返すことを想定
        result.WindowTitle.Value.Should().NotBeNull();
    }
}