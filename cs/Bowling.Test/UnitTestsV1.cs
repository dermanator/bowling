using Bowling.V1;
using FluentAssertions;

namespace Bowling.Test;

public class UnitTestsV1
{
    private readonly GameV1 _game = new();

    [SetUp]
    public void Setup()
    {
        _game.ResetGame();
    }

    [Test]
    public void game_updates_round_number_correctly()
    {
        _game.EnterScore(1);
        _game.EnterScore(4);

        _game.EnterScore(4);
        _game.EnterScore(5);

        _game.EnterScore(6);
        _game.EnterScore(4);

        _game.EnterScore(5);
        _game.EnterScore(5);

        _game.EnterScore(10);

        _game.EnterScore(0);
        _game.EnterScore(1);

        _game.EnterScore(7);
        _game.EnterScore(3);

        _game.EnterScore(6);
        _game.EnterScore(4);

        _game.EnterScore(10);

        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        
        Console.WriteLine(_game);
    }

    [Test]
    public void game_handles_perfect_score()
    {
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        
        Console.WriteLine(_game);
    }
}