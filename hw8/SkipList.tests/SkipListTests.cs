namespace SkipList.tests;

public class SkipListTests
{
    private SkipList<int> list;

    [SetUp]
    public void Setup()
    {
        this.list = new();
        this.list.Add(1);
        this.list.Add(3);
        this.list.Add(2);
        this.list.Add(2);
    }

    [Test]
    public void IsNotReadOnly()
    {
        Assert.That(this.list.IsReadOnly, Is.False);
    }

    [Test]
    public void AppendResultsInSortedOrderAndCorrectCount()
    {
        Assert.That(this.list[0], Is.EqualTo(1));
        Assert.That(this.list[1], Is.EqualTo(2));
        Assert.That(this.list[2], Is.EqualTo(2));
        Assert.That(this.list[3], Is.EqualTo(3));
        Assert.That(this.list.Count, Is.EqualTo(4));
    }

    [Test]
    public void GetIndexerThrowsWhenIndexIsOutOfRange()
    {
        int x;
        Assert.Throws<ArgumentOutOfRangeException>(() => x = this.list[7]);
    }

    [Test]
    public void IndexSetterThrowsNotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => this.list[4] = 7);
    }

    [Test]
    public void IndexOfGivesCorrectResult()
    {
        Assert.That(this.list.IndexOf(1), Is.EqualTo(0));
        Assert.That(this.list.IndexOf(2), Is.InRange(1, 2));
        Assert.That(this.list.IndexOf(3), Is.EqualTo(3));
        Assert.That(this.list.IndexOf(4), Is.EqualTo(-1));
    }

    [Test]
    public void ContainsGivesCorrectResult()
    {
        Assert.That(this.list.Contains(1), Is.True);
        Assert.That(this.list.Contains(2), Is.True);
        Assert.That(this.list.Contains(3), Is.True);
        Assert.That(this.list.Contains(4), Is.False);
    }

    [Test]
    public void InsertThrowsNotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => this.list.Insert(5, 4));
    }

    [Test]
    public void RemoveAtThrowsNotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => this.list.RemoveAt(0));
    }

    public void ClearRemovesAllElements()
    {
        this.list.Clear();

        Assert.That(this.list.Count, Is.EqualTo(0));
        Assert.That(this.list.Contains(1), Is.False);
        Assert.That(this.list.Contains(2), Is.False);
        Assert.That(this.list.Contains(3), Is.False);
    }

    [Test]
    public void RemoveRemovesElementFromList()
    {
        Assert.That(this.list.Remove(2), Is.True);
        Assert.That(this.list.Count, Is.EqualTo(3));
        Assert.That(this.list[0], Is.EqualTo(1));
        Assert.That(this.list[1], Is.EqualTo(2));
        Assert.That(this.list[2], Is.EqualTo(3));
    }

    [Test]
    public void RemoveReturnsFalseWhenElementIsNotFound()
    {
        Assert.That(this.list.Remove(4), Is.False);
        Assert.That(this.list.Count, Is.EqualTo(4));
        Assert.That(this.list[0], Is.EqualTo(1));
        Assert.That(this.list[1], Is.EqualTo(2));
        Assert.That(this.list[2], Is.EqualTo(2));
        Assert.That(this.list[3], Is.EqualTo(3));
    }

    [Test]
    public void CopyToResultsInCorrectArray()
    {
        var array = new int[4];
        this.list.CopyTo(array, 0);

        Assert.That(array[0], Is.EqualTo(1));
        Assert.That(array[1], Is.EqualTo(2));
        Assert.That(array[2], Is.EqualTo(2));
        Assert.That(array[3], Is.EqualTo(3));
    }

    [Test]
    public void CopyToThrowsWhenArrayIsTooSmall()
    {
        var array = new int[3];
        Assert.Throws<ArgumentException>(() => this.list.CopyTo(array, 0));
    }

    [Test]
    public void CopyToThrowsWhenIndexIsOutOfRange()
    {
        var array = new int[4];
        Assert.Throws<ArgumentOutOfRangeException>(() => this.list.CopyTo(array, -1));
    }

    [Test]
    public void CopyToThrowsWhenIndexPlusCountIsGreaterThanArrayLength()
    {
        var array = new int[4];
        Assert.Throws<ArgumentException>(() => this.list.CopyTo(array, 1));
    }

    [Test]
    public void GetEnumeratorReturnsEnumerator()
    {
        var enumerator = this.list.GetEnumerator();
        Assert.That(enumerator, Is.Not.Null);
        Assert.That(enumerator.MoveNext(), Is.True);
        Assert.That(enumerator.Current, Is.EqualTo(1));
        Assert.That(enumerator.MoveNext(), Is.True);
        Assert.That(enumerator.Current, Is.EqualTo(2));
        Assert.That(enumerator.MoveNext(), Is.True);
        Assert.That(enumerator.Current, Is.EqualTo(2));
        Assert.That(enumerator.MoveNext(), Is.True);
        Assert.That(enumerator.Current, Is.EqualTo(3));
        Assert.That(enumerator.MoveNext(), Is.False);
    }

    [Test]
    public void ForEachLoopWorks()
    {
        foreach (int x in this.list)
        {
            Assert.That(this.list.Contains(x), Is.True);
        }
    }
}
