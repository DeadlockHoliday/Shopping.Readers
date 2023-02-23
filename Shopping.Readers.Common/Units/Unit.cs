namespace Shopping.Readers.Common.Units;

public readonly record struct Unit
{
    private const string GramsMeasure = "г";
    private const string PiecesMeasure = "шт";

    public decimal Value { get; init; }
    public string Measure { get; init; }

    public Unit(decimal value, string measure)
    {
        Value = value;
        Measure = measure;
    }

    public static Unit CreateGrams(decimal value)
        => new(value, GramsMeasure);

    public static Unit CreatePieces(decimal value)
        => new(value, PiecesMeasure);

    public bool IsGrams()
        => Measure.Equals(GramsMeasure, StringComparison.CurrentCultureIgnoreCase);

    public bool IsPieces()
        => Measure.Equals(PiecesMeasure, StringComparison.CurrentCultureIgnoreCase);
};