namespace TicTacToe.tests;

using Game;

public class GameTests
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        this.game = new Game();
    }

    [Test]
    public void ThreeCrossesResultInCrossWin()
    {
        Assert.That(game.CurPlayer, Is.EqualTo(GameObject.Cross));

        game.MakeMove(0, 0);
        Assert.That(game.Field, Is.EqualTo(new GameObject[,]{
            {GameObject.Cross, GameObject.Empty, GameObject.Empty},
            {GameObject.Empty, GameObject.Empty, GameObject.Empty},
            {GameObject.Empty, GameObject.Empty, GameObject.Empty},
        }));
        Assert.That(game.CurPlayer, Is.EqualTo(GameObject.Circle));
        Assert.That(game.Winner, Is.EqualTo(GameObject.Empty));
        Assert.That(game.IsFinished, Is.EqualTo(false));

        game.MakeMove(0, 1);
        Assert.That(game.Field, Is.EqualTo(new GameObject[,]{
            {GameObject.Cross, GameObject.Circle, GameObject.Empty},
            {GameObject.Empty, GameObject.Empty, GameObject.Empty},
            {GameObject.Empty, GameObject.Empty, GameObject.Empty},
        }));
        Assert.That(game.CurPlayer, Is.EqualTo(GameObject.Cross));
        Assert.That(game.Winner, Is.EqualTo(GameObject.Empty));
        Assert.That(game.IsFinished, Is.EqualTo(false));

        game.MakeMove(1, 0);
        Assert.That(game.Field, Is.EqualTo(new GameObject[,]{
            {GameObject.Cross, GameObject.Circle, GameObject.Empty},
            {GameObject.Cross, GameObject.Empty, GameObject.Empty},
            {GameObject.Empty, GameObject.Empty, GameObject.Empty},
        }));
        Assert.That(game.CurPlayer, Is.EqualTo(GameObject.Circle));
        Assert.That(game.Winner, Is.EqualTo(GameObject.Empty));
        Assert.That(game.IsFinished, Is.EqualTo(false));

        game.MakeMove(1, 1);
        Assert.That(game.Field, Is.EqualTo(new GameObject[,]{
            {GameObject.Cross, GameObject.Circle, GameObject.Empty},
            {GameObject.Cross, GameObject.Circle, GameObject.Empty},
            {GameObject.Empty, GameObject.Empty, GameObject.Empty},
        }));
        Assert.That(game.CurPlayer, Is.EqualTo(GameObject.Cross));
        Assert.That(game.Winner, Is.EqualTo(GameObject.Empty));
        Assert.That(game.IsFinished, Is.EqualTo(false));

        game.MakeMove(2, 0);
        Assert.That(game.Field, Is.EqualTo(new GameObject[,]{
            {GameObject.Cross, GameObject.Circle, GameObject.Empty},
            {GameObject.Cross, GameObject.Circle, GameObject.Empty},
            {GameObject.Cross, GameObject.Empty, GameObject.Empty},
        }));
        Assert.That(game.Winner, Is.EqualTo(GameObject.Cross));
        Assert.That(game.IsFinished, Is.EqualTo(true));
    }

    [Test]
    public void FullFieldResultsInTie()
    {
        game.MakeMove(0, 0);
        game.MakeMove(0, 1);
        game.MakeMove(1, 0);
        game.MakeMove(1, 1);
        game.MakeMove(2, 1);
        game.MakeMove(2, 0);
        game.MakeMove(0, 2);
        game.MakeMove(1, 2);
        game.MakeMove(2, 2);
        
        Assert.That(game.Winner, Is.EqualTo(GameObject.Empty));
        Assert.That(game.IsFinished, Is.EqualTo(true));
    }
}