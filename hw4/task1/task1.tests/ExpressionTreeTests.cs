namespace hw4task1.tests;

public class ExpressionTreeTests
{
    private static readonly (ExpressionTree, Fraction)[] TestCases =
    {
        (new ExpressionTree.Number(5), (Fraction)5),
        (
            new ExpressionTree.Binop
            (
                ExpressionTree.BinopType.Add,
                new ExpressionTree.Number(5),
                new ExpressionTree.Number(3)
            ), 
            (Fraction)8
        ),
        (
            new ExpressionTree.Binop
            (
                ExpressionTree.BinopType.Mul,
                new ExpressionTree.Binop
                (
                    ExpressionTree.BinopType.Add,
                    new ExpressionTree.Number(1),
                    new ExpressionTree.Number(1)
                ),
                new ExpressionTree.Number(5)
            ), 
            (Fraction)10
        ),
    };

    [TestCaseSource(nameof(TestCases))]
    public void EvaluationOfExpressionTreeGivesExpectedValue((ExpressionTree tree, Fraction expectedResult) testCase)
        => Assert.That(testCase.tree.Evaluate(), Is.EqualTo(testCase.expectedResult));
}