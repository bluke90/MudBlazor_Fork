// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// NOTE: This file is a copy of src/ProtonBlazor/State/ParameterUsageOptions.cs
// Keep this file in sync with the original.
// Analyzers cannot reference ProtonBlazor assembly, so we duplicate the enum here
// following Microsoft's pattern (see ILLink analyzer DynamicallyAccessedMemberTypes.cs).

namespace ProtonBlazor.State;

[Flags]
internal enum ParameterUsageOptions
{
    None = 0,
    Read = 1 << 1,
    Write = 1 << 2,
    All = Read | Write
}
