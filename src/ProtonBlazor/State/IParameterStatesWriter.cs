// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.State.Builder;

namespace ProtonBlazor.State;

/// <summary>
/// Represents a writer for parameter states.
/// </summary>
internal interface IParameterStatesWriter
{
    /// <summary>
    /// Adds a parameter builder to the list for processing.
    /// </summary>
    /// <param name="builder">The parameter builder to add.</param>
    void WriteParameter(IParameterBuilderAttach builder);
}
