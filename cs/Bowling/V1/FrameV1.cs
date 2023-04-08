namespace Bowling.V1;

public class FrameV1
{
    private FrameV1? _nextRound;
    private FrameV1? _prevRound;
    public int? Chance1 { get; private set; }
    public int? Chance2 { get; private set; }
    public int? ExtraChance { get; private set; }
    public int FrameNumber { get; private set; }

    public FrameV1(int frameNumber)
    {
        FrameNumber = frameNumber;
    }

    public void SetPreviousRound(FrameV1? prevRound)
    {
        _prevRound = prevRound;
    }

    public void SetNextRound(FrameV1? nextRound)
    {
        _nextRound = nextRound;
    }

    public int SubTotal => Chance1.GetValueOrDefault() + Chance2.GetValueOrDefault() + ExtraChance.GetValueOrDefault();

    public bool IsStrike => Chance1 == 10;
    public bool IsSecondStrike => Chance2 == 10;

    public bool IsSpare => !IsStrike && SubTotal == 10;

    public bool CanCalculateStrikeBonus => IsStrike &&
        FrameNumber < 10 &&
        (_nextRound?.Chance1.HasValue ?? false) &&
        ((_nextRound?.Chance2.HasValue ?? false) || (_nextRound?._nextRound?.Chance1.HasValue ?? false));

    public int StrikeBonus => CanCalculateStrikeBonus
        ? (_nextRound?.Chance1).GetValueOrDefault() + (_nextRound?.Chance2 ?? _nextRound?._nextRound?.Chance1 ?? 0)
        : 0;

    public int SpareBonus => IsSpare && FrameNumber < 10
                ? _nextRound?.Chance1 ?? 0
                : 0;

    public int RoundTotal => SubTotal + StrikeBonus + SpareBonus;

    public int AccumulatedTotal => (_prevRound?.AccumulatedTotal ?? 0) + RoundTotal;

    public bool EnterScore(int score)
    {
        if (score < 0) return false;
        if (score > 10) return false;
        if (FrameOver) return false;

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

        return true;
    }

    public void ResetFrame()
    {
        Chance1 = null;
        Chance2 = null;
        ExtraChance = null;
    }

    public bool FrameOver =>
        FrameNumber < 10 && Chance1 == 10 ||
        FrameNumber < 10 && Chance2.HasValue ||
        FrameNumber == 10 && ExtraChance.HasValue ||
        FrameNumber == 10 && Chance2.HasValue && !IsSpare && !IsSecondStrike;

    public override string ToString()
    {
        return $"Round: {FrameNumber}" +
            $"\nBowl 1: {Chance1}" +
            (Chance2.HasValue ? $"\nBowl 2: {Chance2}" : "") +
            (ExtraChance.HasValue ? $"\nBowl 3: {ExtraChance}" : "") +
            $"\nRound Total: {RoundTotal}" +
            $"\nAcc. Total: {AccumulatedTotal}";
    }
}
