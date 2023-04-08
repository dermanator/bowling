namespace Bowling.V2;

public class Game
{
    private Frame[] Frames;
    public int CurrentFrame { get; private set; }
    private int CurrentFrameIndex => CurrentFrame - 1;

    public Game()
    {
        Frames = Enumerable.Range(1, 10).Select(r => r != 10 ? new Frame(r) : new LastFrame(r)).ToArray();

        for (var i = 0; i < 10; i++)
        {
            if (i < 9)
            {
                Frames[i].SetNextFrame(Frames[i + 1]);
            }
            if (i > 0)
            {
                Frames[i].SetPreviousFrame(Frames[i - 1]);
            }
        }
    }

    public void ResetGame()
    {
        CurrentFrame = 1;
        foreach (var frame in Frames)
        {
            frame.ResetFrame();
        }
    }

    public bool GameOver => CurrentFrame == 10 && Frames[CurrentFrameIndex].FrameOver;

    public int? Score
    {
        get
        {
            var index = CurrentFrameIndex;
            int? accumulatedTotal = null;

            while (index >= 0 && !accumulatedTotal.HasValue)
            {
                accumulatedTotal = Frames[index--].AccumulatedTotal;
            }

            return accumulatedTotal;
        }
    }

    public void EnterScore(int score)
    {
        if (GameOver) return;

        var frame = Frames[CurrentFrameIndex];
        frame.EnterScore(score);

        if (frame.FrameOver && !GameOver)
        {
            CurrentFrame++;
        }
    }

    public override string ToString()
    {
        return string.Join("\n", Frames.Select(r => r.ToString()));
    }
}
