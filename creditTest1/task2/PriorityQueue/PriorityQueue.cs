namespace PriorityQueue;

using System.Collections.Generic;

/// <summary>
/// Class representing priority queue.
/// </summary>
/// <typeparam name="T">Values that queue can hold in it.</typeparam>
public sealed class PriorityQueue<T>
{
    private SortedDictionary<int, Queue<T>> queues = new ();

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
        if (!this.queues.ContainsKey(priority))
        {
            this.queues[priority] = new Queue<T>();
        }

        this.queues[priority].Enqueue(value);
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

        var maxPriority = this.queues.Keys.Max()!;
        T ans = this.queues[maxPriority].Dequeue();
        this.count--;
        if (this.queues[maxPriority].Count == 0)
        {
            this.queues.Remove(maxPriority);
        }

        return ans;
    }
}
