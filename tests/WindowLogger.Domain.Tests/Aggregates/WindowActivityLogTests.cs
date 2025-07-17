using FluentAssertions;
using WindowLogger.Domain.Aggregates;
using WindowLogger.Domain.Entities;
using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Domain.Tests.Aggregates;

public class WindowActivityLogTests
{
    [Fact]
    public void WindowActivityLog_新しいログを作成できる()
    {
        // Act
        var log = WindowActivityLog.Create();
        
        // Assert
        log.Should().NotBeNull();
        log.Records.Should().BeEmpty();
        log.RecordCount.Should().Be(0);
    }
    
    [Fact]
    public void WindowActivityLog_記録を追加できる()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record = CreateSampleRecord();
        
        // Act
        log.AddRecord(record);
        
        // Assert
        log.Records.Should().HaveCount(1);
        log.Records.First().Should().Be(record);
        log.RecordCount.Should().Be(1);
    }
    
    [Fact]
    public void WindowActivityLog_複数の記録を追加できる()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record1 = CreateSampleRecord("App1");
        var record2 = CreateSampleRecord("App2");
        
        // Act
        log.AddRecord(record1);
        log.AddRecord(record2);
        
        // Assert
        log.Records.Should().HaveCount(2);
        log.RecordCount.Should().Be(2);
    }
    
    [Fact]
    public void WindowActivityLog_記録は時系列順でソートされる()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var laterRecord = CreateRecordWithTime(new DateTime(2024, 1, 1, 13, 0, 0, DateTimeKind.Utc));
        var earlierRecord = CreateRecordWithTime(new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        
        // Act
        log.AddRecord(laterRecord);
        log.AddRecord(earlierRecord);
        
        // Assert
        log.Records.Should().HaveCount(2);
        log.Records.First().Should().Be(earlierRecord);
        log.Records.Last().Should().Be(laterRecord);
    }
    
    [Fact]
    public void WindowActivityLog_指定期間の記録を取得できる()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record1 = CreateRecordWithTime(new DateTime(2024, 1, 1, 10, 0, 0, DateTimeKind.Utc));
        var record2 = CreateRecordWithTime(new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc));
        var record3 = CreateRecordWithTime(new DateTime(2024, 1, 1, 14, 0, 0, DateTimeKind.Utc));
        
        log.AddRecord(record1);
        log.AddRecord(record2);
        log.AddRecord(record3);
        
        var startTime = new DateTime(2024, 1, 1, 11, 0, 0, DateTimeKind.Utc);
        var endTime = new DateTime(2024, 1, 1, 13, 0, 0, DateTimeKind.Utc);
        
        // Act
        var filteredRecords = log.GetRecordsByTimeRange(startTime, endTime);
        
        // Assert
        filteredRecords.Should().HaveCount(1);
        filteredRecords.First().Should().Be(record2);
    }
    
    [Fact]
    public void WindowActivityLog_アプリケーション名での絞り込みができる()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record1 = CreateSampleRecord("Visual Studio Code");
        var record2 = CreateSampleRecord("Notepad");
        var record3 = CreateSampleRecord("Visual Studio Code");
        
        log.AddRecord(record1);
        log.AddRecord(record2);
        log.AddRecord(record3);
        
        // Act
        var filteredRecords = log.GetRecordsByApplicationName("Visual Studio Code");
        
        // Assert
        filteredRecords.Should().HaveCount(2);
        filteredRecords.Should().Contain(record1);
        filteredRecords.Should().Contain(record3);
    }
    
    [Fact]
    public void WindowActivityLog_読み取り専用のコレクションを返す()
    {
        // Arrange
        var log = WindowActivityLog.Create();
        var record = CreateSampleRecord();
        log.AddRecord(record);
        
        // Act
        var records = log.Records;
        
        // Assert
        records.Should().BeAssignableTo<IReadOnlyCollection<WindowActivityRecord>>();
    }
    
    private static WindowActivityRecord CreateSampleRecord(string applicationName = "Test App")
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create($"Document - {applicationName}");
        var recordedAt = RecordedAt.Now();
        
        return WindowActivityRecord.Create(id, title, recordedAt);
    }
    
    private static WindowActivityRecord CreateRecordWithTime(DateTime dateTime)
    {
        var id = WindowActivityRecordIdentifier.NewId();
        var title = WindowTitle.Create("Document - Test App");
        var recordedAt = RecordedAt.Create(dateTime);
        
        return WindowActivityRecord.Create(id, title, recordedAt);
    }
}