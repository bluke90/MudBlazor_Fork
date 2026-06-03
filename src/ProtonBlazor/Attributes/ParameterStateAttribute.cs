// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.State;

[AttributeUsage(AttributeTargets.Property)]
public class ParameterStateAttribute : Attribute
{
    public ParameterUsageOptions ParameterUsage { get; set; } = ParameterUsageOptions.All;
}
