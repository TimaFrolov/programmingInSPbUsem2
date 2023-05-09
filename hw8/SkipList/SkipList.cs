namespace SkipList;

/// <summary>
/// Represents SkipList <see href="https://en.wikipedia.org/wiki/Skip_list"/> of objects.
/// </summary>
/// <typeparam name="T">Objects to store in list.</typeparam>
public sealed class SkipList<T> : System.Collections.Generic.IList<T>
where T : IComparable<T>
{
    private readonly int maxLayer;

    private HeadNode head;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class that is empty.
    /// </summary>
    /// <param name="layers">Amount of layers in SkipList. Should be positive.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when layers is less than or equal to zero.</exception>
    public SkipList(int layers = 16)
    {
        if (layers <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(layers));
        }

        this.maxLayer = layers - 1;
        this.head = new(layers - 1);
    }

    /// <summary>
    /// Interface for SkipList nodes.
    /// </summary>
    private interface INode
    {
        /// <summary>
        /// Gets next node in list.
        /// </summary>
        /// <value>Next node.</value>
        public ListNode? Next { get; }

        /// <summary>
        /// Adds value to list.
        /// </summary>
        /// <param name="value"> Value to add.</param>
        /// <param name="layersFromThisLayer"> levels to skip from this node layer. </param>
        public void Add(T value, int layersFromThisLayer);

        /// <summary>
        /// Returns true if value is in list after this node.
        /// </summary>
        /// <param name="value">Value to search for.</param>
        /// <returns>True if value is found, false otherwise.</returns>
        public bool Contains(T value);

        /// <summary>
        /// Removes value from list.
        /// </summary>
        /// <param name="value">Value to remove.</param>
        /// <returns>True if value was successfully removed, false otherwise.</returns>
        public bool Remove(T value);
    }

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public int Count { get; private set; }

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public bool IsReadOnly => false;

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    /// <summary>
    /// Gets or sets the element at the specified index.
    /// Set is not supported.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown when trying to set value.</exception>
    public T this[int index]
    {
        get
        {
            HeadNode bottomHead = this.head.GetBottom();
            ListNode? node = bottomHead.Next;
            for (int i = 0; i < index; i++)
            {
                if (node is null)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                node = node.Next;
            }

            return node is null ? throw new ArgumentOutOfRangeException(nameof(index)) : node.Value;
        }
        set => throw new NotSupportedException();
    }

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public int IndexOf(T value)
    {
        HeadNode bottomHead = this.head.GetBottom();
        var index = 0;
        ListNode? node = bottomHead.Next;
        while (node is not null && node.Value.CompareTo(value) < 0)
        {
            node = node.Next;
            index++;
        }

        return node is not null && node.Value.CompareTo(value) == 0 ? index : -1;
    }

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    /// <summary>
    /// Not supported.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown when trying to insert value.</exception>
    public void Insert(int index, T item)
        => throw new NotSupportedException();

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    /// <summary>
    /// Not supported.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown when trying to insert value.</exception>
    public void RemoveAt(int index)
        => throw new NotSupportedException();

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public void Add(T value)
    {
        this.head.Add(value, this.RandomizeLayer());
        this.Count++;
    }

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public void Clear()
    {
        this.head = new(this.maxLayer);
        this.Count = 0;
    }

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public bool Contains(T value)
        => this.head.Contains(value);

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (array.Length - arrayIndex < this.Count)
        {
            throw new ArgumentException("The number of elements in the source SkipList is greater than the available space from arrayIndex to the end of the destination array.");
        }

        HeadNode bottomHead = this.head.GetBottom();
        var index = arrayIndex;
        ListNode? node = bottomHead.Next;
        while (node is not null)
        {
            array[index++] = node.Value;
            node = node.Next;
        }
    }

    /// <inheritdoc cref="IList&lt;T&gt;"/>
    public bool Remove(T value)
    {
        bool ans = this.head.Remove(value);
        if (ans)
        {
            this.Count--;
        }

        return ans;
    }

    /// <inheritdoc cref="IEnumerable&lt;T&gt;"/>
    public SkipListEnumerator GetEnumerator()
        => new(this);

    /// <inheritdoc cref="IEnumerable&lt;T&gt;"/>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => this.GetEnumerator();

    /// <inheritdoc cref="System.Collections.IEnumerable"/>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    private int RandomizeLayer()
        => (int)(new Random().NextSingle() * this.maxLayer) + 1;

    /// <summary>
    /// Iterator over <see cref="SkipList{T}"/>.
    /// </summary>
    public class SkipListEnumerator : IEnumerator<T>
    {
        private readonly SkipList<T> list;
        private INode? current;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipListEnumerator"/> class.
        /// </summary>
        /// <param name="list"> List to iterate through. </param>
        public SkipListEnumerator(SkipList<T> list)
        {
            this.list = list;
            this.current = list.head.GetBottom();
        }

        /// <inheritdoc cref="IEnumerator&lt;T&gt;"/>
        public T Current
            => this.current is ListNode node ? node.Value : throw new InvalidOperationException();

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        object? System.Collections.IEnumerator.Current
            => this.Current;

        /// <inheritdoc cref="IEnumerator&lt;T&gt;"/>
        public void Dispose()
        {
        }

        /// <inheritdoc cref="IEnumerator&lt;T&gt;"/>
        public bool MoveNext()
        {
            if (this.current is null)
            {
                throw new InvalidOperationException();
            }

            this.current = this.current.Next;
            return this.current is not null;
        }

        /// <inheritdoc cref="IEnumerator&lt;T&gt;"/>
        public void Reset()
            => this.current = this.list.head.GetBottom();
    }

    private class HeadNode : INode
    {
        public HeadNode(int maxLevel)
        {
            if (maxLevel > 0)
            {
                this.Down = new(maxLevel - 1);
            }
        }

        public ListNode? Next { get; private set; } = null;

        public HeadNode? Down { get; private set; } = null;

        public HeadNode GetBottom()
            => this.Down is null ? this : this.Down.GetBottom();

        public void Add(T value, int levelsFromTop)
        {
            if (this.Next is not null)
            {
                this.Next.Add(value, levelsFromTop);
                return;
            }

            if (levelsFromTop > 0)
            {
                if (this.Down is null)
                {
                    throw new ArgumentOutOfRangeException(nameof(levelsFromTop));
                }

                this.Down.Add(value, levelsFromTop - 1);
                return;
            }

            if (this.Down is null)
            {
                this.Next = new ListNode(value);
                return;
            }

            this.Down.Add(value, 0);
            this.Next = new ListNode(value, null, this.Down.Next);
        }

        public bool Contains(T value)
        {
            if (this.Next is not null && this.Next.Value.CompareTo(value) <= 0)
            {
                return this.Next.Contains(value);
            }

            return this.Down is not null && this.Down.Contains(value);
        }

        public bool Remove(T value)
            => this.Next is null ? (this.Down is null ? false : this.Down.Remove(value)) : this.Next.Remove(value);
    }

    private class ListNode : INode
    {
        public ListNode(T value, ListNode? next = null, ListNode? down = null)
        {
            this.Value = value;
            this.Next = next;
            this.Down = down;
        }

        public ListNode? Next { get; private set; }

        public ListNode? Down { get; private set; }

        public T Value { get; }

        public ListNode GetBottom()
            => this.Down is null ? this : this.Down.GetBottom();

        public void Add(T value, int levelsFromTop)
        {
            if (this.Next is not null && this.Next.Value.CompareTo(value) < 0)
            {
                this.Next.Add(value, levelsFromTop);
                return;
            }

            if (levelsFromTop > 0)
            {
                if (this.Down is null)
                {
                    throw new ArgumentOutOfRangeException(nameof(levelsFromTop));
                }

                this.Down.Add(value, levelsFromTop - 1);
                return;
            }

            if (this.Down is null)
            {
                this.Next = new ListNode(value, this.Next);
                return;
            }

            this.Down.Add(value, 0);
            this.Next = new ListNode(value, this.Next, this.Down.Next);
        }

        public bool Contains(T value)
        {
            if (this.Value.CompareTo(value) == 0)
            {
                return true;
            }

            if (this.Next is not null && this.Next.Value.CompareTo(value) <= 0)
            {
                return this.Next.Contains(value);
            }

            return this.Down is not null && this.Down.Contains(value);
        }

        public bool Remove(T value)
        {
            if (this.Next is null)
            {
                return this.Down is not null && this.Down.Remove(value);
            }

            if (this.Next.Value.CompareTo(value) == 0)
            {
                this.Next = this.Next.Next;
                if (this.Down is not null)
                {
                    this.Down.Remove(value);
                }

                return true;
            }

            if (this.Next.Value.CompareTo(value) < 0)
            {
                return this.Next.Remove(value);
            }

            return this.Down is not null && this.Down.Remove(value);
        }
    }
}
