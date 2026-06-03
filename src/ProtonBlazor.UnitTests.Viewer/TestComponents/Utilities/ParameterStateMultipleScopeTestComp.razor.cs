// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.State;

namespace ProtonBlazor.UnitTests;

public partial class ParameterStateMultipleScopeTestComp : ProComponentBase
{
    public ParameterStateMultipleScopeTestComp()
    {
        using (var registerScope1 = CreateRegisterScope())
        {
            _a = registerScope1.RegisterParameter<int>(nameof(A))
                .WithParameter(() => A);
            _b = registerScope1.RegisterParameter<int>(nameof(B))
                .WithParameter(() => B);
        }

        using (var registerScope2 = CreateRegisterScope())
        {
            _c = registerScope2.RegisterParameter<int>(nameof(C))
                .WithParameter(() => C);
        }
    }

    private readonly ParameterState<int> _a;
    private readonly ParameterState<int> _b;
    private readonly ParameterState<int> _c;

    [Parameter, ParameterState]
    public int A { get; set; }

    [Parameter, ParameterState]
    public int B { get; set; }

    [Parameter, ParameterState]
    public int C { get; set; }
}
