namespace hw6task2;

/// <summary>
/// Static class for parsing map file.
/// </summary>
public static class Parser
{
    /// <summary>
    /// Parse map file.
    /// </summary>
    /// <param name="stream">File to read map from.</param>
    /// <exception cref="ParserException">Thrown when file contains unexpected character.</exception>
    /// <returns>Map read from file.</returns>
#pragma warning disable SA1011 // Disable warning requiring space after closing bracket.
    public static GameObject[,]? ParseFile(FileStream stream)
#pragma warning restore SA1011
    {
        var map = new List<List<GameObject>>();
        var row = new List<GameObject>();
        while (stream.Position < stream.Length)
        {
            char cur = (char)stream.ReadByte();
            switch (cur)
            {
                case ' ':
                    row.Add(GameObject.Empty);
                    break;
                case '#':
                    row.Add(GameObject.Wall);
                    break;
                case '@':
                    row.Add(GameObject.Player);
                    break;
                case '\n':
                    map.Add(row);
                    row = new List<GameObject>();
                    break;
                default:
                    throw new ParserException($"Unexpected character: {cur}");
            }
        }

        if (row.Count > 0)
        {
            map.Add(row);
        }

        if (map.Count == 0)
        {
            return new GameObject[0, 0];
        }

        var result = new GameObject[map.Count, map.Select(x => x.Count).Max()];
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                result[i, j] = map[i][j];
            }
        }

        return result;
    }
}

/// <summary>
/// Exception thrown when error occured in parsing file.
/// </summary>
[System.Serializable]
public class ParserException : Exception
{
    /// <inheritdoc cref="Exception"/>
    public ParserException()
    {
    }

    /// <inheritdoc cref="Exception"/>
    public ParserException(string message)
    : base(message)
    {
    }

    /// <inheritdoc cref="Exception"/>
    public ParserException(string message, System.Exception inner)
    : base(message, inner)
    {
    }

    /// <inheritdoc cref="Exception"/>
    protected ParserException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
    : base(info, context)
    {
    }
}