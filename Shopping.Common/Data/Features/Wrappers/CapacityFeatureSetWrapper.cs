using System.Text.Json.Nodes;

namespace Shopping.Common.Data.Features.Wrappers;

public class CapacityFeatureSetWrapper : FeatureSetWrapperBase
{
    public CapacityFeatureSetWrapper(JsonObject featureSet) : base(featureSet)
    { 
    }

    public long MassGramms
    {
        get => TryGet(nameof(MassGramms), 0L);
        set => Set(nameof(MassGramms), () => value);
    }

    public long Pieces
    {
        get => TryGet(nameof(Pieces), 0L);
        set => Set(nameof(Pieces), () => value);
    }
}