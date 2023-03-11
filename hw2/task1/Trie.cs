namespace Hw2Task1
{
    /// <summary>Represetnts a collection of strings.</summary>
    public class Trie
    {
        private Dictionary<char, Trie> next;

        private bool isTerminal;

        /// <summary>Initializes a new instance of the <see cref="Trie"/> class that contains no strings in it.</summary>
        public Trie()
        {
            this.next = new Dictionary<char, Trie>();
            this.isTerminal = false;
            this.Size = 0;
        }

        /// <summary>Initializes a new instance of the <see cref="Trie"/> class that contains strings from data.</summary>
        /// <param name="data">Strings to insert into trie.</param>
        public Trie(IEnumerable<string> data)
        : this()
        {
            foreach (string element in data)
            {
                this.Add(element);
            }
        }

        /// <summary>Gets amount of strings contained in trie.</summary>
        public int Size { get; private set; }

        /// <summary>Add new string into trie.</summary>
        /// <param name="element">String to add into trie.</param>
        /// <returns>false if given string was already stored in trie, otherwise true.</returns>
        public bool Add(string element)
        {
            return this.Add(element, 0);
        }

        /// <summary>Check if trie contains string.</summary>
        /// <param name="element">String to locate in trie.</param>
        /// <returns>true if trie contains given string, otherwise false.</returns>
        public bool Contains(string element)
        {
            return this.Contains(element, 0);
        }

        /// <summary>Remove string from trie.</summary>
        /// <param name="element">String to remove from trie.</param>
        /// <returns>true if given string was stored in trie, otherwise false.</returns>
        public bool Remove(string element)
        {
            return this.Remove(element, 0);
        }

        /// <summary>Count how many strings in trie with given prefix.</summary>
        /// <param name="prefix">Prefix to use.</param>
        /// <returns>Amount of strings starting with given prefix contained in trie.</returns>
        public int HowManyStartsWithPrefix(string prefix)
        {
            return this.HowManyStartsWithPrefix(prefix, 0);
        }

        private bool Add(string element, int curIndex)
        {
            if (curIndex == element.Length)
            {
                bool wasTerminal = this.isTerminal;
                this.isTerminal = true;
                if (!wasTerminal)
                {
                    this.Size += 1;
                }

                return !wasTerminal;
            }

            char curChar = element[curIndex];

            if (!this.next.ContainsKey(curChar))
            {
                this.next[curChar] = new Trie();
            }

            bool wasAdded = this.next[curChar].Add(element, curIndex + 1);
            if (wasAdded)
            {
                this.Size += 1;
            }

            return wasAdded;
        }

        private bool Contains(string element, int curIndex)
        {
            if (curIndex == element.Length)
            {
                return this.isTerminal;
            }

            char curChar = element[curIndex];
            return this.next.ContainsKey(curChar)
                ? this.next[curChar].Contains(element, curIndex + 1)
                : false;
        }

        private bool Remove(string element, int curIndex)
        {
            if (curIndex == element.Length)
            {
                bool wasTerminal = this.isTerminal;
                this.isTerminal = false;
                return wasTerminal;
            }

            char curChar = element[curIndex];

            if (!this.next.ContainsKey(curChar))
            {
                return false;
            }

            Trie nextTrie = this.next[curChar];

            if (!nextTrie.Remove(element, curIndex + 1))
            {
                return false;
            }

            if (!nextTrie.isTerminal && nextTrie.next.Count == 0)
            {
                this.next.Remove(curChar);
            }

            this.Size -= 1;

            return true;
        }

        private int HowManyStartsWithPrefix(string prefix, int curIndex)
        {
            if (curIndex == prefix.Length)
            {
                return this.Size;
            }

            char curChar = prefix[curIndex];
            return this.next.ContainsKey(curChar)
                ? this.next[curChar].HowManyStartsWithPrefix(prefix, curIndex + 1)
                : 0;
        }
    }
}