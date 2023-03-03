using System.Text.Json.Nodes;

namespace Shopping.Common.Data.Scoping;

public class NamingFeatureScope : FeatureScopeBase
{
    public NamingFeatureScope(JsonObject featureSet) : base(featureSet)
    {
    }

    public string? GroupingName
    {
        get => TryGet(nameof(GroupingName), (string?)null);
        set => Set(nameof(GroupingName), () => value);
    }
}