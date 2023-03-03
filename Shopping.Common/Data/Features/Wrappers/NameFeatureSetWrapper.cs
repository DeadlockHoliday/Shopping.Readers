using System.Text.Json.Nodes;

namespace Shopping.Common.Data.Features.Wrappers;

public class NameFeatureSetWrapper : FeatureSetWrapperBase
{
    public NameFeatureSetWrapper(IDictionary<string, JsonNode?> featureSet) : base(featureSet)
    {
    }

    public string? GroupingName
    {
        get => TryGet(nameof(GroupingName), (string?) null);
        set => Set(nameof(GroupingName), () => value);
    }
}