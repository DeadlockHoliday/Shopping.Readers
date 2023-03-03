using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;

namespace Shopping.Common.Data;

public readonly record struct Product
{
    public bool IsProcessed => !string.IsNullOrEmpty(ProcessorVersion);
    public string ProcessorVersion { get; init; }
    public required string Info { get; init; }
    public required string Category { get; init; }
    public readonly JsonObject Features { get; init; }
}