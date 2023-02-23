﻿namespace Shopping.Readers.MT.Data;

internal readonly record struct Supply : ISupply
{
    public DateOnly Date { get; init; }

    public Vendor Vendor => Vendor.MT;

    public IReadOnlyList<ISupplyPosition> Positions { get; init; }
}
