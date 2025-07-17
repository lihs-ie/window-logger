using System.Collections.Immutable;
using WindowLogger.Domain.Entities;

namespace WindowLogger.Domain.Aggregates;

public class WindowActivityLog
{
    private readonly List<WindowActivityRecord> _records = new();

    public IReadOnlyCollection<WindowActivityRecord> Records => _records.AsReadOnly();
    public int RecordCount => _records.Count;

    private WindowActivityLog()
    {
    }

    public static WindowActivityLog Create()
    {
        return new WindowActivityLog();
    }

    public void AddRecord(WindowActivityRecord record)
    {
        _records.Add(record);
        _records.Sort((x, y) => x.CompareTo(y));
    }

    public IReadOnlyCollection<WindowActivityRecord> GetRecordsByTimeRange(DateTime startTime, DateTime endTime)
    {
        return _records
            .Where(r => r.RecordedAt.Value >= startTime && r.RecordedAt.Value <= endTime)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<WindowActivityRecord> GetRecordsByApplicationName(string applicationName)
    {
        return _records
            .Where(r => r.ApplicationName == applicationName)
            .ToList()
            .AsReadOnly();
    }
}