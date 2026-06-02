using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace ProtonBlazor;

/// <summary>
/// The horizontal distribution of child items in a <see cref="ProStack"/> component.
/// </summary>
[EnumExtensions]
public enum Justify
{
    /// <summary>
    /// Items are aligned to the start of the <see cref="ProStack"/>.
    /// </summary>
    [Description("start")]
    FlexStart,

    /// <summary>
    /// Items are centered horizontally.
    /// </summary>
    [Description("center")]
    Center,

    /// <summary>
    /// Items are aligned to the end of the <see cref="ProStack"/>.
    /// </summary>
    [Description("end")]
    FlexEnd,

    /// <summary>
    /// Space is applied between each item, with items aligned against the start and end.
    /// </summary>
    [Description("space-between")]
    SpaceBetween,

    /// <summary>
    /// Space is applied between each item, with additional spacing for the first and last item.
    /// </summary>
    [Description("space-around")]
    SpaceAround,

    /// <summary>
    /// Space is applied evenly between each item, including the edges of the first and last item.
    /// </summary>
    [Description("space-evenly")]
    SpaceEvenly
}
