namespace Common.Algorithm.Core.Text.Distance;

public class DistanceInfo
{
    public List<string>? OriginalInputs { get; set; }

    public double Distance { get; set; }

    public LcsInfo? LcsInfo { get; set; }
}

public class LcsInfo
{
    /// <summary>
    /// The matched sub-sequences from LCS algorithm
    /// </summary>
    /// <example>
    /// When calculate distance of `(abbabbc, abbac)`, the result will be:
    /// 5, ['abbac']
    /// 4, ['abba', 'abbc', 'bbac']
    /// ...
    ///
    /// Only longest sub-sequences will be added if you indicated
    /// </example>
    public Dictionary<int, List<string>>? LcsMatchedSubSequences { get; set; }
}
