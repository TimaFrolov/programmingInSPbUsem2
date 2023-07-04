namespace hw5;

using System.Text;

/// <summary>
/// Class representing weighted undirected graph.
/// </summary>
/// <typeparam name="TNode">Type of nodes in graph.</typeparam>
public class Graph<TNode>
where TNode : notnull
{
    private HashSet<TNode> nodes = new ();
    private Dictionary<(TNode, TNode), int> edges = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="Graph&lt;TNode&gt;"/> class with no nodes and no edges.
    /// </summary>
    public Graph()
    {
    }

    /// <summary>
    /// Add edge between given nodes in graph.
    /// </summary>
    /// <param name="node1">First node to connect new edge to.</param>
    /// <param name="node2">Second node to connect new edge to.</param>
    /// <param name="length">Length of new edge.</param>
    public void AddEdge(TNode node1, TNode node2, int length)
    {
        if (this.edges.ContainsKey((node2, node1)))
        {
            (node1, node2) = (node2, node1);
        }

        this.nodes.Add(node1);
        this.nodes.Add(node2);
        this.edges[(node1, node2)] = length;
    }

    /// <summary>
    /// Generate longest tree in graph. Uses Kruskal's algorithm.
    /// </summary>
    /// <returns>Longest tree.</returns>
    public Graph<TNode> LongestTree()
    {
        (TNode node1, TNode node2, int length)[] sortedEdges = this.edges.ToArray().Select(pair => (pair.Key.Item1, pair.Key.Item2, pair.Value)).ToArray();

        Array.Sort(sortedEdges, (x, y) => y.length - x.length);

        var tree = new Graph<TNode>();

        var dss = new DisjointSetSystem(this.nodes);

        foreach (var edge in sortedEdges)
        {
            if (!dss.GetSet(edge.node1).Equals(dss.GetSet(edge.node2)))
            {
                tree.AddEdge(edge.node1, edge.node2, edge.length);
                dss.Union(edge.node1, edge.node2);
            }
        }

        return tree;
    }

    /// <inheritdoc cref="object.ToString"/>
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var edge in this.edges)
        {
            stringBuilder.Append('(');
            stringBuilder.Append(edge.Key.Item1);
            stringBuilder.Append(", ");
            stringBuilder.Append(edge.Key.Item2);
            stringBuilder.Append(')');
            stringBuilder.Append(", ");
        }

        return stringBuilder.ToString();
    }

    /// <inheritdoc cref="object.Equals(object?)"/>
    public override bool Equals(object? obj)
        => obj is Graph<TNode> graph && this.edges.All(pair => graph.edges.ContainsKey(pair.Key) && graph.edges[pair.Key] == pair.Value);

    /// <inheritdoc cref="object.GetHashCode"/>
    public override int GetHashCode()
        => base.GetHashCode();

    private class DisjointSetSystem
    {
        private Dictionary<TNode, TNode> sets;

        public DisjointSetSystem(IEnumerable<TNode> nodes)
            => this.sets = nodes.ToArray().ToDictionary(x => x);

        public TNode GetSet(TNode node)
        {
            if (node.Equals(this.sets[node]))
            {
                return node;
            }

            return this.sets[node] = this.GetSet(this.sets[node]);
        }

        public void Union(TNode node1, TNode node2)
            => this.sets[this.GetSet(node1)] = node2;
    }
}