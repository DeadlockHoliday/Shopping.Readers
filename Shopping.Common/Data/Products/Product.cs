using Shopping.Common.Data.Features;
using System.Diagnostics.CodeAnalysis;

namespace Shopping.Common.Data.Products;

public readonly record struct Product
{
    public bool IsProcessed => !string.IsNullOrEmpty(ProcessorVersion);
    public string ProcessorVersion { get; init; }
    public required string Info { get; init; }
    public required string Category { get; init; }
    public readonly FeatureSet FeatureSet { get; init; }


    [SetsRequiredMembers]
    public Product(string info, string category)
    {
        Info = info;
        Category = category;
        ProcessorVersion = string.Empty;
        FeatureSet = new FeatureSet();
    }

    public TFeatureSet GetFeatureSet<TFeatureSet>()
        where TFeatureSet : FeatureSet, new()
    {
        if (FeatureSet is TFeatureSet result)
        {
            return result;
        }

        var paramTypes = new Type[] { typeof(FeatureSet) };
        var args = new object[] { FeatureSet };
        var targetType = typeof(TFeatureSet);
        var constructor = targetType
            .GetConstructor(paramTypes);

        if (constructor is null)
        {
            throw new ArgumentException($"Can't find a constructor {targetType.Name}({paramTypes.First().Name}); ");
        }

        return (TFeatureSet) constructor!.Invoke(args);
    }
}