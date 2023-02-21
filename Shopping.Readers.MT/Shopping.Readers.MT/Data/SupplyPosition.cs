﻿using NMoneys;
using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Data;

internal readonly struct SupplyPosition : ISupplyPosition
{
    public long Quantity { get; init; }

    public long WeightGramms { get; init; }

    public Money Price { get; init; }

    public string Url { get; init; }

    public IProduct Product { get; init; }
}