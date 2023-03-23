namespace Zipper;

/// <summary>Represetnts a collection of string keys and TValue values.</summary>
/// <typeparam name="TValue">Type of values to associate with keys.</typeparam>
public class Trie<TValue>
{
    private static Value.None none = new Value.None();

    private Dictionary<char, Trie<TValue>> next;

    private Value value;

    /// <summary>Initializes a new instance of the <see cref="Trie&lt;TValue>"/> class that contains no strings in it.</summary>
    public Trie()
    {
        this.next = new Dictionary<char, Trie<TValue>>();
        this.value = none;
        this.Size = 0;
    }

    /// <summary>Initializes a new instance of the <see cref="Trie&lt;TValue>"/> class that contains strings from given collection.</summary>
    /// <param name="data">Collection of key-value pairs to put into trie.</param>
    public Trie(IEnumerable<KeyValuePair<string, TValue>> data)
    : this()
    {
        foreach (var (key, value) in data)
        {
            this[key] = value;
        }
    }

    /// <summary>Gets amount of strings contained in trie.</summary>
    public int Size { get; private set; }

    /// <summary>Get or set value associated with given key.</summary>
    /// <param name="key">Key of the value to get or set.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no value associated with given key.</exception>
    /// <returns>Value associated with given key.</returns>
    public TValue this[string key]
    {
        get => this.Get(key);
        set => this.Add(key, value);
    }

    /// <summary>Add new key, value pair into trie.</summary>
    /// <param name="key">String to use as key.</param>
    /// <param name="value">Value to associate with key.</param>
    /// <returns>false if given key was already stored in trie, otherwise true.</returns>
    public bool Add(string key, TValue value)
        => this.Set(key, value, 0);

    /// <summary>Check if trie contains string.</summary>
    /// <param name="key">String to locate in trie.</param>
    /// <returns>true if trie contains given string, otherwise false.</returns>
    public bool Contains(string key)
        => this.Contains(key, 0);

    /// <summary>Get value associated with key.</summary>
    /// <param name="key">Key to find value for.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no value associated with given key.</exception>
    /// <returns>Value associated with given key.</returns>
    public TValue Get(string key)
        => this.Get(key, 0);

    /// <summary>Remove string from trie.</summary>
    /// <param name="key">String to remove from trie.</param>
    /// <returns>true if given string was stored in trie, otherwise false.</returns>
    public bool Remove(string key)
        => this.Remove(key, 0);

    /// <summary>Count how many strings in trie with given prefix.</summary>
    /// <param name="prefix">Prefix to use.</param>
    /// <returns>Amount of strings starting with given prefix contained in trie.</returns>
    public int HowManyStartsWithPrefix(string prefix)
    {
        return this.HowManyStartsWithPrefix(prefix, 0);
    }

    private bool Set(string key, TValue value, int curIndex)
    {
        if (curIndex == key.Length)
        {
            bool wasTerminal = this.value != none;
            this.value = new Value.Some(value);
            if (!wasTerminal)
            {
                this.Size += 1;
            }

            return !wasTerminal;
        }

        char curChar = key[curIndex];

        if (!this.next.ContainsKey(curChar))
        {
            this.next[curChar] = new Trie<TValue>();
        }

        bool wasAdded = this.next[curChar].Set(key, value, curIndex + 1);
        if (wasAdded)
        {
            this.Size += 1;
        }

        return wasAdded;
    }

    private bool Contains(string key, int curIndex)
    {
        if (curIndex == key.Length)
        {
            return this.value != none;
        }

        char curChar = key[curIndex];
        return this.next.ContainsKey(curChar)
            ? this.next[curChar].Contains(key, curIndex + 1)
            : false;
    }

    private TValue Get(string key, int curIndex)
    {
        if (curIndex == key.Length)
        {
            return this.value switch
            {
                Value.Some x => x.value,
                _ => throw new KeyNotFoundException()
            };
        }

        char curChar = key[curIndex];
        return this.next.ContainsKey(curChar)
            ? this.next[curChar].Get(key, curIndex + 1)
            : throw new KeyNotFoundException();
    }

    private bool Remove(string key, int curIndex)
    {
        if (curIndex == key.Length)
        {
            bool wasTerminal = this.value != none;
            this.value = none;
            return wasTerminal;
        }

        char curChar = key[curIndex];

        if (!this.next.ContainsKey(curChar))
        {
            return false;
        }

        Trie<TValue> nextTrie = this.next[curChar];

        if (!nextTrie.Remove(key, curIndex + 1))
        {
            return false;
        }

        if (nextTrie.Size == 0)
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

    private record Value
    {
        public record None()
        : Value();

        public record Some(TValue value)
        : Value();

        private Value()
        {
        }
    }
}
