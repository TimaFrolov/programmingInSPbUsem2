namespace task1.tests;

public class Tests
{
    [Test]
    public void MapOnListReturnsListWithFunctionAppliedToAllElements()
        => Assert.That(CollectionTransformer.Map(x => x * 2, new List<int> { 1, 2, 3 }), Is.EqualTo(new List<int> { 2, 4, 6 }));

    [Test]
    public void FilterEvenNumbersReturnsEvenNumbers()
        => Assert.That(CollectionTransformer.Filter(x => x % 2 == 0, new List<int> { 1, 2, 3, 4, 5, 10, 9 }), Is.EqualTo(new List<int> { 2, 4, 10 }));

    [Test]
    public void FoldListReturnsCorrectValue()
        => Assert.That(CollectionTransformer.Fold((acc, elem) => acc * elem, 1, new List<int> { 1, 2, 3 }), Is.EqualTo(6));

    [Test]
    public void FoldOnEmptyListReturnsInitValue()
        => Assert.That(CollectionTransformer.Fold((_, _) => -1, 1, new List<int>()), Is.EqualTo(1));
}
