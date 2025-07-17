namespace WindowLogger.Domain.ValueObjects;

public readonly record struct RecordedAt
{
    private const string FUTURE_TIME_ERROR_MESSAGE = "Recorded time cannot be in the future.";
    private const string TOO_OLD_TIME_ERROR_MESSAGE = "Recorded time is too old.";
    private const int MAXIMUM_AGE_IN_YEARS = 5;
    private const double DAYS_PER_YEAR = 365.25;
    private static readonly TimeSpan MAXIMUM_AGE = TimeSpan.FromDays(MAXIMUM_AGE_IN_YEARS * DAYS_PER_YEAR);

    public DateTime Value { get; }

    private RecordedAt(DateTime value)
    {
        Value = value;
    }

    public static RecordedAt Create(DateTime value)
    {
        var utcValue = value.Kind == DateTimeKind.Utc
            ? value
            : value.ToUniversalTime();

        var now = DateTime.UtcNow;

        if (utcValue > now)
        {
            throw new ArgumentException(FUTURE_TIME_ERROR_MESSAGE);
        }

        if (utcValue < now.Subtract(MAXIMUM_AGE))
        {
            throw new ArgumentException(TOO_OLD_TIME_ERROR_MESSAGE);
        }

        return new RecordedAt(utcValue);
    }

    public static RecordedAt Now()
    {
        return new RecordedAt(DateTime.UtcNow);
    }

    public string ToIso8601String()
    {
        return Value.ToString("O");
    }
}

