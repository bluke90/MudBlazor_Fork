// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.UnitTests.Analyzers.Verifiers;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Analyzers;

extern alias ProtonBlazorAnalyzer;
using VerifyCS = CSharpCodeFixVerifier<
    ProtonBlazorAnalyzer::ProtonBlazor.Analyzers.ParameterStateAnalyzer,
    ProtonBlazorAnalyzer::ProtonBlazor.Analyzers.ParameterStateCodeFixProvider>;

/// <summary>
/// Tests for ParameterStateCodeFixProvider following Microsoft's code fix testing patterns.
/// See: https://github.com/dotnet/samples/tree/main/csharp/roslyn-sdk/Tutorials/MakeConst
/// </summary>
[TestFixture]
public class ParameterStateCodeFixProviderTests
{
    [Test]
    public async Task MUD0012_ExternalAccess_CodeFix_ReplacesWithGetState()
    {
        var source = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public int GetExternalCounter()
    {
        return {|MUD0012:_componentA.Counter|};
    }
}";

        var fixedSource = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public int GetExternalCounter()
    {
        return _componentA.GetState(x => x.Counter);
    }
}";

        await VerifyCS.VerifyCodeFixAsync(source, fixedSource);
    }

    [Test]
    public async Task MUD0012_ExternalAccess_InVariableAssignment_CodeFix_ReplacesWithGetState()
    {
        var source = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public string Name { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public void Method()
    {
        var x = {|MUD0012:_componentA.Name|};
    }
}";

        var fixedSource = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public string Name { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public void Method()
    {
        var x = _componentA.GetState(x => x.Name);
    }
}";

        await VerifyCS.VerifyCodeFixAsync(source, fixedSource);
    }

    [Test]
    public async Task MUD0012_ExternalAccess_AsMethodArgument_CodeFix_ReplacesWithGetState()
    {
        var source = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public void Method()
    {
        DoSomething({|MUD0012:_componentA.Counter|});
    }

    private void DoSomething(int value) { }
}";

        var fixedSource = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public void Method()
    {
        DoSomething(_componentA.GetState(x => x.Counter));
    }

    private void DoSomething(int value) { }
}";

        await VerifyCS.VerifyCodeFixAsync(source, fixedSource);
    }

    [Test]
    public async Task MUD0012_ExternalAccess_ViaLocalVariable_CodeFix_ReplacesWithGetState()
    {
        var source = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    public void Method()
    {
        var component = new ComponentA();
        var x = {|MUD0012:component.Counter|};
    }
}";

        var fixedSource = @"
using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    public void Method()
    {
        var component = new ComponentA();
        var x = component.GetState(x => x.Counter);
    }
}";

        await VerifyCS.VerifyCodeFixAsync(source, fixedSource);
    }

    [Test]
    public async Task MUD0012_ExternalAccess_AddsUsingDirective_WhenMissing()
    {
        var source = @"using System;
using ProtonBlazor;
using ProtonBlazor.State;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public int GetExternalCounter()
    {
        return {|MUD0012:_componentA.Counter|};
    }
}";

        // SyntaxGenerator.AddNamespaceImports appends at the end
        var fixedSource = @"using System;
using ProtonBlazor;
using ProtonBlazor.State;
using ProtonBlazor.Extensions;

class ComponentA : ComponentBaseWithState
{
    [ProtonBlazor.State.ParameterState]
    public int Counter { get; set; }
}

class ComponentB
{
    private ComponentA _componentA = new ComponentA();

    public int GetExternalCounter()
    {
        return _componentA.GetState(x => x.Counter);
    }
}";

        await VerifyCS.VerifyCodeFixAsync(source, fixedSource);
    }
}
