namespace Hw2Task2;

/// <summary>
/// Implementation of stack (FIFO container) on list.
/// </summary>
/// <typeparam name="T"> The type of elements in stack. </typeparam>
public class ListStack<T> : IEnumerable<T>
{
    private ListElement? topElement;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListStack&lt;T&gt;"/> class that contains elements from data in it (last element from data will be at top of stack).
    /// </summary>
    /// <param name="data"> Elements to put into stack. </param>
    public ListStack(IEnumerable<T> data)
    {
        foreach (T element in data)
        {
            this.Push(element);
        }
    }

    /// <summary>
    /// Push element on stack.
    /// </summary>
    /// <param name="element"> Element to push. </param>
    public void Push(T element)
        => this.topElement = new ListElement(this.topElement, element);

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

    /// <inheritdoc cref="IEnumerator&lt;T&gt;"/>
    public class Enumerator : IEnumerator<T>
    {
        private ListElement? curElement;

        private ListElement? initialElement;

        private bool wasMoved;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListStack&lt;T&gt;.Enumerator"/> class.
        /// </summary>
        /// <param name="stack"> Stack to iterate through. </param>
        public Enumerator(ListStack<T> stack)
        {
            this.initialElement = stack.topElement;
            this.curElement = stack.topElement;
        }

        /// <inheritdoc cref="System.Collections.Generic.IEnumerator&lt;T&gt;"/>
        public T Current => this.curElement != null ? this.curElement.Value : throw new EnumeratorException();

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        object System.Collections.IEnumerator.Current => this.Current ?? throw new EnumeratorException();

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        public bool MoveNext()
        {
            if (this.curElement == null)
            {
                return false;
            }

            if (!this.wasMoved)
            {
                this.wasMoved = true;
                return true;
            }

            if (this.curElement.NextElement == null)
            {
                return false;
            }

            this.curElement = this.curElement.NextElement;
            return true;
        }

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        public void Reset()
        {
            this.curElement = this.initialElement;
        }

        /// <inheritdoc cref="System.Collections.IEnumerator"/>
        public void Dispose()
        {
        }
    }

    private class ListElement
    {
        public ListElement(ListElement? nextElement, T value)
        {
            this.NextElement = nextElement;
            this.Value = value;
        }

        public ListElement? NextElement { get; }

        public T Value { get; }
    }
}
