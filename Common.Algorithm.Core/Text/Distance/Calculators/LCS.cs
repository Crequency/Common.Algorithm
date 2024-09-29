namespace Common.Algorithm.Core.Text.Distance.Calculators;

public class LCS : IDistanceCalculator
{
    private int Width { get; set; }

    private int Height { get; set; }

    public DistanceInfo GetDistanceInfo(List<string> inputs, CalculationOptions? options = null)
    {
        if (inputs.Count != 2)
            throw new ArgumentOutOfRangeException(
                nameof(inputs),
                "There should be only two inputs"
            );

        options ??= new();

        var result = GetLcsInfo(inputs[0], inputs[1]);

        return new DistanceInfo()
        {
            OriginalInputs = inputs,
            Distance = result.LcsMatchedSubSequences!.Keys.First(),
            LcsInfo = result,
        };
    }

    private LcsInfo GetLcsInfo(string a, string b)
    {
        Width = Math.Max(a.Length, b.Length);
        Height = Math.Min(a.Length, b.Length);

        var sa = a.Length > b.Length ? b : a;
        var sb = a.Length > b.Length ? a : b;

        var calMatrix = new int[Height + 1, Width + 1];
        var dirMatrix = new int[Height + 1, Width + 1];

        var results = new List<string>();

        for (var i = 1; i <= Height; ++i)
        for (var j = 1; j <= Width; ++j)
        {
            var same = sa[i - 1] == sb[j - 1];
            calMatrix[i, j] = (
                same
                    ? calMatrix[i - 1, j - 1] + 1
                    : Math.Max(calMatrix[i, j - 1], calMatrix[i - 1, j])
            );
            dirMatrix[i, j] = same ? 1 : (calMatrix[i - 1, j] >= calMatrix[i, j - 1] ? 2 : 3);
        }

        for (var i = 0; i < Width; ++i)
            results.Add("");

        for (int j = Width; j >= 1; --j)
            Trace(Width - j, Height, Width - (Width - j));

        var m = new Dictionary<string, int>();
        foreach (var result in results)
            if (result.Length == results[0].Length)
                m[result] = 1;
        results.Clear();
        foreach (var pair in m)
            if (pair.Value == 1)
                results.Add(pair.Key);

        return new()
        {
            LcsMatchedSubSequences = new Dictionary<int, List<string>>
            {
                { results[0].Length, results },
            },
        };

        void Trace(int i, int m, int n)
        {
            if (m == 0 || n == 0)
                return;
            switch (dirMatrix[m, n])
            {
                case 1:
                    Trace(i, m - 1, n - 1);
                    results[i] = string.Concat(results[i].Append(sa[m - 1]));
                    break;
                case 2:
                    Trace(i, m - 1, n);
                    break;
                case 3:
                    Trace(i, m, n - 1);
                    break;
            }
        }
    }
}
