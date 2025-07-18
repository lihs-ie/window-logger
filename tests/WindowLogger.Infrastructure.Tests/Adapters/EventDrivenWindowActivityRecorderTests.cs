using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WindowLogger.Domain.Entities;
using WindowLogger.Infrastructure.Adapters;
using Xunit;

namespace WindowLogger.Infrastructure.Tests.Adapters;

public sealed class EventDrivenWindowActivityRecorderTests : IDisposable
{
    private readonly Mock<ILogger<EventDrivenWindowActivityRecorder>> _mockLogger;
    private EventDrivenWindowActivityRecorder? _recorder;

    public EventDrivenWindowActivityRecorderTests()
    {
        _mockLogger = new Mock<ILogger<EventDrivenWindowActivityRecorder>>();
    }

    [Fact]
    public void Constructor_ShouldInitializeEventHook()
    {
        // Arrange & Act
        _recorder = new EventDrivenWindowActivityRecorder(_mockLogger.Object);

        // Assert
        _recorder.Should().NotBeNull();
    }

    [Fact]
    public void RecordCurrentWindowActivity_ShouldReturnValidRecord()
    {
        // Arrange
        _recorder = new EventDrivenWindowActivityRecorder(_mockLogger.Object);

        // Act
        var result = _recorder.RecordCurrentWindowActivity();

        // Assert
        result.Should().NotBeNull();
        result.WindowTitle.Should().NotBeNull();
        result.WindowTitle.Value.Should().NotBeEmpty();
        result.RecordedAt.Should().NotBeNull();
        result.Id.Should().NotBeNull();
    }

    [Fact]
    public void Dispose_ShouldCleanupEventHook()
    {
        // Arrange
        _recorder = new EventDrivenWindowActivityRecorder(_mockLogger.Object);

        // Act & Assert (例外が発生しないことを確認)
        Action disposing = () => _recorder.Dispose();
        disposing.Should().NotThrow();
    }

    public void Dispose()
    {
        _recorder?.Dispose();
    }
}