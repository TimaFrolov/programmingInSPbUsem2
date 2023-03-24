namespace Hw2Task2;

/// <summary>
/// Exception thrown for incorrect usage of Enumerator.
/// </summary>
[System.Serializable]
public class EnumeratorException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public EnumeratorException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public EnumeratorException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public EnumeratorException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected EnumeratorException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}