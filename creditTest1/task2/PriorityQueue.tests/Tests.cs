namespace PriorityQueue.tests;

public class Tests
{
    private static readonly List<(List<(int priority, int value)> data, List<int> result)> TestCases = new List<(List<(int, int)>, List<int>)>
    {
        (new List<(int, int)> { (10, 0), (5, 1), (20, 2), (5, 3), (5, 3), (1, 4), (2, 5), (5, 6) }, new List<int> { 2,0,1,3,3,6,5,4 })
    };

    private PriorityQueue<int> queue;

    [SetUp]
    public void Setup()
    {
        this.queue = new PriorityQueue<int>();
    }

    [TestCaseSource(nameof(TestCases))]
    public void Test((List<(int priority, int value)> data, List<int> result) testCase)
    {
        foreach (var (priority, value) in testCase.data)
        {
            this.queue.Enqueue(value, priority);
        }
        List<int> ans = new();
        while (!this.queue.Empty)
        {
            ans.Add(this.queue.Dequeue());
        }
        Assert.That(ans, Is.EqualTo(testCase.result));
    }
}
