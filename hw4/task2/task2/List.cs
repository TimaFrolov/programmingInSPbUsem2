namespace hw4task2;

/// <summary>
/// Represents a strongly typed list of objects that can be accessed by index.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class List<T>
{
    /// <summary>
    /// ListElement.None instance.
    /// </summary>
    protected static readonly ListElement.None None = new ();

    private ListElement top = None;

    /// <summary>
    /// Gets or sets top element of the list.
    /// </summary>
    protected ListElement Top
    {
        get => this.top;
        set => this.top = value;
    }

    /// <summary>
    /// Get element in list by index.
    /// </summary>
    /// <param name="index">Index to get element at.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if length of list less than given index.</exception>
    /// <returns>Element at given index.</returns>
    public virtual T Get(int index)
    {
        ListElement.Some element = this.top.GetSomeOrThrowException<IndexOutOfRangeException>();
        while (index > 0)
        {
            element = element.Next.GetSomeOrThrowException<IndexOutOfRangeException>();
            index--;
        }

        return element.Value;
    }

    /// <summary>
    /// Get index of given item. For comparison uses non-static T.Equals(T other) method.
    /// </summary>
    /// <param name="item">Item to find in list.</param>
    /// <returns>Fist occurance of given item in list. -1 if there is no one.</returns>
    public virtual int IndexOf(T item)
    {
        int index = 0;
        ListElement element = this.top;
        while (element is ListElement.Some some)
        {
            if (some.Value?.Equals(item) ?? item is null)
            {
                return index;
            }

            element = some.Next;
            index++;
        }

        return -1;
    }

    /// <summary>
    /// Change element on given index to given item.
    /// </summary>
    /// <param name="index">Index to change element on.</param>
    /// <param name="item">Item to place instead of previous one.</param>
    /// /// <exception cref="IndexOutOfRangeException">Thrown if length of list less than given index.</exception>
    public virtual void Change(int index, T item)
    {
        ListElement.Some changeThis = this.top.GetSomeOrThrowException<IndexOutOfRangeException>();
        while (index > 0)
        {
            changeThis = changeThis.Next.GetSomeOrThrowException<IndexOutOfRangeException>();
            index--;
        }

        changeThis.Value = item;
    }

    /// <summary>
    /// Add element to top of list.
    /// </summary>
    /// <param name="item">Item to add to list.</param>
    public virtual void Add(T item)
        => this.top = new ListElement.Some(item, this.top);

    /// <summary>
    /// Remove one copy of given item from list. For comparison uses non-static T.Equals(T other) method.
    /// </summary>
    /// <param name="item">Item to remove from list.</param>
    /// /// <exception cref="KeyNotFoundException">Thrown if there was no occurance of given item in list.</exception>
    public virtual void Remove(T item)
    {
        ListElement.Some removeFrom = this.top.GetSomeOrThrowException<KeyNotFoundException>();
        if (removeFrom.Value?.Equals(item) ?? item is null)
        {
            this.top = removeFrom.Next;
            return;
        }

        ListElement.Some next = removeFrom.Next.GetSomeOrThrowException<KeyNotFoundException>();
        while (!(next.Value?.Equals(item) ?? item is null))
        {
            removeFrom = next;
            next = next.Next.GetSomeOrThrowException<KeyNotFoundException>();
        }

        removeFrom.Next = next.Next;
    }

    /// <summary>
    /// Algebraic list.
    /// </summary>
    protected record ListElement
    {
        /// <summary>
        /// Empty list.
        /// </summary>
        public record None()
        : ListElement;

#pragma warning disable SA1313 // Disable checking parameter names to not have duplicates of value and next in Some

        /// <summary>
        /// List with some values in it.
        /// </summary>
        /// <param name="Value">Value stored at head.</param>
        /// <param name="Next">Tail of list.</param>
        public record Some(T Value, ListElement Next)
        : ListElement()
        {
            /// <summary>
            /// Gets or sets value stored at head.
            /// </summary>
            public T Value { get; set; } = Value;

            /// <summary>
            /// Gets or sets tail of list.
            /// </summary>
            public ListElement Next { get; set; } = Next;
        }

#pragma warning restore SA1313

        private ListElement()
        {
        }

        /// <summary>
        /// Convert to <see cref="ListElement.Some"/> or throw TException if it is impossible.
        /// </summary>
        /// <typeparam name="TException">Exception to throw.</typeparam>
        /// <returns>this, converted to <see cref="ListElement.Some"/>.</returns>
        public Some GetSomeOrThrowException<TException>()
            where TException : Exception, new()
            => this switch
            {
                Some some => some,
                _ => throw new TException()
            };
    }
}
