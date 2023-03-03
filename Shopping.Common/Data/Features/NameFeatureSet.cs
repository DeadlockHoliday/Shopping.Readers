namespace Shopping.Common.Data.Features;

public class NameFeatureSet : FeatureSet
{
    public NameFeatureSet() : base()
    {
    }

    public NameFeatureSet(FeatureSet featureSet) : base(featureSet)
    {
        
    }

    public string? GroupingName
    {
        get => TryGet(nameof(GroupingName), string.Empty);
        set => features[nameof(GroupingName)] = value;
    }
}