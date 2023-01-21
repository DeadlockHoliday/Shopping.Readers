internal static class ResultWriter
{
    internal static void WriteResult(IEnumerable<OrderItem> items, string writePath)
    {
        using var outStream = File.OpenWrite(writePath);
        using var textWriter = new StreamWriter(outStream);

        foreach (var item in items)
        {
            textWriter.WriteLine(item);
        }
    }
}
