namespace Common.Algorithm.Core.Text.Distance;

public class CalculationOptions
{
    public LcsOptions? LcsOptions { get; set; }
}

public class LcsOptions
{
    public bool ContainsOnlyLongestSubSequences { get; set; } = true;
}
