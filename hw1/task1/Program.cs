namespace Task1
{
    /// <summary>Internal class Program.</summary>
    internal class Program
    {
        /// <summary>Function that called when running application.</summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "help")
            {
                Console.WriteLine("Pass array of integers as command arguments (one number per argument) to sort it");
                Console.WriteLine("For example: task1 3 4 1 5 6");
                Console.WriteLine("Or: dotnet run 10 9 8 7 6 ");
                return;
            }

            int[] arr = new int[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (!int.TryParse(args[i], out arr[i]))
                {
                    Console.WriteLine("Incorrect arguments! Use task1 help (Or dotnet run help)");
                    return;
                }
            }

            SelectionSort(arr);
            Console.WriteLine("Sorted array: {0}", string.Join(" ", arr));
        }

        private static void SelectionSort(int[] arr)
        {
            for (var i = 0; i < arr.Length - 1; i++)
            {
                int indexOfMinValue = i;
                for (var j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[indexOfMinValue])
                    {
                        indexOfMinValue = j;
                    }
                }

                int minValue = arr[indexOfMinValue];
                arr[indexOfMinValue] = arr[i];
                arr[i] = minValue;
            }
        }
    }
}
