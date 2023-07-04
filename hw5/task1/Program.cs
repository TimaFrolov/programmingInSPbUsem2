namespace hw5;

/// <summary>Internal class Program.</summary>
internal class Program
{
    private const string IncorrectWorkspaceMessage = "Given command arguments are incorrect: {0}\n"
    + "Use \"task1 --help\" to get help";

    private const string IncorrectGraphExceptionResponse = "Given file contains incorrect graph!";
    private const string SecurityExceptionMessage = "You have no permissions to open file: {0}\n";
    private const string FileNotFoundExceptionMessage = "Not found input file: {0}\n";
    private const string PathTooLongExceptionMessage = "Given file path too long: {0}\n";
    private const string HelpMessage = "Use \"task1 filename\" to build longest graph from file";
    private const string SuccessMessage = "Nodes in longest tree of given graph: {0}";

    /// <summary>Function that called when running application.</summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
        Workspace ws;
        try
        {
            ws = new Workspace(args);
        }
        catch (WorkspaceException ex)
        {
            Console.WriteLine(IncorrectWorkspaceMessage, ex.Message);
            return;
        }

        switch (ws.Mode)
        {
            case Workspace.WorkMode.Normal:
                {
                    FileStream file;
                    try
                    {
                        file = new FileStream(ws.InFile, FileMode.Open);
                    }
                    catch (System.Security.SecurityException ex)
                    {
                        Console.WriteLine(SecurityExceptionMessage, ex.Message);
                        break;
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine(FileNotFoundExceptionMessage, ex.Message);
                        break;
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine(SecurityExceptionMessage, ex.Message);
                        break;
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Console.WriteLine(FileNotFoundExceptionMessage, ex.Message);
                        break;
                    }
                    catch (PathTooLongException ex)
                    {
                        Console.WriteLine(PathTooLongExceptionMessage, ex.Message);
                        break;
                    }

                    Graph<int> tree;
                    try
                    {
                        tree = new Parser(file).ParseFile().LongestTree();
                    }
                    catch (IncorrectGraphException)
                    {
                        Console.WriteLine(IncorrectGraphExceptionResponse);
                        break;
                    }
                    finally
                    {
                        file.Close();
                    }

                    Console.WriteLine(SuccessMessage, tree.ToString());
                    break;
                }

            case Workspace.WorkMode.Help:
                {
                    Console.WriteLine(HelpMessage);
                    break;
                }
        }
    }
}