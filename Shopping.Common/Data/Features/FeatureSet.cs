using System.Text.Json.Nodes;

namespace Shopping.Common.Data.Features;

public class FeatureSet
{
    protected readonly IDictionary<string, JsonNode?> features;

    public FeatureSet()
    {
        features = new Dictionary<string, JsonNode?>();
    }

    public FeatureSet(FeatureSet other)
    {
        features = other.features.ToDictionary(x => x.Key, x => x.Value);
    }

    public FeatureSet(params FeatureSet[] others)
    {
        features = others!.SelectMany(x => x.features)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    protected TValue TryGet<TValue>(string key, TValue fallback)
    {
        if (features.TryGetValue(key, out var node))
        {
            return node!.GetValue<TValue>();
        }

        return fallback;
    }
}