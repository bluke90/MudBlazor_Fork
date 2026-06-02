using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ProtonBlazor.UnitTests.Analyzers.Internal;

extern alias ProtonBlazorAnalyzer;

#nullable enable
internal sealed class TestAnalyzerOptions : AnalyzerConfigOptionsProvider
{
    private TestAnalyzerOptions(Dictionary<string, string>? values) => _values = values ?? [];

    internal static AnalyzerOptions Create(
        ProtonBlazorAnalyzer::ProtonBlazor.Analyzers.AllowedAttributePattern attributeProviderAttribute,
        ImmutableArray<AdditionalText> additionalText, string? attributeList = null)
    {
        return new AnalyzerOptions(additionalText, new TestAnalyzerOptions(new Dictionary<string, string>
        {
            [ProtonBlazorAnalyzer::ProtonBlazor.Analyzers.ProComponentUnknownParametersAnalyzer.AllowedAttributePatternProperty] = attributeProviderAttribute.ToString(),
            [ProtonBlazorAnalyzer::ProtonBlazor.Analyzers.ProComponentUnknownParametersAnalyzer.AllowedAttributeListProperty] = attributeList ?? string.Empty
        }));
    }

    private readonly Dictionary<string, string> _values;

    public override AnalyzerConfigOptions GlobalOptions => new TestAnalyzerConfigOptions(_values);
    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => new TestAnalyzerConfigOptions(_values);
    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => new TestAnalyzerConfigOptions(_values);

    private sealed class TestAnalyzerConfigOptions(Dictionary<string, string> values) : AnalyzerConfigOptions
    {
        public override bool TryGetValue(string key, [NotNullWhen(returnValue: true)] out string? value)
        {
            return values.TryGetValue(key, out value);
        }
    }
}
