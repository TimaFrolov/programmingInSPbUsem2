namespace hw5.tests;

public class ParserTests
{
    private string filepath;
    private FileStream file;

    [SetUp]
    public void Setup()
    {
        this.filepath = Path.GetTempFileName();
        this.file = new FileStream(this.filepath, FileMode.Open);
    }

    [TearDown]
    public void Teardown()
    {
        this.file.Close();
        File.Delete(filepath);
    }

    [Test]
    public void ReadingExpressionFromFileResultInCorrectExpression()
    {
        this.file.Write("1: 2 (10), 3 (5)\n2: \t\t\t 3   (        1)".ToArray().Select(x => (byte)x).ToArray());
        this.file.Position = 0;

        var expectedGraph = new Graph<int>();
        expectedGraph.AddEdge(1, 2, 10);
        expectedGraph.AddEdge(1, 3, 5);
        expectedGraph.AddEdge(2, 3, 1);

        Assert.That(new Parser(file).ParseFile(), Is.EqualTo(expectedGraph));
    }

    [Test]
    public void ReadingIncorrectTokenResultInException()
    {
        this.file.Write("* + 1 ())))) a".ToArray().Select(x => (byte)x).ToArray());
        this.file.Position = 0;

        Assert.Throws<IncorrectGraphException>(() => new Parser(file).ParseFile());
    }
}