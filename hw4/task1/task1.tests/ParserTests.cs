namespace hw4task1.tests;

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
        this.file.Write("*( + 1 1) 2 - / 4 5      ())))))))))))))))(())))      \n\n\r\t\t789 12 43 * / -".ToArray().Select(x => (byte)x).ToArray());
        this.file.Position = 0;

        Assert.That(Parser.ParseExpression(file), Is.EqualTo(
            new List<Token> {new Token.Mul(), new Token.Add(), new Token.Number(1), new Token.Number(1),
            new Token.Number(2), new Token.Sub(), new Token.Div() , new Token.Number(4),  new Token.Number(5),
            new Token.Number(789), new Token.Number(12), new Token.Number(43), new Token.Mul(), new Token.Div(), new Token.Sub()}
        ));
    }

    [Test]
    public void ReadingIncorrectTokenResultInException()
    {
        this.file.Write("* + 1 ())))) a".ToArray().Select(x => (byte)x).ToArray());
        this.file.Position = 0;

        Assert.Throws<UnexpectedTokenException>(() => Parser.ParseExpression(file));
    }
}