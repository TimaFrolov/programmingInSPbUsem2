namespace encoder.tests;

public class Tests
{
    private static readonly List<(List<byte> source, List<byte> encoded)> EncodingTestCases = new List<(List<byte>, List<byte>)>
    {
        (new List<byte> { 1, 1, 1, 1, 1, 1 }, new List<byte> { 6, 1 }),
        (new List<byte> { 1, 1, 1, 2, 1, 1 }, new List<byte> { 3, 1, 1, 2, 2, 1 }),
        (Enumerable.Range(0, 300).Select(_ => (byte)1).ToList(), new List<byte> { 255, 1, 45, 1}),
    };


    [TestCaseSource(nameof(EncodingTestCases))]
    public void EncodeTest((List<byte> source, List<byte> encoded) testCase)
    {
        Assert.That(Encoder.Encode(testCase.source).encodedData, Is.EqualTo(testCase.encoded));
    }


    [TestCaseSource(nameof(EncodingTestCases))]
    public void DecodeTest((List<byte> source, List<byte> encoded) testCase)
    {
        Assert.That(Encoder.Decode(testCase.encoded), Is.EqualTo(testCase.source));
    }
}
