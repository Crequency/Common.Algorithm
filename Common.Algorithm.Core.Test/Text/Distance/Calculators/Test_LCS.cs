using System.Diagnostics;
using System.Text;
using Common.Algorithm.Core.Text.Distance.Calculators;

namespace Common.Algorithm.Core.Test.Text.Distance.Calculators;

[TestClass]
public class Test_LCS
{
    [TestMethod]
    public void TestGetDistanceInfo()
    {
        var distanceInfo = new LCS().GetDistanceInfo(inputs: ["ABCBDAB", "BDCABA"]);
        Assert.AreEqual(4, distanceInfo.Distance);
        Assert.IsNotNull(distanceInfo.LcsInfo);
        Assert.IsNotNull(distanceInfo.LcsInfo.LcsMatchedSubSequences);
        var content = new StringBuilder();
        foreach (var pair in distanceInfo.LcsInfo.LcsMatchedSubSequences)
        {
            content.AppendLine($"+ {pair.Key}: ");
            foreach (var sequence in pair.Value)
            {
                content.AppendLine($"    - {sequence}");
            }
        }
        Debug.WriteLine(content.ToString());
        var d1 = new Dictionary<int, List<string>>() { { 4, ["BCBA", "BDAB"] } }.Assertable();
        var d2 = distanceInfo.LcsInfo.LcsMatchedSubSequences.Assertable();
        Debug.WriteLine(d1);
        Debug.WriteLine(d2);
        Assert.AreEqual(d1, d2);
    }
}

public static class LcsTestUtils
{
    public static string Assertable(this Dictionary<int, List<string>> dict)
    {
        return string.Join(
            '\n',
            dict.OrderBy(p => p.Key)
                .Select(p => $"{p.Key}: {string.Join(',', p.Value.OrderBy(s => s))};")
        );
    }
}
