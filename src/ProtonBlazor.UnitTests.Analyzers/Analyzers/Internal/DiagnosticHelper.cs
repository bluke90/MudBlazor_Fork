// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace ProtonBlazor.UnitTests.Analyzers.Internal;

extern alias ProtonBlazorAnalyzer;

#nullable enable
internal static class DiagnosticHelper
{
    internal static IReadOnlyList<Diagnostic> FilterToClass(this IEnumerable<Diagnostic> diagnostics, string? className)
    {
        var results = new List<Diagnostic>();
        foreach (var diagnostic in diagnostics)
        {
            if (diagnostic.Properties.TryGetValue(ProtonBlazorAnalyzer::ProtonBlazor.Analyzers.ProComponentUnknownParametersAnalyzer.ClassNamePropertyKey, out var cn)
                && string.Equals(cn, className))
            {
                results.Add(diagnostic);
            }
        }

        return results;
    }
}
