namespace Zipper.Tests;

public class TrieTests
{
    [Test]
    public void PutAndContainsResultIsTrue()
    {
        Trie<int> trie = new();
        trie.Add("abc", 1);
        Assert.That(trie.Contains("abc"), Is.EqualTo(true));
    }

    [Test]
    public void PutAndGetResultInSameValue()
    {
        Trie<int> trie = new();
        trie.Add("abc", 1);
        Assert.That(trie.Get("abc"), Is.EqualTo(1));
    }

    [Test]
    public void PutSomeValuesAndGetThemResultInSameValues()
    {
        Trie<int> trie = new();
        trie.Add("abc", 1);
        trie.Add("ab", 10);
        trie.Add("a", 5);
        Assert.That(trie.Get("abc"), Is.EqualTo(1));
        Assert.That(trie.Get("ab"), Is.EqualTo(10));
        Assert.That(trie.Get("a"), Is.EqualTo(5));
    }

    [Test]
    public void PutAndRemoveResultInNoContains()
    {
        Trie<int> trie = new();
        trie.Add("abc", 1);
        trie.Remove("abc");
        Assert.That(trie.Contains("abc"), Is.EqualTo(false));
    }

    [Test]
    public void GenerateFromCollectionResultInCorrectTrie()
    {
        Trie<int> trie = new(
            new KeyValuePair<string, int>[]
            { new KeyValuePair<string, int>("abc", 1), new KeyValuePair<string, int>("ab", 10), new KeyValuePair<string, int>("a", 5) });
        Assert.That(trie.Get("abc"), Is.EqualTo(1));
        Assert.That(trie.Get("ab"), Is.EqualTo(10));
        Assert.That(trie.Get("a"), Is.EqualTo(5));
    }
}