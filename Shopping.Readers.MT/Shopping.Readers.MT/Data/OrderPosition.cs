﻿using Shopping.Readers.Common.Contracts;
using Shopping.Readers.Common.Contracts.Static;

namespace Shopping.Readers.MT.Data;

internal readonly struct OrderPosition : IOrderPosition
{
    public decimal Quantity { get; init; }

    public string Url { get; init; }

    public string Info { get; init; }

    public string CategoryName { get; init; }

    public decimal Price { get; init; } // TODO: Encapsulate Price and CurrencyCode

    public CurrencyCode CurrencyCode => CurrencyCode.RUB;
}