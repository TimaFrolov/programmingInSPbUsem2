namespace hw6task2.tests;

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
    public void ReadingMapFromFileResultInCorrectMap()
    {
        this.file.Write("     #\n     #\n###    @\n\n  ###\n  #".ToArray().Select(x => (byte)x).ToArray());
        this.file.Position = 0;

        var expectedGraph = new GameObject[,] {
            {GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Wall,GameObject.Empty,GameObject.Empty},
            {GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Wall,GameObject.Empty,GameObject.Empty},
            {GameObject.Wall,GameObject.Wall,GameObject.Wall,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Player},
            {GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty},
            {GameObject.Empty,GameObject.Empty,GameObject.Wall,GameObject.Wall,GameObject.Wall,GameObject.Empty,GameObject.Empty,GameObject.Empty},
            {GameObject.Empty,GameObject.Empty,GameObject.Wall,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty,GameObject.Empty},
        };

        Assert.That(Parser.ParseFile(file), Is.EqualTo(expectedGraph));
    }

    [Test]
    public void ReadingIncorrectTokenResultInException()
    {
        this.file.Write("     #\n gdfgd    #\n###g54    @\n\n  ".ToArray().Select(x => (byte)x).ToArray());
        this.file.Position = 0;

        Assert.Throws<ParserException>(() => Parser.ParseFile(file));
    }
}