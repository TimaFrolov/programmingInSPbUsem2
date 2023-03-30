namespace hw4task2.tests;

public class UniqueListTests
{
    [Test]
    public void AddElementHasSameOnTop()
    {
        var list = new UniqueList<int>();

        list.Add(1);
        Assert.That(list.Get(0), Is.EqualTo(1));
    }

    [Test]
    public void AddTwoElementsResultInLastOnTop()
    {
        var list = new UniqueList<int>();

        list.Add(3);
        list.Add(1);
        Assert.That(list.Get(0), Is.EqualTo(1));
    }

    [Test]
    public void AddTwoElementsAndRemoveTopResultInFirstOnTop()
    {
        var list = new UniqueList<int>();

        list.Add(3);
        list.Add(1);
        list.Remove(1);
        Assert.That(list.Get(0), Is.EqualTo(3));
    }

    [Test]
    public void AddElementAndRemoveItTwiceResultInException()
    {
        var list = new UniqueList<int>();

        list.Add(1);
        list.Remove(1);
        Assert.Throws<KeyNotFoundException>(() => list.Remove(1));
    }

    [Test]
    public void AddAndReadFromIndexOneResultInException()
    {
        var list = new UniqueList<int>();

        list.Add(1);
        Assert.Throws<IndexOutOfRangeException>(() => list.Get(1));
    }

    [Test]
    public void AddTwoElementsAndGetIndexOfFirstResultInOne()
    {
        var list = new UniqueList<int>();

        list.Add(5);
        list.Add(3);
        Assert.That(list.IndexOf(5), Is.EqualTo(1));
    }

    [Test]
    public void AddTwoSameElementsResultInException()
    {
        var list = new UniqueList<int>();

        list.Add(5);
        Assert.Throws<AlreadyContainsException>(() => list.Add(5));
    }

    [Test]
    public void ChangeTopAndGetResultInSameValue()
    {
        var list = new UniqueList<int>();

        list.Add(5);
        list.Add(7);
        list.Add(8);
        list.Change(0, 10);
        Assert.That(list.Get(0), Is.EqualTo(10));
    }

    [Test]
    public void ChangeTailAndGetResultInSameValue()
    {
        var list = new UniqueList<int>();

        list.Add(5);
        list.Add(7);
        list.Add(8);
        list.Change(2, 10);
        Assert.That(list.Get(2), Is.EqualTo(10));
    }

    [Test]
    public void ChangeIncorrectIndexResultInException()
    {
        var list = new UniqueList<int>();

        list.Add(5);
        list.Add(7);
        list.Add(8);
        Assert.Throws<IndexOutOfRangeException>(() => list.Change(5, 10));
    }

    [Test]
    public void ChangeElementToSameResultInNoChanges()
    {
        var list = new UniqueList<int>();

        list.Add(5);
        list.Add(7);
        list.Add(8);
        list.Change(0, 8);
        Assert.That(list.Get(0), Is.EqualTo(8));
    }

    [Test]
    public void ChangeElementToAlreadyContainedResultInException()
    {
        var list = new UniqueList<int>();

        list.Add(5);
        list.Add(7);
        list.Add(8);
        Assert.Throws<AlreadyContainsException>(() => list.Change(1, 8));
    }
}