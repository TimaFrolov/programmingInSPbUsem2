namespace TicTacToe.Game;

/// <summary>
/// Class representing Tic Tac Toe game.
/// </summary>
public class Game
{
    /// <summary>
    /// Gets current state of game field.
    /// </summary>
    /// <value>Game field.</value>
    public GameObject[,] Field { get; private set; } = new GameObject[3, 3];

    /// <summary>
    /// Gets current player to make move.
    /// </summary>
    /// <value>Player to make move.</value>
    public GameObject CurPlayer { get; private set; } = GameObject.Cross;

    /// <summary>
    /// Gets winner of the game when it is finished. If it is not, equals to <see cref="GameObject.Empty"/>.
    /// If true, and Winner is still <see cref="GameObject.Empty"/>, then it's a draw.
    /// </summary>
    /// <value>Game winner.</value>
    public GameObject Winner { get; private set; }

    /// <summary>
    /// Gets a value indicating whether game is finished or not.
    /// </summary>
    /// <value>true if game is finished, false otherwise.</value>
    public bool IsFinished { get; private set; }

    /// <summary>
    /// Emulate player making move at given position.
    /// If game is finished or field on given position is not empty, does nothing.
    /// Changes CurPlayer state.
    /// </summary>
    /// <param name="x">X coordinates of field to make move.</param>
    /// <param name="y">Y coordinates of field to make move.</param>
    public void MakeMove(int x, int y)
    {
        if (this.IsFinished || this.Field[x, y] != GameObject.Empty)
        {
            return;
        }

        this.Field[x, y] = this.CurPlayer;
        this.CurPlayer = this.CurPlayer == GameObject.Cross ? GameObject.Circle : GameObject.Cross;
        this.UpdateWinner();
        this.UpdateFinishedStatus();
    }

    private void UpdateWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if (this.Field[i, 0] != GameObject.Empty && this.Field[i, 0] == this.Field[i, 1] && this.Field[i, 1] == this.Field[i, 2])
            {
                this.Winner = this.Field[i, 0];
                return;
            }

            if (this.Field[0, i] != GameObject.Empty && this.Field[0, i] == this.Field[1, i] && this.Field[1, i] == this.Field[2, i])
            {
                this.Winner = this.Field[0, i];
                return;
            }
        }

        if (this.Field[0, 0] != GameObject.Empty && this.Field[0, 0] == this.Field[1, 1] && this.Field[1, 1] == this.Field[2, 2])
        {
            this.Winner = this.Field[0, 0];
            return;
        }

        if (this.Field[0, 2] != GameObject.Empty && this.Field[0, 2] == this.Field[1, 1] && this.Field[1, 1] == this.Field[2, 0])
        {
            this.Winner = this.Field[0, 2];
            return;
        }
    }

    private void UpdateFinishedStatus()
    {
        if (this.Winner != GameObject.Empty)
        {
            this.IsFinished = true;
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (this.Field[i, j] == GameObject.Empty)
                {
                    return;
                }
            }
        }

        this.IsFinished = true;
    }
}

/// <summary>
/// Enum representing game object.
/// </summary>
public enum GameObject
{
    /// <summary>Tile is empty.</summary>
    Empty = 0,

    /// <summary>Tile is cross.</summary>
    Cross,

    /// <summary>Tile is circle.</summary>
    Circle,
}