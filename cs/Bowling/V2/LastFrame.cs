namespace Bowling.V2;

public class LastFrame : Frame
{
    public int? ExtraChance { get; private set; }

    public LastFrame(int frameNumber) : base(frameNumber) { }

    private bool HasSecondStrike => Chance2 == 10;

    private bool HasThirdStrike => ExtraChance == 10;

    public override int? SubTotal => FrameOver ? base.SubTotal.GetValueOrDefault() + ExtraChance.GetValueOrDefault() : null;

    protected override int? PreviousFrameBonus1 => base.PreviousFrameBonus1;

    protected override int? PreviousFrameBonus2 => Chance2; //  Will eventually have a value even if first chance is a stike

    public override int? FrameTotal => SubTotal;  //  No future frames to look at

    public override bool FrameOver => ExtraChance.HasValue || base.FrameOver && Chance1 + Chance2 < 10;

    public override void EnterScore(int score)
    {
        if (!CanEnterScore(score)) return;

        if (!Chance1.HasValue)
        {
            Chance1 = score;
        }
        else if (!Chance2.HasValue)
        {
            Chance2 = score;
        }
        else
        {
            ExtraChance = score;
        }
    }

    public override void ResetFrame()
    {
        base.ResetFrame();
        ExtraChance = null;
    }

    protected override bool CanEnterScore(int score)
    {
        //  Base has already validated score value and that the frame is not over
        if (!(IsStrike || Chance2.HasValue))
        {
            return base.CanEnterScore(score);
        }
        return true;
    }

    protected override void RecordScore(int score)
    {
        if (!Chance1.HasValue)
        {
            Chance1 = score;
        }
        else if (!Chance2.HasValue)
        {
            Chance2 = score;
        }
        else
        {
            ExtraChance = score;
        }
    }

    public override string ToString()
    {
        return $"Frame: {FrameNumber.ToString()},".PadRight(12) +
            $"Score 1: {(IsStrike ? "X" : Chance1)},".PadRight(14) +
            $"Score 2: {(IsSpare ? "\\" : HasSecondStrike ? "X" : Chance2)},".PadRight(14) +
            $"Score 3: {(HasThirdStrike ? "X" : ExtraChance)},".PadRight(14) +
            $"Sub Total: {SubTotal},".PadRight(17) +
            $"Frame Total: {FrameTotal},".PadRight(19) +
            $"Acc. Total: {AccumulatedTotal}";
    }
}
