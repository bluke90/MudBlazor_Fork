// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using ProtonBlazor.Docs.Components;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Docs.Documentation;

/// <summary>
/// Tests for the <see cref="ApiTypeLink"/> component.
/// </summary>
[TestFixture]
public sealed class ApiTypeLinkTests : BunitTest
{
    /// <summary>
    /// Renders booleans.
    /// </summary>
    [Test]
    public void ApiTypeLink_Boolean()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Boolean"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">bool</code>");
    }

    /// <summary>
    /// Renders boolean arrays.
    /// </summary>
    [Test]
    public void ApiTypeLink_BooleanArray()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Boolean[]"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">bool[]</code>");
    }

    /// <summary>
    /// Renders integers.
    /// </summary>
    [Test]
    public void ApiTypeLink_Int32()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Int32"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">int</code>");
    }

    /// <summary>
    /// Renders int arrays.
    /// </summary>
    [Test]
    public void ApiTypeLink_Int32Array()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Int32[]"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">int[]</code>");
    }

    /// <summary>
    /// Renders longs.
    /// </summary>
    [Test]
    public void ApiTypeLink_Int64()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Int64"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">long</code>");
    }

    /// <summary>
    /// Renders long arrays.
    /// </summary>
    [Test]
    public void ApiTypeLink_Int64Array()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Int64[]"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">long[]</code>");
    }

    /// <summary>
    /// Renders strings.
    /// </summary>
    [Test]
    public void ApiTypeLink_String()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.String"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">string</code>");
    }

    /// <summary>
    /// Renders string arrays.
    /// </summary>
    [Test]
    public void ApiTypeLink_StringArray()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.String[]"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">string[]</code>");
    }

    /// <summary>
    /// Renders doubles.
    /// </summary>
    [Test]
    public void ApiTypeLink_Double()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Double"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">double</code>");
    }

    /// <summary>
    /// Renders double arrays.
    /// </summary>
    [Test]
    public void ApiTypeLink_DoubleArray()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Double[]"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">double[]</code>");
    }

    /// <summary>
    /// Renders floats.
    /// </summary>
    [Test]
    public void ApiTypeLink_Single()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Single"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">float</code>");
    }

    /// <summary>
    /// Renders float arrays.
    /// </summary>
    [Test]
    public void ApiTypeLink_SingleArray()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Single[]"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">float[]</code>");
    }

    /// <summary>
    /// Renders objects.
    /// </summary>
    [Test]
    public void ApiTypeLink_Object()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Object"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">object</code>");
    }

    /// <summary>
    /// Renders object arrays.
    /// </summary>
    [Test]
    public void ApiTypeLink_ObjectArray()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Object[]"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">object[]</code>");
    }

    /// <summary>
    /// Renders void return types.
    /// </summary>
    [Test]
    public void ApiTypeLink_Void()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Void"));

        comp.Markup.Should().Be("<code class=\"docs-code docs-code-primary\">void</code>");
    }

    /// <summary>
    /// Renders ProtonBlazor component links.
    /// </summary>
    [Test]
    public void ApiTypeLink_ProtonBlazor_Component()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "ProtonBlazor.ProAlert"));

        comp.Markup.Should().Contain("<a href=\"/api/ProAlert\" blazor:onclick=\"6\" class=\"pro-typography pro-link pro-primary-text pro-link-underline-hover pro-typography-body1 docs-link docs-code docs-code-primary\">ProAlert</a>", "There should be a link to ProAlert");
    }

    /// <summary>
    /// Renders ProtonBlazor enum links.
    /// </summary>
    [Test]
    public void ApiTypeLink_ProtonBlazor_Enums()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "ProtonBlazor.Adornment"));

        comp.Markup.Should().Contain("<a href=\"/api/Adornment\"", "There should be a link to Adornment");

        comp.Markup.Should().Contain("class=\"pro-typography pro-link pro-primary-text pro-link-underline-hover pro-typography-body1 docs-link docs-code docs-code-primary\">Adornment</a>", "There should be a link to Adornment");
    }

    /// <summary>
    /// Renders links to external Microsoft types.
    /// </summary>
    [Test]
    public void ApiTypeLink_External_MicrosoftType()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "Microsoft.AspNetCore.Components.RenderFragment"));

        comp.Markup.Should().Contain("<a href=\"https://learn.microsoft.com/dotnet/api/Microsoft.AspNetCore.Components.RenderFragment\" target=\"_external\" blazor:onclick=\"1\" class=\"pro-typography pro-link pro-primary-text pro-link-underline-hover pro-typography-caption docs-link docs-code docs-code-primary\">", "There should be a link to Microsoft docs");

        comp.Markup.Should().Contain("<svg class=\"pro-icon-root pro-icon-default pro-svg-icon pro-icon-size-small\" style=\"position:relative;top:7px;\" focusable=\"false\" viewBox=\"0 0 24 24\" aria-hidden=\"true\" role=\"img\"><path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M3.9 12c0-1.71 1.39-3.1 3.1-3.1h4V7H7c-2.76 0-5 2.24-5 5s2.24 5 5 5h4v-1.9H7c-1.71 0-3.1-1.39-3.1-3.1zM8 13h8v-2H8v2zm9-6h-4v1.9h4c1.71 0 3.1 1.39 3.1 3.1s-1.39 3.1-3.1 3.1h-4V17h4c2.76 0 5-2.24 5-5s-2.24-5-5-5z\"/></svg>", "There should be a Link icon");
    }

    /// <summary>
    /// Renders links to external system types.
    /// </summary>
    [Test]
    public void ApiTypeLink_External_SystemType()
    {
        var comp = Context.Render<ApiTypeLink>(parameters => parameters.Add(x => x.TypeName, "System.Guid"));

        comp.Markup.Should().Contain("<a href=\"https://learn.microsoft.com/dotnet/api/System.Guid\" target=\"_external\" blazor:onclick=\"1\" class=\"pro-typography pro-link pro-primary-text pro-link-underline-hover pro-typography-caption docs-link docs-code docs-code-primary\">", "There should be a link to Microsoft docs");

        comp.Markup.Should().Contain("<svg class=\"pro-icon-root pro-icon-default pro-svg-icon pro-icon-size-small\" style=\"position:relative;top:7px;\" focusable=\"false\" viewBox=\"0 0 24 24\" aria-hidden=\"true\" role=\"img\"><path d=\"M0 0h24v24H0z\" fill=\"none\"/><path d=\"M3.9 12c0-1.71 1.39-3.1 3.1-3.1h4V7H7c-2.76 0-5 2.24-5 5s2.24 5 5 5h4v-1.9H7c-1.71 0-3.1-1.39-3.1-3.1zM8 13h8v-2H8v2zm9-6h-4v1.9h4c1.71 0 3.1 1.39 3.1 3.1s-1.39 3.1-3.1 3.1h-4V17h4c2.76 0 5-2.24 5-5s-2.24-5-5-5z\"/></svg>", "There should be a Link icon");
    }
}
