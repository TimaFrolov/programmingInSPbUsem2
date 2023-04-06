namespace hw5;

/// <summary>
/// Static class providing function to parse file into list of tokens.
/// </summary>
public class Parser
{
    private FileStream stream;
    private Graph<int> graph;
    private int curByte;

    /// <summary>
    /// Initializes a new instance of the <see cref="Parser"/> class that reads from given file.
    /// </summary>
    /// <param name="stream">File to read data from.</param>
    public Parser(FileStream stream)
    {
        this.stream = stream;
        this.graph = new Graph<int>();
        this.curByte = -1;
    }

    /// <summary>
    /// Convert content of file to <see cref="Graph&lt;Int32&gt;"/>.
    /// </summary>
    /// <exception cref="IncorrectGraphException">Thrown given files contains unexpected token.</exception>
    /// <returns>Graph generated from file.</returns>
    public Graph<int> ParseFile()
    {
        this.curByte = this.stream.ReadByte();
        while (this.stream.Position < this.stream.Length)
        {
            this.ParseNode();
        }

        return this.graph;
    }

    private void ParseNode()
    {
        this.SkipSpaces();
        int node = this.ParseInt();
        this.ConsumeOrThrowException(':');

        while (this.stream.Position < this.stream.Length)
        {
            this.SkipSpaces();
            int node2 = this.ParseInt();
            this.SkipSpaces();
            this.ConsumeOrThrowException('(');
            this.SkipSpaces();
            int length = this.ParseInt();
            this.SkipSpaces();
            this.ConsumeOrThrowException(')');
            this.SkipSpaces();
            this.graph.AddEdge(node, node2, length);

            if (!this.TryConsume(','))
            {
                this.SkipSpaces();
                if (this.stream.Position < this.stream.Length && !this.TryConsume('\n'))
                {
                    throw new IncorrectGraphException();
                }

                return;
            }

            this.SkipSpaces();
        }
    }

    private bool TryConsume(int symbol)
    {
        if (this.curByte == symbol)
        {
            this.curByte = this.stream.ReadByte();
            return true;
        }

        return false;
    }

    private void ConsumeOrThrowException(int symbol)
    {
        if (this.curByte != symbol)
        {
            throw new IncorrectGraphException();
        }

        this.curByte = this.stream.ReadByte();
    }

    private void SkipSpaces()
    {
        while (true)
        {
            switch (this.curByte)
            {
                case ' ':
                case '\t':
                    this.curByte = this.stream.ReadByte();
                    break;
                default:
                    return;
            }
        }
    }

    private int ParseInt()
    {
        if (this.curByte < '0' || this.curByte > '9')
        {
            throw new IncorrectGraphException();
        }

        int ans = this.curByte - '0';
        while (this.stream.Position < this.stream.Length)
        {
            switch (this.stream.ReadByte())
            {
                case int x when x >= '0' && x <= '9':
                    ans = (ans * 10) + x - '0';
                    break;
                case int x:
                    this.curByte = x;
                    return ans;
            }
        }

        this.curByte = -1;
        return ans;
    }
}

/// <summary>
/// Exception for unexpected token.
/// </summary>
[System.Serializable]
public class IncorrectGraphException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public IncorrectGraphException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectGraphException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectGraphException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected IncorrectGraphException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}