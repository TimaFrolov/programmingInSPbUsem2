namespace hw6task2;

/// <summary>Internal class Program.</summary>
internal class Program
{
    private const string HelpMessage = "Usage: hw6task2 [Map file name]. If no map file given, using default map.";
    private const string ParserExceptionMessage = "Incorrect map file: {0}\nStarting game with empty map!";
    private const string SecurityExceptionMessage = "You have no permissions to open file: {0}\nStarting game with empty map!";
    private const string FileNotFoundExceptionMessage = "Not found input file: {0}\nStarting game with empty map!";
    private const string PathTooLongExceptionMessage = "Given file path too long: {0}\nStarting game with empty map!";
    private const string IncorrectGameMapMessage = ParserExceptionMessage;
    private const int DelayAfterException = 1000;

    /// <summary>Function that called when running application.</summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
#pragma warning disable SA1011 // Disable warning requiring space after closing bracket.
        GameObject[,]? map = null;
#pragma warning restore SA1011
        if (args.Length > 0)
        {
            switch (args[0])
            {
                case "-h":
                case "--help":
                    {
                        Console.WriteLine(HelpMessage);
                        break;
                    }

                default:
                    {
                        FileStream file;
                        try
                        {
                            file = new FileStream(args[0], FileMode.Open);
                        }
                        catch (System.Security.SecurityException ex)
                        {
                            Console.WriteLine(SecurityExceptionMessage, ex.Message);
                            Thread.Sleep(DelayAfterException);
                            break;
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine(FileNotFoundExceptionMessage, ex.Message);
                            Thread.Sleep(DelayAfterException);
                            break;
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            Console.WriteLine(SecurityExceptionMessage, ex.Message);
                            Thread.Sleep(DelayAfterException);
                            break;
                        }
                        catch (DirectoryNotFoundException ex)
                        {
                            Console.WriteLine(FileNotFoundExceptionMessage, ex.Message);
                            Thread.Sleep(DelayAfterException);
                            break;
                        }
                        catch (PathTooLongException ex)
                        {
                            Console.WriteLine(PathTooLongExceptionMessage, ex.Message);
                            Thread.Sleep(DelayAfterException);
                            break;
                        }

                        try
                        {
                            map = Parser.ParseFile(file);
                            file.Close();
                        }
                        catch (ParserException ex)
                        {
                            file.Close();
                            Console.WriteLine(ParserExceptionMessage, ex.Message);
                            Thread.Sleep(DelayAfterException);
                        }

                        break;
                    }
            }
        }

        if (map != null && !Game.ValidateMap(map))
        {
            Console.WriteLine(IncorrectGameMapMessage);
            map = null;
        }

        RunGame(map);
    }

#pragma warning disable SA1011 // Disable warning requiring space after closing bracket.
    private static void RunGame(GameObject[,]? map)
#pragma warning restore SA1011
    {
        Console.CursorVisible = false;
        var printer = new ConsolePrinter();
        var game = map != null ? new Game(printer, map) : new Game(printer);

        EventHandler<System.EventArgs> actionOnExit = (_, _) =>
        {
            Console.CursorLeft = 0;
            Console.CursorTop = game.GetSize().height + 3;
            Console.CursorVisible = true;
        };
        var eventLoop = new EventLoop();
        eventLoop.OnUpButton += game.MovePlayerUp;
        eventLoop.OnLeftButton += game.MovePlayerLeft;
        eventLoop.OnDownButton += game.MovePlayerDown;
        eventLoop.OnRightButton += game.MovePlayerRight;
        eventLoop.OnQuitButton += actionOnExit;
        Console.CancelKeyPress += (sender, args) => actionOnExit(sender, args);
        eventLoop.Run();
    }
}