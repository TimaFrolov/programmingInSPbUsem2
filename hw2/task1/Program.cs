namespace Hw2Task1
{
    /// <summary>Internal class Program.</summary>
    internal class Program
    {
        private const string AddCommand = "add ";
        private const string AddTrueResponse = "Sucessfully added given string to trie";
        private const string AddFalseResponse = "This string already was in trie";
        private const string ContainsCommand = "contains ";
        private const string ContainsTrueResponse = "Given string is in trie";
        private const string ContainsFalseResponse = "Given string is not in trie";
        private const string RemoveCommand = "remove ";
        private const string RemoveTrueResponse = "Sucessfully removed given string from trie";
        private const string RemoveFalseResponse = "This string was not in trie";
        private const string CountPrefixCommand = "count ";
        private const string CountPrefixResponse = "Trie contains {0} strings with given prefix";
        private const string SizeCommand = "size";
        private const string SizeCommandResponse = "Trie contains {0} strings";
        private const string ExitCommand = "exit";
        private const string ExitCommandResponse = "Exit application!";
        private const string HelpCommand = "help";
        private const string HelpCommandResponse = "To add string to trie use \"" + AddCommand + "<string>\"\n"
                                                 + "To check if trie contains string use \"" + ContainsCommand + "<string>\"\n"
                                                 + "To remove string from trie use \"" + RemoveCommand + "<string>\"\n"
                                                 + "To get how many strings in trie use \"size\"\n"
                                                 + "To check how many strings with prefix in trie use \"" + CountPrefixCommand + "<prefix>\" "
                                                 + "To get this message again use \"" + HelpCommand + "\"\n"
                                                 + "To exit the application use \"" + ExitCommand + "\"";

        private const string IncorrectCommandResponse = "Incorrect command! Use \"" + HelpCommand + "\" to get help";
        private const string GreetingsString = "Welcome to console interface of trie (If you don't know what it is, check out http://neerc.ifmo.ru/wiki/index.php?title=Бор).\n"
                                             + "To interact with trie you can use these commands:\n" + HelpCommandResponse;

        private const string NullResponse = "Error reading stdin";

        /// <summary>Function that called when running application.</summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine(GreetingsString);
            var trie = new Trie();
            while (true)
            {
                Console.Write("> ");
                string? command = Console.ReadLine();
                Console.WriteLine(
                    command switch
                    {
                        null => NullResponse,
                        string str when str.StartsWith(AddCommand)
                            => trie.Add(str.Substring(AddCommand.Length)) ? AddTrueResponse : AddFalseResponse,
                        string str when str.StartsWith(ContainsCommand)
                            => trie.Contains(str.Substring(ContainsCommand.Length)) ? ContainsTrueResponse : ContainsFalseResponse,
                        string str when str.StartsWith(RemoveCommand)
                            => trie.Remove(str.Substring(RemoveCommand.Length)) ? RemoveTrueResponse : RemoveFalseResponse,
                        string str when str.StartsWith(CountPrefixCommand)
                            => string.Format(CountPrefixResponse, trie.HowManyStartsWithPrefix(str.Substring(CountPrefixCommand.Length))),
                        SizeCommand => string.Format(SizeCommandResponse, trie.Size),
                        ExitCommand => ExitCommandResponse,
                        HelpCommand => HelpCommandResponse,
                        _ => IncorrectCommandResponse
                    });
                if (command is null || command.Equals(ExitCommand))
                {
                    break;
                }
            }
        }
    }
}