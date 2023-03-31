namespace hw4task2.tests;

public class ListTests
{
    private List<int> list;

    [SetUp]
    public void Setup()
    {
        this.list = new ();
    }

    [Test]
    public void AddElementHasSameOnTop()
    {
        this.list.Add(1);
        Assert.That(this.list.Get(0), Is.EqualTo(1));
    }

    [Test]
    public void AddTwoElementsResultInLastOnTop()
    {
        this.list.Add(3);
        this.list.Add(1);
        Assert.That(this.list.Get(0), Is.EqualTo(1));
    }

    [Test]
    public void AddTwoElementsAndRemoveTopResultInFirstOnTop()
    {
        this.list.Add(3);
        this.list.Add(1);
        this.list.Remove(1);
        Assert.That(this.list.Get(0), Is.EqualTo(3));
    }

    [Test]
    public void AddElementAndRemoveItTwiceResultInException()
    {
        this.list.Add(1);
        this.list.Remove(1);
        Assert.Throws<KeyNotFoundException>(() => this.list.Remove(1));
    }

    [Test]
    public void AddAndReadFromIndexOneResultInException()
    {
        this.list.Add(1);
        Assert.Throws<IndexOutOfRangeException>(() => this.list.Get(1));
    }

    [Test]
    public void AddTwoElementsAndGetIndexOfFirstResultInOne()
    {
        this.list.Add(5);
        this.list.Add(3);
        Assert.That(this.list.IndexOf(5), Is.EqualTo(1));
    }

    [Test]
    public void AddTwoSameElementsAndGetIndexOfFirstResultInZero()
    {
        this.list.Add(5);
        this.list.Add(5);
        Assert.That(this.list.IndexOf(5), Is.EqualTo(0));
    }

    [Test]
    public void ChangeTopAndGetResultInSameValue()
    {
        this.list.Add(5);
        this.list.Add(7);
        this.list.Add(8);
        this.list.Change(0, 10);
        Assert.That(this.list.Get(0), Is.EqualTo(10));
    }

    [Test]
    public void ChangeTailAndGetResultInSameValue()
    {
        this.list.Add(5);
        this.list.Add(7);
        this.list.Add(8);
        this.list.Change(2, 10);
        Assert.That(this.list.Get(2), Is.EqualTo(10));
    }

    [Test]
    public void ChangeIncorrectIndexResultInException()
    {
        this.list.Add(5);
        this.list.Add(7);
        this.list.Add(8);
        Assert.Throws<IndexOutOfRangeException>(() => this.list.Change(5, 10));
    }
}