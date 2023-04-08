namespace Bowling.V2;

public class Frame
{
    protected Frame? _nextFrame;

    protected Frame? _prevFrame;

    public int? Chance1 { get; protected set; }

    public int? Chance2 { get; protected set; }

    public int FrameNumber { get; private set; }

    public Frame(int frameNumber)
    {
        FrameNumber = frameNumber;
    }

    public void SetPreviousFrame(Frame? prevFrame)
    {
        _prevFrame = prevFrame;
    }

    public void SetNextFrame(Frame? nextFrame)
    {
        _nextFrame = nextFrame;
    }

    public virtual int? SubTotal => FrameOver ? Chance1.GetValueOrDefault() + Chance2.GetValueOrDefault() : null;

    public bool IsStrike => Chance1 == 10;

    public bool IsSpare => Chance1 != 10 && Chance1 + Chance2 == 10;

    protected virtual int? PreviousFrameBonus1 => Chance1;
    
    protected virtual int? PreviousFrameBonus2 => Chance2 ?? _nextFrame?.PreviousFrameBonus1;

    public virtual int? FrameTotal => SubTotal +
        (IsStrike || IsSpare ? _nextFrame?.PreviousFrameBonus1 : 0) +
        (IsStrike ? _nextFrame?.PreviousFrameBonus2 : 0);

    public int? AccumulatedTotal => (_prevFrame != null ? _prevFrame.AccumulatedTotal : 0) + FrameTotal;

    protected bool ValidateChance(int chance)
    {
        return chance > -1 && chance < 11;
    }

    protected virtual bool CanEnterScore(int score)
    {
        if (score + Chance1.GetValueOrDefault() > 10) return false;
        return true;
    }

    protected virtual void RecordScore(int score)
    {
        if (!Chance1.HasValue)
        {
            Chance1 = score;

            if (IsStrike)
            {
                // Chance2 = 0;
            }
        }
        else
        {
            Chance2 = score;
        }
    }

    public virtual void EnterScore(int score)
    {
        if (FrameOver) return;
        if (!ValidateChance(score)) return;
        if (!CanEnterScore(score)) return;

        RecordScore(score);
    }

    public virtual void ResetFrame()
    {
        Chance1 = null;
        Chance2 = null;
    }

    public virtual bool FrameOver => Chance1 == 10 || Chance2.HasValue;

    public override string ToString()
    {
        return $"Frame: {FrameNumber.ToString()}".PadRight(12) +
            $"Score 1: {(IsStrike ? "X" : Chance1)}".PadRight(14) +
            $"Score 2: {(IsSpare ? "\\" : !IsStrike ? Chance2 : "")}".PadRight(14) +
            String.Empty.PadRight(14) +
            $"Sub Total: {SubTotal}".PadRight(17) +
            $"Frame Total: {FrameTotal}".PadRight(19) +
            $"Acc. Total: {AccumulatedTotal}";
    }
}
