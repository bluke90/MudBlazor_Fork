// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.Utilities;


namespace ProtonBlazor.Charts;

/// <summary>
/// Represents a node in a Sankey diagram, including its name, column position, and optional color.
/// </summary>
/// <remarks>
/// A Sankey diagram node is a visual element that represents a specific entity or category in
/// the diagram. Each node must have a unique name to ensure proper identification and linking within the
/// diagram.
/// </remarks>
/// <param name="Name">The name of this node.</param>
/// <param name="Column">The column in which to display this node.</param>
/// <param name="Color">The color of this node. Picks colors from <see cref="IChartOptions.ChartPalette"/> if set to <c>null</c>.</param>
public record SankeyNode(string Name, int Column, ProColor? Color = null)
{
    public ProColor? Color { get; set; } = Color;
}
