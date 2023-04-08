using Bowling.V2;
using FluentAssertions;

namespace Bowling.Test;

public class UnitTestsV2
{
    private readonly Game _game = new();

    [SetUp]
    public void Setup()
    {
        _game.ResetGame();
    }

    [Test]
    public void game_updates_round_number_correctly()
    {
        _game.CurrentFrame.Should().Be(1);
        _game.Score.Should().Be(null);
        _game.GameOver.Should().BeFalse();
        
        _game.EnterScore(1);
        _game.EnterScore(4);
        _game.CurrentFrame.Should().Be(2);
        _game.Score.Should().Be(5);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(4);
        _game.EnterScore(5);
        _game.CurrentFrame.Should().Be(3);
        _game.Score.Should().Be(14);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(6);
        _game.EnterScore(4);
        _game.CurrentFrame.Should().Be(4);
        _game.Score.Should().Be(14);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(5);
        _game.EnterScore(5);
        _game.CurrentFrame.Should().Be(5);
        _game.Score.Should().Be(29);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(10);
        _game.CurrentFrame.Should().Be(6);
        _game.Score.Should().Be(49);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(0);
        _game.EnterScore(1);
        _game.CurrentFrame.Should().Be(7);
        _game.Score.Should().Be(61);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(7);
        _game.EnterScore(3);
        _game.CurrentFrame.Should().Be(8);
        _game.Score.Should().Be(61);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(6);
        _game.EnterScore(4);
        _game.CurrentFrame.Should().Be(9);
        _game.Score.Should().Be(77);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(10);
        _game.CurrentFrame.Should().Be(10);
        _game.Score.Should().Be(97);
        _game.GameOver.Should().BeFalse();

        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.EnterScore(10);
        _game.CurrentFrame.Should().Be(10);
        _game.Score.Should().Be(157);
        _game.GameOver.Should().BeTrue();

        Console.WriteLine();
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