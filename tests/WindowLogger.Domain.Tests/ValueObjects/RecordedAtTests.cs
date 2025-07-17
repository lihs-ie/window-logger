using FluentAssertions;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Domain.Tests.ValueObjects;

public class RecordedAtTests
{
    [Fact]
    public void RecordedAt_正常な日時で作成できる()
    {
        // Arrange
        var dateTime = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        
        // Act
        var recordedAt = RecordedAt.Create(dateTime);
        
        // Assert
        recordedAt.Should().NotBeNull();
        recordedAt.Value.Should().Be(dateTime);
    }
    
    [Fact]
    public void RecordedAt_現在時刻で作成できる()
    {
        // Arrange
        var before = DateTime.UtcNow;
        
        // Act
        var recordedAt = RecordedAt.Now();
        
        // Assert
        var after = DateTime.UtcNow;
        recordedAt.Value.Should().BeOnOrAfter(before);
        recordedAt.Value.Should().BeOnOrBefore(after);
        recordedAt.Value.Kind.Should().Be(DateTimeKind.Utc);
    }
    
    [Fact]
    public void RecordedAt_未来の日時では作成できない()
    {
        // Arrange
        var futureDateTime = DateTime.UtcNow.AddDays(1);
        
        // Act
        var action = () => RecordedAt.Create(futureDateTime);
        
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Recorded time cannot be in the future.");
    }
    
    [Fact]
    public void RecordedAt_過去すぎる日時では作成できない()
    {
        // Arrange
        var tooOldDateTime = DateTime.UtcNow.AddYears(-10);
        
        // Act
        var action = () => RecordedAt.Create(tooOldDateTime);
        
        // Assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Recorded time is too old.");
    }
    
    [Fact]
    public void RecordedAt_UTCでない日時は自動的にUTCに変換される()
    {
        // Arrange
        var localDateTime = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Local);
        
        // Act
        var recordedAt = RecordedAt.Create(localDateTime);
        
        // Assert
        recordedAt.Value.Kind.Should().Be(DateTimeKind.Utc);
    }
    
    [Fact]
    public void RecordedAt_同じ日時のインスタンスは等価である()
    {
        // Arrange
        var dateTime = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var recordedAt1 = RecordedAt.Create(dateTime);
        var recordedAt2 = RecordedAt.Create(dateTime);
        
        // Act & Assert
        recordedAt1.Should().Be(recordedAt2);
        recordedAt1.GetHashCode().Should().Be(recordedAt2.GetHashCode());
    }
    
    [Fact]
    public void RecordedAt_ISO8601形式の文字列で表現できる()
    {
        // Arrange
        var dateTime = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var recordedAt = RecordedAt.Create(dateTime);
        
        // Act
        var iso8601String = recordedAt.ToIso8601String();
        
        // Assert
        iso8601String.Should().Be("2024-01-01T12:00:00.0000000Z");
    }
}