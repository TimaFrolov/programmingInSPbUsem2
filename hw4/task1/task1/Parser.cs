namespace hw4task1;

/// <summary>
/// Static class providing function to parse file into list of tokens. 
/// </summary>
public static class Parser
{
    private static readonly Token.Add AddToken = new();
    private static readonly Token.Sub SubToken = new();
    private static readonly Token.Mul MulToken = new();
    private static readonly Token.Div DivToken = new();

    /// <summary>
    /// Convert content of given file to <see cref="List&lt;Token&gt;"/>.
    /// </summary>
    /// <param name="stream">File to read from.</param>
    /// <exception cref="UnexpectedTokenException">Thrown given files contains unexpected token.</exception>
    /// <returns>List of tokens generated from given file.</returns>
    public static List<Token> ParseExpression(FileStream stream)
    {
        var expr = new List<Token>();

        int curByte = 0;
        bool readFromStream = true;
        while (stream.Position < stream.Length)
        {
            if (readFromStream)
            {
                curByte = stream.ReadByte();
            }

            readFromStream = true;
            switch (curByte)
            {
                case '+':
                    expr.Add(AddToken);
                    break;
                case '-':
                    expr.Add(SubToken);
                    break;
                case '*':
                    expr.Add(MulToken);
                    break;
                case '/':
                    expr.Add(DivToken);
                    break;
                case int x when x >= '0' && x <= '9':
                    (int number, curByte) = ParseInt(curByte, stream);
                    readFromStream = false;
                    expr.Add(new Token.Number(number));
                    break;
                case '(':
                case ')':
                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    continue;
                default:
                    throw new UnexpectedTokenException();
            }
        }

        return expr;
    }

    private static (int result, int nextByte) ParseInt(int firstByte, FileStream stream)
    {
        int ans = firstByte - '0';
        while (stream.Position < stream.Length)
        {
            switch (stream.ReadByte())
            {
                case int x when x >= '0' && x <= '9':
                    ans = (ans * 10) + x - '0';
                    break;
                case int x:
                    return (ans, x);
            }
        }

        return (ans, -1);
    }

    private static ExpressionTree.BinopType ToBinop(int binop)
        => binop switch
        {
            '+' => ExpressionTree.BinopType.Add,
            '-' => ExpressionTree.BinopType.Sub,
            '*' => ExpressionTree.BinopType.Mul,
            '/' => ExpressionTree.BinopType.Div,
            _ => throw new UnknownBinopException($"Unknown operation: {binop}")
        };
}

/// <summary>
/// Exception for unexpected token.
/// </summary>
[System.Serializable]
public class UnexpectedTokenException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public UnexpectedTokenException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public UnexpectedTokenException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public UnexpectedTokenException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected UnexpectedTokenException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}