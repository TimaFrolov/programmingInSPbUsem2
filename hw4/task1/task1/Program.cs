namespace hw4task1;

/// <summary>Internal class Program.</summary>
internal class Program
{
    private const string IncorrectWorkspaceMessage = "Given command arguments are incorrect: {0}\n"
    + "Use \"zipper help\" to get help";
    private const string IncorrectExpressionExceptionResponse = "Given file contains incorrect expression!";
    private const string UnexpectedTokenExceptionResponse = IncorrectExpressionExceptionResponse;
    private const string SecurityExceptionMessage = "You have no permissions to open file: {0}\n";
    private const string FileNotFoundExceptionMessage = "Not found input file: {0}\n";
    private const string PathTooLongExceptionMessage = "Given file path too long: {0}\n";
    private const string EncodeSuccessMessage = "File succesfully encoded! Output file: {0}. Compression ratio: {1:N2}";
    private const string DecodeSuccessMessage = "File succesfully decoded! Output file: {0}";
    private const string HelpMessage = "Use \"task1 filename\" to evaluate expression from file";
    private const string SuccessMessage = "Expression constructed from file: {0}\nEvaluated value of expression: {1}";

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

                    ExpressionTree tree;
                    try
                    {
                        tree = Builder.BuildExpressionTree(Parser.ParseExpression(file));
                    }
                    catch (UnexpectedTokenException)
                    {
                        Console.WriteLine(UnexpectedTokenExceptionResponse);
                        break;
                    }
                    catch (IncorrectExpressionException)
                    {
                        Console.WriteLine(IncorrectExpressionExceptionResponse);
                        break;
                    }
                    finally
                    {
                        file.Close();
                    }
                    Console.WriteLine(SuccessMessage, tree, tree.Evaluate());
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