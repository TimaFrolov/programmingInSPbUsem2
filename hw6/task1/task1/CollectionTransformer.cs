namespace task1;

using System.Collections.Generic;

/// <summary>
/// Static class providing functions for transform lists.
/// </summary>
public static class CollectionTransformer
{
    /// <summary>
    /// Apply function to each element of given collection.
    /// </summary>
    /// <param name="func">Function to apply.</param>
    /// <param name="collection">Collection to iterate through.</param>
    /// <typeparam name="TIn">Type of values in given collection.</typeparam>
    /// <typeparam name="TOut">Type of output list.</typeparam>
    /// <returns>List of values getting by applying given function to each element of given collection.</returns>
    public static List<TOut> Map<TIn, TOut>(Func<TIn, TOut> func, IEnumerable<TIn> collection)
    {
        var ans = new List<TOut>();
        IEnumerator<TIn> enumerator = collection.GetEnumerator();

        while (enumerator.MoveNext())
        {
            ans.Add(func(enumerator.Current));
        }

        return ans;
    }

    /// <summary>
    /// Get elements from collection that satisfies predicate.
    /// </summary>
    /// <param name="pred">Predicate to check on elements from given collection.</param>
    /// <param name="collection">Collection to iterate through.</param>
    /// <typeparam name="T">Type of values in given collection.</typeparam>
    /// <returns>List of values from collection that satisfies given predicate.</returns>
    public static List<T> Filter<T>(Func<T, bool> pred, IEnumerable<T> collection)
    {
        var ans = new List<T>();
        IEnumerator<T> enumerator = collection.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (pred(enumerator.Current))
            {
                ans.Add(enumerator.Current);
            }
        }

        return ans;
    }

    /// <summary>
    /// Function to fold collection.
    /// </summary>
    /// <param name="func">Function to apply to current accumulator and next value from collection.</param>
    /// <param name="init">Initial value of accumulator.</param>
    /// <param name="collection">Collection to iterate through.</param>
    /// <typeparam name="TIn">Type of values in given collection.</typeparam>
    /// <typeparam name="TOut">Type of output value.</typeparam>
    /// <returns>Accumulator after last element in collection.</returns>
    public static TOut Fold<TIn, TOut>(Func<TOut, TIn, TOut> func, TOut init, IEnumerable<TIn> collection)
    {
        IEnumerator<TIn> enumerator = collection.GetEnumerator();

        while (enumerator.MoveNext())
        {
            init = func(init, enumerator.Current);
        }

        return init;
    }
}
