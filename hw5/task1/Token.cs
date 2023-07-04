namespace hw5;

/// <summary>
/// Class for tokens can appear in file.
/// </summary>
/// <value></value>
public record Token
{
    /// <summary>Token representing ":".</summary>
    public record Colon()
    : Token();

    /// <summary>Token representing ",".</summary>
    public record Comma()
    : Token();

    /// <summary>Token representing "(".</summary>
    public record LeftParenthesis()
    : Token();

    /// <summary>Token representing ")".</summary>
    public record RightParenthesis()
    : Token();

    /// <summary>Token representing number.</summary>
    /// <param name="value">Number that token represents.</param>
    public record Number(int value)
    : Token();
}