using Shopping.Normalizing.Data;
using Shopping.Normalizing.Processing.Units;

namespace Shopping.Normalizing;

public static class SupplyNormalizer
{
    public static SupplyItem NormalizeLine(string line)
    {
        // 1. Extract Name
        // 2. Extract Mass
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();

        var mass = 0uL;
        var massUnit = units.FirstOrDefault(x => x.Measure == "г");
        if (massUnit.Value != 0)
        {
            mass = (ulong)massUnit.Value;
        }

        // 3. Extract Amount
        return new()
        {
            MassGramms = mass
        };
    }
}