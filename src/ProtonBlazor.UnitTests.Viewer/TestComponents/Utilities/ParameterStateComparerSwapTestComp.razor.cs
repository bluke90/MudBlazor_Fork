// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.State;

namespace ProtonBlazor.UnitTests;

public partial class ParameterStateComparerSwapTestComp : ProComponentBase
{
    private readonly List<ParameterChangedEventArgs<double>> _parameterChanges = new();

    [Parameter]
    public double DoubleParam { get; set; }

    [Parameter]
    public IEqualityComparer<double> DoubleEqualityComparer { get; set; } = new DoubleEpsilonEqualityComparer(0.0001f);

    public ParameterStateComparerSwapTestComp()
    {
        using var registerScope = CreateRegisterScope();
        registerScope.RegisterParameter<double>(nameof(DoubleParam))
            .WithParameter(() => DoubleParam)
            .WithChangeHandler(ParameterChangedHandler)
            .WithComparer(() => DoubleEqualityComparer);
    }

    private void ParameterChangedHandler(ParameterChangedEventArgs<double> args)
    {
        _parameterChanges.Add(args);
    }
}
