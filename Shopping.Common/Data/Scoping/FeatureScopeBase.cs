using System.Text.Json.Nodes;

namespace Shopping.Common.Data.Scoping;

public class FeatureScopeBase
{
    protected readonly JsonObject featureSet;
    public FeatureScopeBase(JsonObject featureSet)
    {
        this.featureSet = featureSet;
    }

    public JsonObject FeatureSet => featureSet;

    public TValue? TryGet<TValue>(string key, TValue? fallback)
    {
        if (featureSet.TryGetPropertyValue(key, out var node))
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