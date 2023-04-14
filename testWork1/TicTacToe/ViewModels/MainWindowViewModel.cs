namespace TicTacToe.ViewModels;

using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Game;

/// <summary>
/// View model for main window.
/// </summary>
public class MainWindowViewModel : ReactiveObject
{
    private Game game = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        this.OnButton1Command = ReactiveCommand.Create(() => this.OnButton(1));
        this.OnButton2Command = ReactiveCommand.Create(() => this.OnButton(2));
        this.OnButton3Command = ReactiveCommand.Create(() => this.OnButton(3));
        this.OnButton4Command = ReactiveCommand.Create(() => this.OnButton(4));
        this.OnButton5Command = ReactiveCommand.Create(() => this.OnButton(5));
        this.OnButton6Command = ReactiveCommand.Create(() => this.OnButton(6));
        this.OnButton7Command = ReactiveCommand.Create(() => this.OnButton(7));
        this.OnButton8Command = ReactiveCommand.Create(() => this.OnButton(8));
        this.OnButton9Command = ReactiveCommand.Create(() => this.OnButton(9));

        this.WhenAnyValue(o => o.CurPlayer).Subscribe(o => this.RaisePropertyChanged(nameof(this.TopText)));
    }

    /// <summary>
    /// Gets current player.
    /// </summary>
    /// <returns>Current player.</returns>
    public string CurPlayer => this.GameObjectToString(this.game.CurPlayer);

    /// <summary>
    /// Gets text that should be on top bar.
    /// </summary>
    /// <value>Text to put in top bar.</value>
    public string TopText => this.game.IsFinished ? this.GameWinner : $"Move of {this.CurPlayer}";

    /// <summary>
    /// Gets test that should be in button 1.
    /// </summary>
    /// <returns>Text to put in button 1.</returns>
    public string Button1Text => this.GameObjectToString(this.game.Field[0, 0]);

    /// <summary>
    /// Gets test that should be in button 2.
    /// </summary>
    /// <returns>Text to put in button 2.</returns>
    public string Button2Text => this.GameObjectToString(this.game.Field[0, 1]);

    /// <summary>
    /// Gets test that should be in button 3.
    /// </summary>
    /// <returns>Text to put in button 3.</returns>
    public string Button3Text => this.GameObjectToString(this.game.Field[0, 2]);

    /// <summary>
    /// Gets test that should be in button 4.
    /// </summary>
    /// <returns>Text to put in button 4.</returns>
    public string Button4Text => this.GameObjectToString(this.game.Field[1, 0]);

    /// <summary>
    /// Gets test that should be in button 5.
    /// </summary>
    /// <returns>Text to put in button 5.</returns>
    public string Button5Text => this.GameObjectToString(this.game.Field[1, 1]);

    /// <summary>
    /// Gets test that should be in button 6.
    /// </summary>
    /// <returns>Text to put in button 6.</returns>
    public string Button6Text => this.GameObjectToString(this.game.Field[1, 2]);

    /// <summary>
    /// Gets test that should be in button 7.
    /// </summary>
    /// <returns>Text to put in button 7.</returns>
    public string Button7Text => this.GameObjectToString(this.game.Field[2, 0]);

    /// <summary>
    /// Gets test that should be in button 8.
    /// </summary>
    /// <returns>Text to put in button 8.</returns>
    public string Button8Text => this.GameObjectToString(this.game.Field[2, 1]);

    /// <summary>
    /// Gets test that should be in button 9.
    /// </summary>
    /// <returns>Text to put in button 9.</returns>
    public string Button9Text => this.GameObjectToString(this.game.Field[2, 2]);

    /// <summary>
    /// Gets command should be done on button 1 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton1Command { get; }

    /// <summary>
    /// Gets command should be done on button 2 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton2Command { get; }

    /// <summary>
    /// Gets command should be done on button 3 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton3Command { get; }

    /// <summary>
    /// Gets command should be done on button 4 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton4Command { get; }

    /// <summary>
    /// Gets command should be done on button 5 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton5Command { get; }

    /// <summary>
    /// Gets command should be done on button 6 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton6Command { get; }

    /// <summary>
    /// Gets command should be done on button 7 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton7Command { get; }

    /// <summary>
    /// Gets command should be done on button 8 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton8Command { get; }

    /// <summary>
    /// Gets command should be done on button 9 click.
    /// </summary>
    /// <returns>Command to execute.</returns>
    public ICommand OnButton9Command { get; }

    private string GameWinner => this.game.Winner switch
    {
        GameObject.Empty => "Game resulted in tie!",
        GameObject.Cross => "Winner is X!",
        GameObject.Circle => "Winner is O!",
        _ => throw new ArgumentOutOfRangeException(),
    };

    private string GameObjectToString(GameObject obj)
        => obj switch
        {
            GameObject.Empty => string.Empty,
            GameObject.Cross => "X",
            GameObject.Circle => "O",
            _ => throw new ArgumentOutOfRangeException(nameof(obj), obj, null),
        };

    private void OnButton(int buttonID)
    {
        this.game.MakeMove((buttonID - 1) / 3, (buttonID - 1) % 3);

        switch (buttonID)
        {
            case 1:
                this.RaisePropertyChanged(nameof(this.Button1Text));
                break;
            case 2:
                this.RaisePropertyChanged(nameof(this.Button2Text));
                break;
            case 3:
                this.RaisePropertyChanged(nameof(this.Button3Text));
                break;
            case 4:
                this.RaisePropertyChanged(nameof(this.Button4Text));
                break;
            case 5:
                this.RaisePropertyChanged(nameof(this.Button5Text));
                break;
            case 6:
                this.RaisePropertyChanged(nameof(this.Button6Text));
                break;
            case 7:
                this.RaisePropertyChanged(nameof(this.Button7Text));
                break;
            case 8:
                this.RaisePropertyChanged(nameof(this.Button8Text));
                break;
            case 9:
                this.RaisePropertyChanged(nameof(this.Button9Text));
                break;
        }

        this.RaisePropertyChanged(nameof(this.CurPlayer));
    }
}