namespace Zipper;

/// <summary>
/// Class for writing numbers by bits into file.
/// </summary>
public class BitWriter
{
    private FileStream file;

    /// <summary>
    /// Initializes a new instance of the <see cref="BitWriter"/> class.
    /// </summary>
    /// <param name="file">FileStream to write to.</param>
    public BitWriter(FileStream file)
        => this.file = file;

    /// <summary>
    /// Write bits to file.
    /// </summary>
    /// <param name="amount">Amount of bits to write to file.</param>
    /// <param name="data">Data to write to file.</param>
    public void WriteBits(int amount, byte[] data)
    {
        for (int i = 0; i * 8 < amount; i += 1)
        {
            this.file.Write(new byte[] { data[i] });
        }
    }

    /// <summary>
    /// Write array of bytes to file.
    /// </summary>
    /// <param name="bytes">Bytes to write.</param>
    public void WriteBytes(byte[] bytes)
        => this.file.Write(bytes);

    /// <summary>
    /// Close file.
    /// </summary>
    public void Close()
        => this.file.Close();
}

/// <summary>
/// Class for reading numbers by bits from file.
/// </summary>
public class BitReader
{
    private FileStream file;

    /// <summary>
    /// Initializes a new instance of the <see cref="BitReader"/> class.
    /// </summary>
    /// <param name="file">FileStream to read from.</param>
    public BitReader(FileStream file)
        => this.file = file;

    /// <summary>
    /// Gets a value indicating whether reader has reached end of file.
    /// </summary>
    public bool ReachedEOF { get; private set; } = false;

    /// <summary>
    /// Read bits from file.
    /// </summary>
    /// <param name="amount">Amount of bits to read from file.</param>
    /// <returns>Array of bytes read from file.</returns>
    public byte[] ReadBits(int amount)
    {
        var buffer = new byte[amount % 8 > 0 ? (amount / 8) + 1 : amount / 8];
        this.file.Read(buffer);
        this.ReachedEOF = this.file.Position >= this.file.Length;
        return buffer;
    }

    /// <summary>
    /// Read byte from fyle.
    /// </summary>
    /// <returns>Byte read from file.</returns>
    public byte ReadByte()
        => this.ReadBits(8)[0];

    /// <summary>
    /// Close file.
    /// </summary>
    public void Close()
        => this.file.Close();
}