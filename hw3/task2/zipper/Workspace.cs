namespace Zipper;

/// <summary>
/// Class with workspace initialization.
/// </summary>
public class Workspace
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Workspace"/> class.
    /// </summary>
    /// <exception cref="WorkspaceException">Thrown when it is not possible to create correct workspace from given arguments.</exception>
    /// <param name="args">Command line arguments.</param>
    public Workspace(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            switch (arg)
            {
                // TODO: Exceptions
                case "-c":
                    this.Mode = this.Mode == WorkMode.Undefined ? WorkMode.Encode : throw new WorkspaceException("Two work modes specified");
                    break;
                case "-u":
                    this.Mode = this.Mode == WorkMode.Undefined ? WorkMode.Decode : throw new WorkspaceException("Two work modes specified");
                    break;
                case "-h":
                case "--help":
                    this.Mode = this.Mode == WorkMode.Undefined ? WorkMode.Help : throw new WorkspaceException("Two work modes specified");
                    break;
                case "-o":
                    this.OutFile = i + 1 < args.Length ? args[++i] : throw new WorkspaceException("\"-o\" argument has to be followed by output file name");
                    break;
                default:
                    this.InFile = arg;
                    break;
            }
        }

        if (this.InFile is null)
        {
            if (this.Mode != WorkMode.Help)
            {
                throw new WorkspaceException("No input filename was given");
            }

            this.InFile = string.Empty;
        }

        if (this.OutFile is null)
        {
            this.OutFile = this.Mode switch
            {
                WorkMode.Encode => this.InFile + ".zipped",
                WorkMode.Decode => this.InFile.Substring(this.InFile.Length - 7) == ".zipped"
                    ? this.InFile.Substring(0, this.InFile.Length - 7)
                    : throw new WorkspaceException("Not found output file name with input file name that doesn't end with .zipped"),
                _ => string.Empty
            };
        }
    }

    /// <summary>
    /// Work mode for application.
    /// </summary>
    public enum WorkMode
    {
        /// <summary>
        /// Mode for encoding <see cref="Workspace.InFile"/> into out file.
        /// </summary>
        Encode,

        /// <summary>
        /// Mode for decoding <see cref="Workspace.InFile"/> into <see cref="Workspace.OutFile"/>.
        /// </summary>
        Decode,

        /// <summary>
        /// Mode for getting help about application.
        /// </summary>
        Help,

        /// <summary>
        /// Value if cannot determine mode from given arguments.
        /// </summary>
        Undefined,
    }

    /// <summary>
    /// Gets path of input file.
    /// </summary>
    public string InFile { get; }

    /// <summary>
    /// Gets path of output file.
    /// </summary>
    public string OutFile { get; }

    /// <summary>
    /// Gets mode of workspace.
    /// </summary>
    public WorkMode Mode { get; } = WorkMode.Undefined;
}

/// <summary>
/// Exception for incorrect workspace.
/// </summary>
[System.Serializable]
public class WorkspaceException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public WorkspaceException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public WorkspaceException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public WorkspaceException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected WorkspaceException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}