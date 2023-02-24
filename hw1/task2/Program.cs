namespace Task2
{
    /// <summary>Internal class Program.</summary>
    internal class Program
    {
        /// <summary>Function that called when running application.</summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            switch (args.Length == 0 ? "help" : args[0])
            {
                case "encode":
                    {
                        int indexOfLastChar;
                        Console.WriteLine("Encoded string: \"{0}\"", BWT.Encode(args[1], out indexOfLastChar));
                        Console.WriteLine($"Index of start symbol: {indexOfLastChar}");
                        break;
                    }

                case "decode":
                    {
                        int indexOfLastChar;
                        if (!int.TryParse(args[1], out indexOfLastChar))
                        {
                            Console.WriteLine("Incorrect arguments! Use task2 help (Or dotnet run help)");
                            return;
                        }

                        Console.WriteLine("Decoded string: \"{0}\"", BWT.Decode(args[2], indexOfLastChar));
                        break;
                    }

                case "help":
                    {
                        Console.WriteLine("Use task2 encode <string to encode> (Or dotnet run encode <string to encode>)");
                        Console.WriteLine("For example: task2 encode banana");
                        Console.WriteLine("String should be passed as one command argument:");
                        Console.WriteLine("For example: dotnet run encode \"String with spaces\"");
                        Console.WriteLine("Use task2 decode <index of last character> <string to encode> (Or dotnet run decode <string to encode>)");
                        Console.WriteLine("For example: task2 decode 3 nnbaaa");
                        Console.WriteLine("String should be passed as one command argument:");
                        Console.WriteLine("For example: dotnet run decode 2 \"hgspacntrwiste iS \"");
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Incorrect arguments! Use task2 help (Or dotnet run help)");
                        break;
                    }
            }
        }
    }
}