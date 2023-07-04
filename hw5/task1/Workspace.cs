namespace hw5;

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
                case "-h":
                case "--help":
                    this.Mode = WorkMode.Help;
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
    }

    /// <summary>
    /// Work mode for application.
    /// </summary>
    public enum WorkMode
    {
        /// <summary>
        /// Mode for processing the task.
        /// </summary>
        Normal,

        /// <summary>
        /// Mode for getting help about application.
        /// </summary>
        Help,
    }

    /// <summary>
    /// Gets path of input file.
    /// </summary>
    public string InFile { get; }

    /// <summary>
    /// Gets mode of workspace.
    /// </summary>
    public WorkMode Mode { get; } = WorkMode.Normal;
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