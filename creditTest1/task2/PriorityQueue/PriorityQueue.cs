namespace PriorityQueue;

using System.Collections.Generic;

/// <summary>
/// Class representing priority queue.
/// </summary>
/// <typeparam name="T">Values that queue can hold in it.</typeparam>
public sealed class PriorityQueue<T>
{
    private SortedDictionary<int, Queue<T>> data = new ();

    private int count = 0;

    /// <summary>
    /// Gets a value indicating whether the queue is empty.
    /// </summary>
    public bool Empty => this.count == 0;

    /// <summary>
    /// Adds value to queue with given priority.
    /// </summary>
    /// <param name="value">Value to add.</param>
    /// <param name="priority">Priority of new value.</param>
    public void Enqueue(T value, int priority)
    {
        if (!this.data.ContainsKey(priority))
        {
            this.data[priority] = new Queue<T>();
        }

        this.data[priority].Enqueue(value);
        this.count++;
    }

    /// <summary>
    /// Returns value with highest priority and removes it from queue.
    /// If there are multiple values with highest priority, returns first added.
    /// </summary>
    /// <returns>Value with highest priority.</returns>
    public T Dequeue()
    {
        if (this.Empty)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        var maxPriority = this.data.Keys.Max()!;
        T ans = this.data[maxPriority].Dequeue();
        this.count--;
        if (this.data[maxPriority].Count == 0)
        {
            this.data.Remove(maxPriority);
        }

        return ans;
    }
}
