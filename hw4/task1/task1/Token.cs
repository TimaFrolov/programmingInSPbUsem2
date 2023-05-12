namespace hw4task1;

/// <summary>
/// Tokens for parser.
/// </summary>
public record Token
{
    /// <summary> "+" token. </summary>
    public record Add()
    : Token();

    /// <summary> "-" token. </summary>
    public record Sub()
    : Token();

    /// <summary> "*" token. </summary>
    public record Mul()
    : Token();

    /// <summary> "/" token. </summary>
    public record Div()
    : Token();

    /// <summary> Token contains integer number. </summary>
    public record Number(int value)
    : Token();

    private Token()
    {
    }
}