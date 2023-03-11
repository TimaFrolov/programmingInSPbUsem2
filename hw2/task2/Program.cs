namespace Hw2Task2
{
    /// <summary>Internal class Program.</summary>
    internal class Program
    {
        /// <summary>Function that called when running application.</summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            if (args[0] == "help")
            {
                Console.WriteLine("Use task2 <expression> (Or dotnet run <expression>) to evaluate expression.");
                Console.WriteLine("For example: task2 1 2 + 4 /");
                Console.WriteLine("<expression> has to be valid expression writen in reverse polish form, containing only natural numbers and symbols \"+\", \"-\", \"*\", \"/\"");
                return;
            }

            IEnumerable<StackElement> parsedArgs;
            try
            {
                parsedArgs = args.Select<string, StackElement>(str => str switch
                {
                    "+" => new StackElement.Binop(BinopType.Add),
                    "-" => new StackElement.Binop(BinopType.Sub),
                    "*" => new StackElement.Binop(BinopType.Mul),
                    "/" => new StackElement.Binop(BinopType.Div),
                    _ => new StackElement.Number(int.Parse(str))
                });
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect arguments!");
                return;
            }

            var listStack = new ListStack<StackElement>(parsedArgs);
            var arrayStack = new ArrayStack<StackElement>(parsedArgs);
            try
            {
                Console.WriteLine($"Evaluation result using stack on lists: {StackCalculator.Evaluate(listStack)}");
                Console.WriteLine($"Evaluation result using stack on arrays: {StackCalculator.Evaluate(arrayStack)}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division by zero occured in calculations");
            }
        }
    }
}