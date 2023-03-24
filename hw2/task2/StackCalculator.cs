namespace Hw2Task2;

/// <summary>
/// Exception for incorrect stack.
/// </summary>
[System.Serializable]
public class IncorrectStackException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public IncorrectStackException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectStackException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectStackException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected IncorrectStackException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}

/// <summary>
/// Exception for unknown binary opeation.
/// </summary>
[System.Serializable]
public class UnknownBinopException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public UnknownBinopException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public UnknownBinopException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public UnknownBinopException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected UnknownBinopException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}

/// <summary>
/// Class for performing evaluation of expression in prefix notation.
/// </summary>
public static class StackCalculator
{
    /// <summary>
    /// Evaluate expression given in prefix notation.
    /// </summary>
    /// <param name="expr"> Expression in prefix notation, containing <see cref="StackElement"/>. </param>
    /// <returns> Result of evaluating. </returns>
    public static Fraction Evaluate(IEnumerable<StackElement> expr)
    {
        IEnumerator<StackElement> enumerator = expr.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new IncorrectStackException("Given stack is empty");
        }

        Fraction ans = Evaluate(enumerator);
        if (enumerator.MoveNext())
        {
            throw new IncorrectStackException("Given stack is incorrect - there are still numbers after evaluating expression");
        }

        return ans;
    }

    private static Func<Fraction, Fraction, Fraction> Operation(BinopType op)
        => op switch
        {
            BinopType.Add => (Fraction x, Fraction y) => x + y,
            BinopType.Sub => (Fraction x, Fraction y) => x - y,
            BinopType.Mul => (Fraction x, Fraction y) => x * y,
            BinopType.Div => (Fraction x, Fraction y) => x / y,
            _ => throw new UnknownBinopException($"Unknown operation: {op}")
        };

    private static Fraction Evaluate(IEnumerator<StackElement> expr)
    {
        switch (expr.Current)
        {
            case StackElement.Binop binop:
                {
                    Fraction l, r;

                    if (!expr.MoveNext())
                    {
                        throw new IncorrectStackException("Given stack is incorrect - not enough numbers for evaluating binary operation");
                    }

                    Fraction r = Evaluate(expr);

                    if (!expr.MoveNext())
                    {
                        throw new IncorrectStackException("Given stack is incorrect - not enough numbers for evaluating binary operation");
                    }

                    Fraction l = Evaluate(expr);

                    return Operation(binop.type)(l, r);
                }

            case StackElement.Number number:
                return number.value;
            default:
                throw new IncorrectStackException("Given stack is incorrect - found an element that niether a number nor a binary operation");
        }
    }
}

/// <summary> Binary operations available to be used in stack calculator. </summary>
public enum BinopType
{
    /// <summary> A + B. </summary>
    Add,

    /// <summary> A - B. </summary>
    Sub,

    /// <summary> A * B. </summary>
    Mul,

    /// <summary> A / B. </summary>
    Div,
}

/// <summary> Class that <see cref="StackCalculator"/> class can do calculations on. </summary>
[System.Serializable]
public record StackElement
{
    /// <summary> Binary operation element. </summary>
    /// <param name="type"> Type of binary operation. </param>
    public record Binop(BinopType type)
    : StackElement();

    /// <summary> Number element. </summary>
    /// <param name="value"> Value of element. </param>
    public record Number(Fraction value)
    : StackElement();

    private StackElement()
    {
    }
}