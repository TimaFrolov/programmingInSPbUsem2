namespace hw4task1;

using System.Text;

/// <summary>
/// Base class for expression
/// </summary>
public abstract record ExpressionTree
{
    /// <summary>
    /// Class representing expression with one number in it.
    /// </summary>
    /// <param name="value"></param>
    public record Number(int value)
    : ExpressionTree()
    {
        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
            => this.Print(new StringBuilder()).ToString();

        /// <inheritdoc cref="ExpressionTree.Print"/>
        protected override StringBuilder Print(StringBuilder builder)
            => builder.Append(this.value);
    }

    /// <summary>
    /// Class representing binary operation.
    /// </summary>
    /// <param name="binop">Type of binary operation.</param>
    /// <param name="left">Left subtree.</param>
    /// <param name="right">Right subtree.</param>
    public record Binop(BinopType binop, ExpressionTree left, ExpressionTree right)
    : ExpressionTree()
    {
        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
            => this.Print(new StringBuilder()).ToString();

        /// <inheritdoc cref="ExpressionTree.Print"/>
        protected override StringBuilder Print(StringBuilder builder)
        {
            builder.Append('(');
            builder.Append(ToChar(this.binop));
            builder.Append(' ');
            this.left.Print(builder);
            builder.Append(' ');
            this.right.Print(builder);
            builder.Append(')');
            return builder;
        }
    }

    private ExpressionTree()
    {
    }

    /// <summary>
    /// Evaluate expression.
    /// </summary>
    /// <returns>Value of evaluated expression.</returns>
    public Fraction Evaluate()
        => this switch
        {
            Number num => num.value,
            Binop op => Operation(op.binop)(op.left.Evaluate(), op.right.Evaluate()),
            _ => throw new IncorrectNodeException()
        };

    /// <summary> Binary operations available to be used in <see cref="ExpressionTree"/>.</summary>
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

    private static Func<Fraction, Fraction, Fraction> Operation(BinopType op)
        => op switch
        {
            BinopType.Add => (Fraction x, Fraction y) => x + y,
            BinopType.Sub => (Fraction x, Fraction y) => x - y,
            BinopType.Mul => (Fraction x, Fraction y) => x * y,
            BinopType.Div => (Fraction x, Fraction y) => x / y,
            _ => throw new UnknownBinopException($"Unknown operation: {op}")
        };

    private static char ToChar(BinopType binop)
        => binop switch
        {
            BinopType.Add => '+',
            BinopType.Sub => '-',
            BinopType.Mul => '*',
            BinopType.Div => '/',
            _ => throw new UnknownBinopException($"Unknown operation: {binop}")
        };

    /// <summary>
    /// Appends string representation of expression to given string builder.
    /// </summary>
    /// <param name="builder">String builder to append string representation of this expression to.</param>
    /// <returns>Builder passed as param.</returns>
    protected abstract StringBuilder Print(StringBuilder builder);
}

/// <summary>
/// Exception for incorrect node in expression tree.
/// </summary>
[System.Serializable]
public class IncorrectNodeException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public IncorrectNodeException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectNodeException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectNodeException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected IncorrectNodeException(
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