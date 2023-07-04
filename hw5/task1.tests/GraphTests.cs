namespace hw5.tests;

public class GraphTests
{
    private static (Graph<int>, Graph<int>) TestCase1()
    {
        var graph = new Graph<int>();
        return (graph, graph);
    }

    private static (Graph<int>, Graph<int>) TestCase2()
    {
        var graph = new Graph<int>();
        graph.AddEdge(1, 2, 10);
        var expectedTree = new Graph<int>();
        expectedTree.AddEdge(1, 2, 10);
        return (graph, expectedTree);
    }

    private static (Graph<int>, Graph<int>) TestCase3()
    {
        var graph = new Graph<int>();
        graph.AddEdge(1, 2, 10);
        graph.AddEdge(1, 3, 5);
        graph.AddEdge(2, 3, 1);
        var expectedTree = new Graph<int>();
        expectedTree.AddEdge(1, 2, 10);
        expectedTree.AddEdge(1, 3, 5);
        return (graph, expectedTree);
    }

    private static readonly (Graph<int>, Graph<int>)[] TestCases =
    {
        TestCase1(),
        TestCase2(),
        TestCase3()
    };

    [TestCaseSource(nameof(TestCases))]
    public void EvaluationOfExpressionTreeGivesExpectedValue((Graph<int> graph, Graph<int> expectedResult) testCase)
        => Assert.That(testCase.graph.LongestTree(), Is.EqualTo(testCase.expectedResult));
}