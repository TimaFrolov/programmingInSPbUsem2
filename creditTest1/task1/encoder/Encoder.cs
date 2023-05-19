namespace encoder;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class for encoding and decoding sequences of bytes.
/// </summary>
public static class Encoder
{
    /// <summary>
    /// Encodes sequence of bytes.
    /// </summary>
    /// <param name="data">data to encode.</param>
    /// <returns>Encoded data and ratio of length of encoded data to source data.</returns>
    public static (List<byte> encodedData, double compressionRatio) Encode(IEnumerable<byte> data)
    {
        var sourceCount = 0;
        var outCount = 0;
        var ans = data.Aggregate(new List<byte>(), (List<byte> list, byte x) =>
        {
            sourceCount++;
            if (list.Count > 0 && list.Last() == x && list[list.Count - 2] < byte.MaxValue)
            {
                list[list.Count - 2]++;
            }
            else
            {
                list.Add(1);
                list.Add(x);
                outCount += 2;
            }

            return list;
        });
        return (ans, outCount / (double)sourceCount);
    }

    /// <summary>
    /// Decodes sequence of bytes.
    /// </summary>
    /// <param name="source">data to decode.</param>
    /// <returns>Decoded data.</returns>
    public static List<byte> Decode(IEnumerable<byte> source)
    {
        var ans = AggregateTwo(source, new List<byte>(), (List<byte> list, (byte amount, byte x) v) =>
        {
            list.AddRange(Enumerable.Range(0, v.amount).Select(_ => v.x));
            return list;
        });
        return ans;
    }

    private static TOut AggregateTwo<TIn, TOut>(IEnumerable<TIn> source, TOut seed, System.Func<TOut, (TIn, TIn), TOut> func)
    {
        TOut ans = seed;
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var cur1 = enumerator.Current;
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("Source sequence has odd number of elements");
            }

            var cur2 = enumerator.Current;
            ans = func(ans, (cur1, cur2));
        }

        return ans;
    }
}
