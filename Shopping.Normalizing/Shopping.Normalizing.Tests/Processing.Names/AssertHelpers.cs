﻿using Shopping.Normalizing.Processing.Names;

namespace Shopping.Normalizing.Tests.Processing.Names;

internal static class AssertHelpers
{
    internal static void AssertSamples(params Sample[] samples)
    {
        Assert.Multiple(() =>
        {
            foreach (var sample in samples)
            {
                var actual = NameExtractor.Extract(sample.Line) ?? "";
                AssertSample(sample, actual);
            }
        });
    }

    internal static void AssertSample(Sample sample, params string[] actual)
    {
        Assert.That(actual,
                    Is.EquivalentTo(sample.Nouns),
                    $"Failed to process line: {sample.Line}");
    }
}
