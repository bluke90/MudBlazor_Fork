// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using ProtonBlazor.Docs.Components;
using ProtonBlazor.Docs.Models;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Docs.Documentation;

/// <summary>
/// Tests for the <see cref="ApiMemberTable"/> component.
/// </summary>
[TestFixture]
public sealed class ApiSeeAlsoLinksTests : BunitTest
{
    /// <summary>
    /// Renders <see cref="ApiMemberTableMode.SeeAlso"/> when see-also links exist.
    /// </summary>
    /// <remarks>
    /// At the time of writing this test, there are see-also links for <see cref="ProButton"/>.
    /// </remarks>
    [Test]
    public void ApiSeeAlsoLinks_RenderSeeAlso_WhenExisting()
    {
        // Get a type with see-also links
        var mudButton = ApiDocumentation.GetType("ProtonBlazor.ProButton");
        using var comp = Context.Render<ApiSeeAlsoLinks>(parameters => parameters.Add(x => x.Type, mudButton));

        comp.Markup.Should().Contain("<a href=\"/api/ProButtonGroup\"", "There should be a see-also link to ProButtonGroup");

        comp.Markup.Should().Contain("class=\"pro-typography pro-link pro-primary-text pro-link-underline-hover pro-typography-body1 docs-link docs-code docs-code-primary\">ProButtonGroup</a>", "There should be a see-also link to ProButtonGroup");

        comp.Markup.Should().NotContain("<div class=\"pro-alert-message\">No see-also links match the current filters.</div>", "There should NOT be a message saying no members are found");
    }

    /// <summary>
    /// Renders the empty state in <see cref="ApiMemberTableMode.SeeAlso"/> when no see-also links exist.
    /// </summary>
    /// <remarks>
    /// At the time of writing this test, there are no see-also links for <see cref="ProAlert"/>.
    /// </remarks>
    [Test]
    public void ApiSeeAlsoLinks_RenderSeeAlso_WhenNotExisting()
    {
        // Get a type with no see-also links
        var mudAlert = ApiDocumentation.GetType("ProtonBlazor.ProAlert");
        using var comp = Context.Render<ApiSeeAlsoLinks>(parameters => parameters.Add(x => x.Type, mudAlert));

        comp.Markup.Should().NotContain("<div class=\"pro-alert-message\">No see-also links match the current filters.</div>", "the current assertion expects no empty-state message");
    }
}
