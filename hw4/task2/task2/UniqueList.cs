namespace hw4task2;

/// <inheritdoc cref="List&lt;T&gt;"/>
/// <summary>
/// Represents a strongly typed list of objects that can be accessed by index that can contain only one copy of each element.
/// For comparison uses non-static T.Equals(T other) method.
/// </summary>
public class UniqueList<T> : List<T>
{
    /// <inheritdoc cref="List&lt;T&gt;.Change"/>
    /// <exception cref="AlreadyContainsException">Thrown there already was copy of given item in list.</exception>
    public override void Change(int index, T item)
    {
        ListElement.Some changeThis = this.Top.GetSomeOrThrowException<IndexOutOfRangeException>();
        while (index > 0)
        {
            if (changeThis.Value?.Equals(item) ?? item is null)
            {
                throw new AlreadyContainsException();
            }

            changeThis = changeThis.Next.GetSomeOrThrowException<IndexOutOfRangeException>();
            index--;
        }

        if (Contains(changeThis.Next, item))
        {
            throw new AlreadyContainsException();
        }

        changeThis.Value = item;
    }

    /// <inheritdoc cref="List&lt;T&gt;.Add"/>
    /// <exception cref="AlreadyContainsException">Thrown there already was copy of given item in list.</exception>
    public override void Add(T item)
    {
        if (Contains(this.Top, item))
        {
            throw new AlreadyContainsException();
        }

        this.Top = new ListElement.Some(item, this.Top);
    }

    private static bool Contains(ListElement element, T value)
        => element switch
        {
            ListElement.Some some => (some.Value?.Equals(value) ?? value is null) || Contains(some.Next, value),
            _ => false
        };
}

/// <summary>
/// Exception thrown when trying to insert item into list that already contains it.
/// </summary>
[System.Serializable]
public class AlreadyContainsException : System.Exception
{
    /// <inheritdoc cref="System.Exception"/>
    public AlreadyContainsException()
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public AlreadyContainsException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    public AlreadyContainsException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="System.Exception"/>
    protected AlreadyContainsException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}