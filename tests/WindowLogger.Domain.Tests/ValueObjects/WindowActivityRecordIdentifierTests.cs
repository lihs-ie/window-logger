using FluentAssertions;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Domain.Tests.ValueObjects;

public class WindowActivityRecordIdentifierTests
{
    [Fact]
    public void WindowActivityRecordIdentifier_新しいIDを生成できる()
    {
        // Act
        var identifier = WindowActivityRecordIdentifier.NewId();
        
        // Assert
        identifier.Should().NotBeNull();
        identifier.Value.Should().NotBe(Ulid.Empty);
    }
    
    [Fact]
    public void WindowActivityRecordIdentifier_有効なULIDで作成できる()
    {
        // Arrange
        var ulid = Ulid.NewUlid();
        
        // Act
        var identifier = WindowActivityRecordIdentifier.Create(ulid);
        
        // Assert
        identifier.Value.Should().Be(ulid);
    }
    
    [Fact]
    public void WindowActivityRecordIdentifier_連続生成で異なるIDが作成される()
    {
        // Act
        var identifier1 = WindowActivityRecordIdentifier.NewId();
        var identifier2 = WindowActivityRecordIdentifier.NewId();
        
        // Assert
        identifier1.Should().NotBe(identifier2);
    }
    
    [Fact]
    public void WindowActivityRecordIdentifier_同じULIDのインスタンスは等価である()
    {
        // Arrange
        var ulid = Ulid.NewUlid();
        var identifier1 = WindowActivityRecordIdentifier.Create(ulid);
        var identifier2 = WindowActivityRecordIdentifier.Create(ulid);
        
        // Act & Assert
        identifier1.Should().Be(identifier2);
        identifier1.GetHashCode().Should().Be(identifier2.GetHashCode());
    }
    
    [Fact]
    public void WindowActivityRecordIdentifier_文字列表現を取得できる()
    {
        // Arrange
        var ulid = Ulid.NewUlid();
        var identifier = WindowActivityRecordIdentifier.Create(ulid);
        
        // Act
        var stringValue = identifier.ToString();
        
        // Assert
        stringValue.Should().Be(ulid.ToString());
        stringValue.Should().HaveLength(26); // ULIDの標準長
    }
    
    [Fact]
    public void WindowActivityRecordIdentifier_時系列順序を保持する()
    {
        // Arrange
        var identifier1 = WindowActivityRecordIdentifier.NewId();
        Thread.Sleep(1); // 時間差を確保
        var identifier2 = WindowActivityRecordIdentifier.NewId();
        
        // Act
        var comparison = identifier1.CompareTo(identifier2);
        
        // Assert
        comparison.Should().BeLessThan(0); // identifier1 < identifier2
    }
}