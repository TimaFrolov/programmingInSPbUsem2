namespace Zipper.Tests;

public class LZWTests
{
    [Test]
    public void EncodeAndThenDecodeGivesSameData()
    {
        byte[] testData = "TOBEORNOTTOBEORTOBEORNOTTOBE".Select(x => (byte)x).ToArray();
        TestingReader reader = new (testData);
        TestingWriter writer = new ();
        LZW.Encode(reader, writer);
        byte[] encodedData = writer.Extract();
        reader = new (encodedData);
        writer = new ();
        LZW.Decode(reader, writer);
        byte[] decodedData = writer.Extract();
        Assert.AreEqual(testData, decodedData);
    }
}

public class TestingWriter : IWriter
{
    private List<byte> list = new ();
    private int curPos;

    public TestingWriter()
    {
    }

    public void WriteBits(int amount, byte[] data)
    {
        int amountOfBytes = amount % 8 > 0 ? (amount / 8) + 1 : amount / 8;
        for (int i = 0; i < amountOfBytes; i++)
        {
            this.list.Add(data[i]);
        }
        this.curPos += amountOfBytes;
    }

    public void WriteBytes(byte[] bytes)
    {
        for (int i = 0; i < bytes.Length; i++)
        {
            this.list.Add(bytes[i]);
        }
        this.curPos += bytes.Length;
    }

    public byte[] Extract()
        => this.list.ToArray();
}

public class TestingReader : IReader
{
    private byte[] arr;

    private int curPos;

    public bool ReachedEOF { get; private set; } = false;

    public TestingReader(byte[] data)
    {
        this.arr = data;
        this.curPos = 0;
    }

    public byte[] ReadBits(int amount)
    {
        int amountOfBytes = amount % 8 > 0 ? (amount / 8) + 1 : amount / 8;
        byte[] ans = new ArraySegment<byte>(this.arr, curPos, amountOfBytes).ToArray();
        this.curPos += amountOfBytes;
        this.ReachedEOF = this.curPos >= this.arr.Length;
        return ans;
    }

    public byte ReadByte()
        => this.ReadBits(8)[0];
}