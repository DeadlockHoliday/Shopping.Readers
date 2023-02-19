using Shopping.Normalizing.Data;
using Shopping.Normalizing.Processing.Names;
using Shopping.Normalizing.Processing.Units;

namespace Shopping.Normalizing;

public static class SupplyNormalizer
{
    public static SupplyItem NormalizeLine(string line)
    {
        var units = UnitExtractor.Extract(line)
            .Select(UnitNormalizer.Normalize)
            .ToArray();
        
        return new()
        {
            Pieces = GetUnit(units, "шт"),
            MassGramms = GetUnit(units, "г"),
            GroupingName = NameExtractor.Extract(line)
        };
    }

    private static ulong GetUnit(Unit[] units, string name)
    {
        var unit = units.FirstOrDefault(x => x.Measure == name);
        return (unit.Value != 0) switch
        {
            true => (ulong)unit.Value,
            _ => 0uL
        };
    }
}