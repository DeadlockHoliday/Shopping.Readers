namespace Shopping.Common.Data.Features;

public sealed class CapacityFeatureSet : FeatureSet
{
    public CapacityFeatureSet() : base()
    {
    }

    public CapacityFeatureSet(FeatureSet featureSet) : base(featureSet)
    {
    }

    public long MassGramms
    {
        get => TryGet(nameof(MassGramms), 0L);
        set => features[nameof(MassGramms)] = value;
    }

    public long Pieces
    {
        get => TryGet(nameof(Pieces), 0L);
        set => features[nameof(Pieces)] = value;
    }
}
