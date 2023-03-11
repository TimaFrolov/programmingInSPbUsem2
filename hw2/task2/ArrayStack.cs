namespace Hw2Task2;

/// <summary>
/// Implementation of stack (FIFO container) on array.
/// </summary>
/// <typeparam name="T"> The type of elements in stack. </typeparam>
public class ArrayStack<T> : IEnumerable<T>
{
    private const int InitSize = 8;

    private T[] arr;

    private int curIndex = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayStack&lt;T&gt;"/> class that contains no elements in it.
    /// </summary>
    public ArrayStack()
    : this(InitSize)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayStack&lt;T&gt;"/> class that contains elements from data in it (last element from data will be at top of stack).
    /// </summary>
    /// <param name="data"> Elements to put into stack. </param>
    public ArrayStack(IEnumerable<T> data)
    : this(data.Count())
    {
        foreach (T element in data)
        {
            this.Push(element);
        }
    }

    private ArrayStack(int initSize)
        => this.arr = new T[initSize];

    /// <summary>
    /// Push element on stack.
    /// </summary>
    /// <param name="element"> Element to push. </param>
    public void Push(T element)
    {
        this.curIndex++;
        if (this.curIndex == this.arr.Length)
        {
            this.Resize(this.arr.Length * 2);
        }

        this.arr[this.curIndex] = element;
    }

    /// <inheritdoc cref="IEnumerable&lt;T&gt;"/>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    /// <inheritdoc cref="IEnumerable&lt;T&gt;"/>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <inheritdoc cref="System.Collections.IEnumerable"/>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void Resize(int newSize)
        => Array.Resize<T>(ref this.arr, newSize);

    /// <inheritdoc cref="IEnumerator&lt;T&gt;"/>
    public class Enumerator : IEnumerator<T>
    {
        private ArrayStack<T> stack;

        private int curIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayStack&lt;T&gt;.Enumerator"/> class.
        /// </summary>
        /// <param name="stack"> Stack to iterate through. </param>
        public Enumerator(ArrayStack<T> stack)
        {
            this.stack = stack;
            this.curIndex = stack.arr.Length;
        }

        /// <inheritdoc cref="System.Collections.Generic.IEnumerator&lt;T&gt;"/>
        public T Current => this.stack.arr[this.curIndex];

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        object System.Collections.IEnumerator.Current => this.Current ?? throw new EnumeratorException();

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        public bool MoveNext()
        {
            this.curIndex -= 1;
            return this.curIndex >= 0;
        }

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        public void Reset()
            => this.curIndex = this.stack.arr.Length;

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        public void Dispose()
        {
        }
    }
}
