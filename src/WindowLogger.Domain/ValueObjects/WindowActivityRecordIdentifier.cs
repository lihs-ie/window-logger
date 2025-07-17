namespace WindowLogger.Domain.ValueObjects;

public readonly record struct WindowActivityRecordIdentifier : IComparable<WindowActivityRecordIdentifier>
{
    public Ulid Value { get; }

    private WindowActivityRecordIdentifier(Ulid value)
    {
        Value = value;
    }

    public static WindowActivityRecordIdentifier Create(Ulid value)
    {
        return new WindowActivityRecordIdentifier(value);
    }

    public static WindowActivityRecordIdentifier NewId()
    {
        return new WindowActivityRecordIdentifier(Ulid.NewUlid());
    }

    public int CompareTo(WindowActivityRecordIdentifier other)
    {
        return Value.CompareTo(other.Value);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}