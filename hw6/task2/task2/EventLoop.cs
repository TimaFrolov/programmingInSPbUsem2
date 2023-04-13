namespace hw6task2;

/// <summary>
/// Class representing console event loop.
/// </summary>
public class EventLoop
{
    /// <summary>
    /// Event triggered when user clicks left button.
    /// </summary>
    public event EventHandler<EventArgs>? OnLeftButton;

    /// <summary>
    /// Event triggered when user clicks right button.
    /// </summary>
    public event EventHandler<EventArgs>? OnRightButton;

    /// <summary>
    /// Event triggered when user clicks upbutton.
    /// </summary>
    public event EventHandler<EventArgs>? OnUpButton;

    /// <summary>
    /// Event triggered when user clicks down button.
    /// </summary>
    public event EventHandler<EventArgs>? OnDownButton;

    /// <summary>
    /// Event triggered when user clicks quit button.
    /// </summary>
    public event EventHandler<EventArgs>? OnQuitButton;

    /// <summary>
    /// Run event loop.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    this.OnUpButton?.Invoke(this, EventArgs.Empty);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    this.OnLeftButton?.Invoke(this, EventArgs.Empty);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    this.OnDownButton?.Invoke(this, EventArgs.Empty);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    this.OnRightButton?.Invoke(this, EventArgs.Empty);
                    break;
                case ConsoleKey.Q:
                    this.OnQuitButton?.Invoke(this, EventArgs.Empty);
                    break;
            }

            if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}