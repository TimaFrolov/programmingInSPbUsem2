namespace hw6task2;

/// <summary>
/// Interface for printing game map.
/// </summary>
public interface IPrinter
{
    /// <summary>
    /// Gets height of buffer.
    /// </summary>
    /// <value>Height of buffer.</value>
    public int BufferHeight { get; }

    /// <summary>
    /// Gets width of buffer.
    /// </summary>
    /// <value>Width of buffer.</value>
    public int BufferWidth { get; }

    /// <summary>
    /// Write string to output.
    /// </summary>
    /// <param name="str">String to write.</param>
    public void Write(string str);

    /// <summary>
    /// Move cursor in output to given position.
    /// </summary>
    /// <param name="pos">Position to move cursor to.</param>
    public void MoveCursor((int cursorLeft, int cursorTop) pos);
}

/// <summary>
/// Implementation of <see cref="IPrinter"/> for printing to console.
/// </summary>
public class ConsolePrinter : IPrinter
{
    /// <inheritdoc cref="IPrinter.BufferHeight"/>
    public int BufferHeight => Console.BufferHeight;

    /// <inheritdoc cref="IPrinter.BufferWidth"/>
    public int BufferWidth => Console.BufferWidth;

    /// <inheritdoc cref="IPrinter"/>
    public void Write(string str) => Console.Write(str);

    /// <inheritdoc cref="IPrinter"/>
    public void MoveCursor((int cursorLeft, int cursorTop) pos)
    {
        Console.CursorLeft = pos.cursorLeft;
        Console.CursorTop = pos.cursorTop;
    }
}