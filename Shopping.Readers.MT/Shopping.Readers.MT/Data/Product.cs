using Shopping.Readers.Common.Contracts;

namespace Shopping.Readers.MT.Data;

internal readonly struct Product : IProduct
{
    public string Info { get; init; }

    public string CategoryName { get; init; }
}