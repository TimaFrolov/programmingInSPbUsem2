namespace SkipList.tests;

public class SkipListOrderTests
{
    private class TestComparable : IComparable<TestComparable>
    {
        public int ComparedValue { get; }
        public int anotherValue { get; }

        public TestComparable(int comparedValue, int anotherValue)
        {
            this.ComparedValue = comparedValue;
            this.anotherValue = anotherValue;
        }

        public static implicit operator TestComparable((int compared, int another) value)
            => new(value.compared, value.another);

        public int CompareTo(TestComparable? other)
            => this.ComparedValue.CompareTo(other?.ComparedValue ?? int.MinValue);

        public new string ToString()
            => $"({this.ComparedValue}, {this.anotherValue})";
    }

    private SkipList<TestComparable> list;

    [SetUp]
    public void Setup()
    {
        this.list = new();
    }

    [Test]
    public void RemoveRemovesFirstOccurrence()
    {
        this.list.Add((1, 1));
        this.list.Add((3, 1));
        this.list.Add((2, 0));
        this.list.Add((2, 1));

        var arr = new TestComparable[4];
        this.list.CopyTo(arr, 0);

        Assert.That(this.list.Remove((2, 9)), Is.True);
        Assert.That(this.list.Count, Is.EqualTo(3));
        Assert.That(this.list[0], Is.EqualTo(arr[0]));
        Assert.That(this.list[1], Is.EqualTo(arr[2]));
        Assert.That(this.list[2], Is.EqualTo(arr[3]));
    }
}