namespace Common.Algorithm.Core.Text.Distance;

public interface IDistanceCalculator
{
    public DistanceInfo GetDistanceInfo(
        List<string> inputs,
        CalculationOptions options
        );
}