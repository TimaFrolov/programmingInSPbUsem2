namespace Zipper;

/// <summary>Internal class Program.</summary>
internal class Program
{
    private const string IncorrectWorkspaceMessage = "Given command arguments are incorrect: {0}\n"
    + "Use \"zipper help\" to get help";

    private const string SecurityExceptionMessage = "You have no permissions to open file: {0}\n";
    private const string FileNotFoundExceptionMessage = "Not found input file: {0}\n";
    private const string PathTooLongExceptionMessage = "Given file path too long: {0}\n";
    private const string EncodeSuccessMessage = "File succesfully encoded! Output file: {0}. Compression ratio: {1}";
    private const string DecodeSuccessMessage = "File succesfully decoded! Output file: {0}";

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

        FileStream inputFile;
        FileStream outputFile;
        if (ws.Mode == Workspace.WorkMode.Encode || ws.Mode == Workspace.WorkMode.Decode)
        {
            try
            {
                inputFile = new FileStream(ws.InFile, FileMode.Open);
                outputFile = new FileStream(ws.OutFile, FileMode.OpenOrCreate);
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine(SecurityExceptionMessage, ex.Message);
                return;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(FileNotFoundExceptionMessage, ex.Message);
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(SecurityExceptionMessage, ex.Message);
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(FileNotFoundExceptionMessage, ex.Message);
                return;
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine(PathTooLongExceptionMessage, ex.Message);
                return;
            }

            switch (ws.Mode)
            {
                case Workspace.WorkMode.Encode:
                    {
                        var input = new BitReader(inputFile);
                        var output = new BitWriter(outputFile);
                        LZW.Encode(input, output);
                        Console.WriteLine(EncodeSuccessMessage, ws.OutFile, (float)inputFile.Length / outputFile.Length);
                        input.Close();
                        output.Close();
                        break;
                    }

                case Workspace.WorkMode.Decode:
                    {
                        var input = new BitReader(inputFile);
                        var output = new BitWriter(outputFile);
                        LZW.Decode(input, output);
                        input.Close();
                        output.Close();
                        Console.WriteLine(DecodeSuccessMessage, ws.OutFile);
                        break;
                    }
            }
        }

        switch (ws.Mode)
        {
            case Workspace.WorkMode.Help:
                // TODO: Help message
                break;
            default:
                // TODO: Error message
                break;
        }
    }
}