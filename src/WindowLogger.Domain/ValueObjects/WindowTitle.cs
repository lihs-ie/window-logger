namespace WindowLogger.Domain.ValueObjects;

public readonly record struct WindowTitle
{
    private const string NULL_OR_WHITESPACE_ERROR_MESSAGE = "Window title cannot be null or whitespace.";

    public string Value { get; }

    private WindowTitle(string value)
    {
        Value = value;
    }

    public static WindowTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(NULL_OR_WHITESPACE_ERROR_MESSAGE);
        }

        return new WindowTitle(value);
    }
}