using FluentAssertions;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Domain.Tests.Entities;

public class WindowActivityRecordTests
{
    [Fact]
    public void WindowActivityRecord_正常な値で作成できる()
    {
        // Arrange
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Visual Studio Code");
        var recordedAt = RecordedAt.Now();
        
        // Act
        var record = WindowActivityRecord.Create(id, title, recordedAt);
        
        // Assert
        record.Should().NotBeNull();
        record.Id.Should().Be(id);
        record.WindowTitle.Should().Be(title);
        record.RecordedAt.Should().Be(recordedAt);
    }
    
    [Fact]
    public void WindowActivityRecord_アプリケーション名を推測できる()
    {
        // Arrange
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Document.txt - Notepad");
        var recordedAt = RecordedAt.Now();
        
        // Act
        var record = WindowActivityRecord.Create(id, title, recordedAt);
        
        // Assert
        record.ApplicationName.Should().Be("Notepad");
    }
    
    [Fact]
    public void WindowActivityRecord_複雑なタイトルからアプリケーション名を推測できる()
    {
        // Arrange
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("MyProject - Microsoft Visual Studio");
        var recordedAt = RecordedAt.Now();
        
        // Act
        var record = WindowActivityRecord.Create(id, title, recordedAt);
        
        // Assert
        record.ApplicationName.Should().Be("Microsoft Visual Studio");
    }
    
    [Fact]
    public void WindowActivityRecord_アプリケーション名が推測できない場合は不明になる()
    {
        // Arrange
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("SimpleTitle");
        var recordedAt = RecordedAt.Now();
        
        // Act
        var record = WindowActivityRecord.Create(id, title, recordedAt);
        
        // Assert
        record.ApplicationName.Should().Be("Unknown");
    }
    
    [Fact]
    public void WindowActivityRecord_同じIDのインスタンスは等価である()
    {
        // Arrange
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Test");
        var recordedAt = RecordedAt.Now();
        
        var record1 = WindowActivityRecord.Create(id, title, recordedAt);
        var record2 = WindowActivityRecord.Create(id, title, recordedAt);
        
        // Act & Assert
        record1.Should().Be(record2);
        record1.GetHashCode().Should().Be(record2.GetHashCode());
    }
    
    [Fact]
    public void WindowActivityRecord_異なるIDのインスタンスは等価でない()
    {
        // Arrange
        var id1 = WindowActivityRecordIdentifier.NewId();
        var id2 = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Test");
        var recordedAt = RecordedAt.Now();
        
        var record1 = WindowActivityRecord.Create(id1, title, recordedAt);
        var record2 = WindowActivityRecord.Create(id2, title, recordedAt);
        
        // Act & Assert
        record1.Should().NotBe(record2);
    }
    
    [Fact]
    public void WindowActivityRecord_時系列順で比較できる()
    {
        // Arrange
        var id1 = WindowActivityRecordIdentifier.NewId();
        var id2 = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Test");
        var recordedAt1 = RecordedAt.Create(new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        var recordedAt2 = RecordedAt.Create(new DateTime(2024, 1, 1, 13, 0, 0, DateTimeKind.Utc));
        
        var record1 = WindowActivityRecord.Create(id1, title, recordedAt1);
        var record2 = WindowActivityRecord.Create(id2, title, recordedAt2);
        
        // Act
        var comparison = record1.CompareTo(record2);
        
        // Assert
        comparison.Should().BeLessThan(0); // record1 < record2
    }
}