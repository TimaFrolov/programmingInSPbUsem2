namespace hw4task1.tests;

public class BuilderTests
{
    private static readonly (Token[], ExpressionTree)[] TestCases =
    {
        (
            new Token[] {new Token.Number(2)},
            new ExpressionTree.Number(2)
        ),
        (
            new Token[] { new Token.Mul(), new Token.Number(1), new Token.Number(4) },
            new ExpressionTree.Binop(ExpressionTree.BinopType.Mul, new ExpressionTree.Number(1), new ExpressionTree.Number(4))
        ),
        (
            new Token[] { new Token.Mul(), new Token.Add(), new Token.Number(1), new Token.Number(1), new Token.Number(2) },
            new ExpressionTree.Binop(
                ExpressionTree.BinopType.Mul,
                new ExpressionTree.Binop(
                    ExpressionTree.BinopType.Add,
                    new ExpressionTree.Number(1),
                    new ExpressionTree.Number(1)),
                new ExpressionTree.Number(2))
        ),
    };

    [TestCaseSource(nameof(TestCases))]
    public void BuildTokenArrayResultsInExpectedExpression((Token[] expr, ExpressionTree expectedTree) testCase)
        => Assert.That(Builder.BuildExpressionTree(testCase.expr), Is.EqualTo(testCase.expectedTree));

    private static readonly Token[][] ExceptionTestCases =
    {
        new Token[] { new Token.Mul(), new Token.Number(1) }, // Not enough tokens
        new Token[] { new Token.Mul(), new Token.Number(1), new Token.Number(2), new Token.Number(3) }, // Too many tokens
        new Token[] {new Token.Number(2), new Token.Number(3)},
    };

    [TestCaseSource(nameof(ExceptionTestCases))]
    public void InvalidExpressionThrowsException(Token[] expr)
        => Assert.Throws<IncorrectExpressionException>(() => Builder.BuildExpressionTree(expr));
}