namespace hw6task2;

using System.Text;

/// <summary>
/// Class representing game.
/// </summary>
public class Game
{
    private const char PlayerSymbol = '@';
    private const char WallSymbol = '#';
    private const char EmptySymbol = ' ';

    private static readonly GameObject[,] DefaultMap = new GameObject[,]
        {
            {
                GameObject.Empty, GameObject.Empty, GameObject.Empty, GameObject.Empty, GameObject.Empty, GameObject.Empty, GameObject.Empty,
            },
            {
                GameObject.Empty, GameObject.Wall, GameObject.Empty, GameObject.Empty, GameObject.Wall, GameObject.Empty, GameObject.Wall,
            },
            {
                GameObject.Empty, GameObject.Wall, GameObject.Wall, GameObject.Empty, GameObject.Empty, GameObject.Empty, GameObject.Wall,
            },
            {
                GameObject.Empty, GameObject.Empty, GameObject.Empty, GameObject.Wall, GameObject.Empty, GameObject.Empty, GameObject.Wall,
            },
            {
                GameObject.Empty, GameObject.Wall, GameObject.Empty, GameObject.Player, GameObject.Empty, GameObject.Empty, GameObject.Empty,
            },
        };

    private IPrinter printer;
    private GameObject[,] map;
    private (int x, int y) playerPos;

    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class printing to IPrinter, with given map.
    /// </summary>
    /// <param name="printer">Object to print game state to.</param>
    /// <param name="map">Map to use in game.</param>
    /// <exception cref="IncorrectGameMapException">Thrown when given map is invalid.</exception>
    public Game(IPrinter printer, GameObject[,] map)
    {
        this.printer = printer;
        this.map = map;
        this.playerPos = this.FindPlayer();
        this.PrintMap();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Game"/> class printing to IPrinter, with default map.
    /// </summary>
    /// <param name="printer">Object to print game state to.</param>
    /// <exception cref="IncorrectGameMapException">Thrown when given map is invalid.</exception>
    public Game(IPrinter printer)
    : this(printer, DefaultMap)
    {
    }

    /// <summary>
    /// Check if given array is correct map.
    /// </summary>
    /// <param name="map">Array to check.</param>
    /// <returns>true if given array is correct map, false otherwise.</returns>
    public static bool ValidateMap(GameObject[,] map)
    {
        bool foundPlayer = false;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == GameObject.Player)
                {
                    if (foundPlayer)
                    {
                        return false;
                    }

                    foundPlayer = true;
                }
            }
        }

        return foundPlayer;
    }

    /// <summary>
    /// Function to move player up 1 tile.
    /// </summary>
    /// <param name="sender">Object who sent event.</param>
    /// <param name="args">Should be empty.</param>
    public void MovePlayerUp(object? sender, EventArgs args)
        => this.MovePlayer((this.playerPos.x, this.playerPos.y - 1));

    /// <summary>
    /// Function to move player down 1 tile.
    /// </summary>
    /// <param name="sender">Object who sent event.</param>
    /// <param name="args">Should be empty.</param>
    public void MovePlayerDown(object? sender, EventArgs args)
        => this.MovePlayer((this.playerPos.x, this.playerPos.y + 1));

    /// <summary>
    /// Function to move player left 1 tile.
    /// </summary>
    /// <param name="sender">Object who sent event.</param>
    /// <param name="args">Should be empty.</param>
    public void MovePlayerLeft(object? sender, EventArgs args)
        => this.MovePlayer((this.playerPos.x - 1, this.playerPos.y));

    /// <summary>
    /// Function to move player left 1 tile.
    /// </summary>
    /// <param name="sender">Object who sent event.</param>
    /// <param name="args">Should be empty.</param>
    public void MovePlayerRight(object? sender, EventArgs args)
        => this.MovePlayer((this.playerPos.x + 1, this.playerPos.y));

    /// <summary>
    /// Get size of game map.
    /// </summary>
    /// <returns>Height and width of map.</returns>
    public (int height, int width) GetSize()
        => (this.map.GetLength(0), this.map.GetLength(1));

    private void PrintMap()
    {
        var str = new StringBuilder();
        str.Append("╔");
        for (int i = 0; i < this.map.GetLength(1); i++)
        {
            str.Append("═");
        }

        str.Append("╗");
        str.Append("\n");
        for (int i = 0; i < this.map.GetLength(0); i++)
        {
            str.Append("║");
            for (int j = 0; j < this.map.GetLength(1); j++)
            {
                switch (this.map[i, j])
                {
                    case GameObject.Player:
                        str.Append(PlayerSymbol);
                        break;
                    case GameObject.Wall:
                        str.Append(WallSymbol);
                        break;
                    case GameObject.Empty:
                        str.Append(EmptySymbol);
                        break;
                }
            }

            str.Append("║");
            str.Append("\n");
        }

        str.Append("╚");
        for (int i = 0; i < this.map.GetLength(1); i++)
        {
            str.Append("═");
        }

        str.Append("╝");
        str.Append("\n");
        str.Append("WASD or arrows to move, Q to quit");
        for (int i = 0; i < this.printer.BufferHeight - this.map.GetLength(0) - 3; i++)
        {
            str.Append("\n");
        }

        this.printer.Write(str.ToString());
    }

    private (int x, int y) FindPlayer()
    {
        (int, int) ans = (-1, -1);
        for (int i = 0; i < this.map.GetLength(0); i++)
        {
            for (int j = 0; j < this.map.GetLength(1); j++)
            {
                if (this.map[i, j] == GameObject.Player)
                {
                    ans = (ans == (-1, -1)) ? (j, i) : throw new IncorrectGameMapException("Given map contains two players");
                }
            }
        }

        return (ans != (-1, -1)) ? ans : throw new IncorrectGameMapException("Given map contains no player");
    }

    private void RePrintChar((int x, int y) pos, char c)
    {
        this.printer.MoveCursor((pos.x + 1, pos.y + 1));
        this.printer.Write(c.ToString());
    }

    private void MovePlayer((int x, int y) pos)
    {
        if (pos.x < 0 || pos.x >= this.map.GetLength(1) || pos.y < 0 || pos.y >= this.map.GetLength(0))
        {
            return;
        }

        if (this.map[pos.y, pos.x] == GameObject.Wall)
        {
            return;
        }

        this.map[this.playerPos.y, this.playerPos.x] = GameObject.Empty;
        this.RePrintChar(this.playerPos, EmptySymbol);
        this.playerPos = pos;
        this.map[this.playerPos.y, this.playerPos.x] = GameObject.Player;
        this.RePrintChar(this.playerPos, PlayerSymbol);
    }
}

/// <summary>
/// Object in game map. Default is <see cref="GameObject.Empty"/>.
/// </summary>
public enum GameObject
{
    /// <summary>
    /// Object representing nothing.
    /// </summary>
    Empty = 0,

    /// <summary>
    /// Object representing player.
    /// </summary>
    Player,

    /// <summary>
    /// Object representing wall.
    /// </summary>
    Wall,
}

/// <summary>
/// Exception for incorrect map.
/// </summary>
[System.Serializable]
public class IncorrectGameMapException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public IncorrectGameMapException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectGameMapException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public IncorrectGameMapException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected IncorrectGameMapException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}