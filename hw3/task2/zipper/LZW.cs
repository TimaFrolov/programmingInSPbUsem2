namespace Zipper;
using System.Text;

/// <summary>
/// Class for encoding and decoding using LZW transformation.
/// </summary>
public static class LZW
{
    private const int BUFFERSIZE = 256;

    private static TableRecord.None none = new ();

    /// <summary>
    /// Method for encoding using LZW.
    /// </summary>
    /// <param name="input">Bit file reader for input file.</param>
    /// <param name="output">Bit file writer for output file.</param>
    public static void Encode(IReader input, IWriter output)
    {
        Trie<int> table = new (
            Enumerable.Range(0, 256)
            .Select(n => new KeyValuePair<string, int>(((char)n).ToString(), n)));
        int tableSize = 256;

        StringBuilder stringBuilder = new ();
        string oldString = string.Empty;
        while (!input.ReachedEOF)
        {
            byte cur = input.ReadByte();
            stringBuilder.Append((char)cur);
            string newString = stringBuilder.ToString();
            if (!table.Contains(newString))
            {
                table[newString] = tableSize;
                stringBuilder = new ();
                stringBuilder.Append((char)cur);
                output.WriteBits(SymbolLengthInBits(tableSize), BitConverter.GetBytes(table[oldString]));
                oldString = ((char)cur).ToString();
                tableSize++;
                continue;
            }

            oldString = newString;
        }

        output.WriteBits(SymbolLengthInBits(tableSize), BitConverter.GetBytes(table[oldString]));
    }

    /// <summary>
    /// Method for decoding file encoded with <see cref="LZW.Encode"/>.
    /// </summary>
    /// <param name="input">Bit file reader for input file.</param>
    /// <param name="output">Bit file writer for output file.</param>
    public static void Decode(IReader input, IWriter output)
    {
        List<TableRecord.Some> table = Enumerable.Range(0, 256).Select(x => new TableRecord.Some((byte)x, none)).ToList();
        int tableSize = 256;

        TableRecord newRecord = none;
        while (!input.ReachedEOF)
        {
            int cur = ToInt32(input.ReadBits(SymbolLengthInBits(tableSize)));

            if (cur == tableSize)
            {
                newRecord = RecordWithSelfNumber(newRecord);
                table.Add((TableRecord.Some)newRecord);
                tableSize++;
                output.WriteBytes(PrintTableRecord(newRecord));
                continue;
            }

            if (newRecord != none)
            {
                table.Add(new TableRecord.Some(TableRecordGetFirst(table[cur]), newRecord));
                tableSize++;
            }

            newRecord = table[cur];
            output.WriteBytes(PrintTableRecord(newRecord));
        }
    }

    private static int SymbolLengthInBits(int tableSize) => int.Log2(tableSize) + 1;

    private static int ToInt32(byte[] data)
    {
        int ans = 0;
        for (int i = 0; i < data.Length; i++)
        {
            ans += (int)data[i] << (i * 8);
        }

        return ans;
    }

    private record TableRecord
    {
        public record None()
        : TableRecord();

        public record Some(byte value, TableRecord next)
        : TableRecord();

        private TableRecord()
        {
        }
    }

    private static byte[] PrintTableRecord(TableRecord tableRecord)
        => tableRecord switch
        {
            TableRecord.None x => new byte[0],
            TableRecord.Some x => PrintTableRecord(x.next).Append(x.value).ToArray(),
            _ => throw new NotImplementedException()
        };

    private static TableRecord.Some RecordWithSelfNumber(TableRecord tableRecord)
        => new TableRecord.Some(TableRecordGetFirst(tableRecord), tableRecord);

    private static byte TableRecordGetFirst(TableRecord tableRecord)
        => tableRecord switch
        {
            TableRecord.Some leaf when leaf.next is TableRecord.None => leaf.value,
            TableRecord.Some x => TableRecordGetFirst(x.next),
            _ => throw new NotImplementedException()
        };
}
