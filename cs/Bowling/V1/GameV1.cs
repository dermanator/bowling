namespace Bowling.V1;

public class GameV1
{
    private FrameV1[] Rounds;
    public int CurrentRound { get; private set; }
    private int CurrentRoundIndex => CurrentRound - 1;

    public GameV1()
    {
        Rounds = Enumerable.Range(1, 10).Select(r => new FrameV1(r)).ToArray();
        for (var i = 0; i < 10; i++)
        {
            if (i < 9)
            {
                Rounds[i].SetNextRound(Rounds[i + 1]);
            }
            if (i > 0)
            {
                Rounds[i].SetPreviousRound(Rounds[i - 1]);
            }
        }
    }

    public void ResetGame()
    {
        CurrentRound = 1;
        foreach (var round in Rounds)
        {
            round.ResetFrame();
        }
    }

    public bool GameOver => CurrentRound == 10 && Rounds[CurrentRoundIndex].FrameOver;

    public void EnterScore(int score)
    {
        if (GameOver) return;

        var round = Rounds[CurrentRoundIndex];
        round.EnterScore(score);

        if (round.FrameOver && !GameOver)
        {
            CurrentRound++;
        }
    }

    public override string ToString()
    {
        return string.Join("\n\n", Rounds.Select(r => r.ToString()));
    }
}
