using System.Text.Json.Nodes;

namespace Shopping.Common.Data.Features.Wrappers;

public class FeatureSetWrapperBase
{
    protected readonly IDictionary<string, JsonNode?> featureSet;
    public FeatureSetWrapperBase(IDictionary<string, JsonNode?> featureSet)
    {
        this.featureSet = featureSet;
    }

    public IDictionary<string, JsonNode?> FeatureSet => featureSet;

    public TValue? TryGet<TValue>(string key, TValue? fallback)
    {
        if (featureSet.TryGetValue(key, out var node))
        {
            return node!.GetValue<TValue>();
        }

        return fallback;
    }

    public void Set(string key, Func<JsonNode?> selector)
    {
        featureSet[key] = selector();
    }
}