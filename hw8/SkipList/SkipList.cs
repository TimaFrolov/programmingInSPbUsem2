namespace SkipList;

public sealed class SkipList<T> : System.Collections.Generic.IList<T>
where T : IComparable<T>
{
    private readonly int maxLevel;

    private HeadNode head;

    public SkipList(int maxLevel = 15)
    {
        if (maxLevel < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLevel));
        }

        this.maxLevel = maxLevel;
        this.head = new(maxLevel);
    }

    private interface INode
    {
        public ListNode? Next { get; }

        public void Add(T value, int levelsFromTop);

        public bool Contains(T value);

        public bool Remove(T value);
    }

    public int Count { get; private set; }

    public bool IsReadOnly => false;

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

    public void Insert(int index, T item)
        => throw new NotSupportedException();

    public void RemoveAt(int index)
        => throw new NotSupportedException();

    public void Add(T value)
    {
        this.head.Add(value, this.RandomizeLayer());
        this.Count++;
    }

    public void Clear()
    {
        this.head = new(this.maxLevel);
        this.Count = 0;
    }

    public bool Contains(T value)
        => this.head.Contains(value);

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

    public bool Remove(T value)
    {
        bool ans = this.head.Remove(value);
        if (ans)
        {
            this.Count--;
        }

        return ans;
    }

    public SkipListEnumerator GetEnumerator()
        => new(this);

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => this.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    private int RandomizeLayer()
        => (int)(new Random().NextSingle() * this.maxLevel) + 1;

    public class SkipListEnumerator : IEnumerator<T>
    {
        private readonly SkipList<T> list;
        private INode? current;

        public SkipListEnumerator(SkipList<T> list)
        {
            this.list = list;
            this.current = list.head.GetBottom();
        }

        public T Current
            => this.current is ListNode node ? node.Value : throw new InvalidOperationException();

        object? System.Collections.IEnumerator.Current
            => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (this.current is null)
            {
                throw new InvalidOperationException();
            }

            this.current = this.current.Next;
            return this.current is not null;
        }

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
            => this.Next is null ? (this.Down is null ? false : this.Down.Contains(value)) : this.Next.Contains(value);

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
            if (this.Value.Equals(value))
            {
                return true;
            }

            if (this.Next is not null && this.Next.Value.CompareTo(value) < 0)
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

            if (this.Next.Value.Equals(value))
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
