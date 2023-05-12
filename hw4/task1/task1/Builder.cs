namespace hw4task1;

/// <summary>
/// Static class providing function to build <see cref="ExpressionTree"/> from collection of <see cref="Token"/>.
/// </summary>
public static class Builder
{
    /// <summary>
    /// Build <see cref="ExpressionTree"/> from collection of <see cref="Token"/>.
    /// </summary>
    /// <param name="expr">Collection of tokens to build <see cref="ExpressionTree"/> from.</param>
    /// <exception cref="IncorrectExpressionException">THrown when given collection of <see cref="Token"/> does not form correct expression.</exception>
    /// <returns>Generated <see cref="ExpressionTree"/>.</returns>
    public static ExpressionTree BuildExpressionTree(IEnumerable<Token> expr)
    {
        IEnumerator<Token> enumerator = expr.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new IncorrectExpressionException("Given stack is empty");
        }

        ExpressionTree ans = BuildExpressionTree(enumerator);
        if (enumerator.MoveNext())
        {
            throw new IncorrectExpressionException("Given expression is incorrect - there are still numbers after building binary operation");
        }

        return ans;
    }

    private static ExpressionTree BuildExpressionTree(IEnumerator<Token> expr)
    {
        switch (expr.Current)
        {
            case Token.Add:
            case Token.Sub:
            case Token.Mul:
            case Token.Div:
                {
                    Token binop = expr.Current;
                    if (!expr.MoveNext())
                    {
                        throw new IncorrectExpressionException("Given expression is incorrect - not enough numbers for building binary operation");
                    }

                    ExpressionTree l = BuildExpressionTree(expr);

                    if (!expr.MoveNext())
                    {
                        throw new IncorrectExpressionException("Given expression is incorrect - not enough numbers for building binary operation");
                    }

                    ExpressionTree r = BuildExpressionTree(expr);

                    return new ExpressionTree.Binop(Binop(binop), l, r);
                }

            case Token.Number number:
                return new ExpressionTree.Number(number.value);
            default:
                throw new IncorrectExpressionException("Given expression is incorrect - found an unknown token");
        }
    }

    private static ExpressionTree.BinopType Binop(Token tk)
        => tk switch
        {
            Token.Add => ExpressionTree.BinopType.Add,
            Token.Sub => ExpressionTree.BinopType.Sub,
            Token.Mul => ExpressionTree.BinopType.Mul,
            Token.Div => ExpressionTree.BinopType.Div,
            _ => throw new UnknownBinopException()
        };
}

/// <summary>
/// Exception for incorrect expression.
/// </summary>
[System.Serializable]
public class IncorrectExpressionException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public IncorrectExpressionException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectExpressionException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectExpressionException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected IncorrectExpressionException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}