using WindowLogger.Domain.ValueObjects;

namespace WindowLogger.Domain.Entities;

public readonly record struct WindowActivityRecord : IComparable<WindowActivityRecord>
{
    private const string UNKNOWN_APPLICATION_NAME = "Unknown";

    public WindowActivityRecordIdentifier Id { get; }
    public WindowTitle WindowTitle { get; }
    public RecordedAt RecordedAt { get; }
    public string ApplicationName { get; }

    private WindowActivityRecord(
        WindowActivityRecordIdentifier id,
        WindowTitle windowTitle,
        RecordedAt recordedAt,
        string applicationName)
    {
        Id = id;
        WindowTitle = windowTitle;
        RecordedAt = recordedAt;
        ApplicationName = applicationName;
    }

    public static WindowActivityRecord Create(
        WindowActivityRecordIdentifier id,
        WindowTitle windowTitle,
        RecordedAt recordedAt)
    {
        var applicationName = ExtractApplicationName(windowTitle);

        return new WindowActivityRecord(id, windowTitle, recordedAt, applicationName);
    }

    private static string ExtractApplicationName(WindowTitle windowTitle)
    {
        var title = windowTitle.Value;

        // パターン1: "filename - ApplicationName" 形式
        const string DASH_SEPARATOR = " - ";
        var dashIndex = title.LastIndexOf(DASH_SEPARATOR);
        if (dashIndex > 0 && dashIndex < title.Length - DASH_SEPARATOR.Length)
        {
            return title.Substring(dashIndex + DASH_SEPARATOR.Length);
        }

        // パターン2: 単純なタイトルの場合は不明
        return UNKNOWN_APPLICATION_NAME;
    }

    public int CompareTo(WindowActivityRecord other)
    {
        return RecordedAt.Value.CompareTo(other.RecordedAt.Value);
    }
}