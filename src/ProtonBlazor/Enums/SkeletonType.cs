using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace ProtonBlazor;

/// <summary>
/// Indicates the shape of a <see cref="ProSkeleton"/> component.
/// </summary>
[EnumExtensions]
public enum SkeletonType
{
    /// <summary>
    /// The skeleton is a placeholder for text.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="ProSkeleton.Width"/> and <see cref="ProSkeleton.Height"/> parameters to control its size.
    /// </remarks>
    [Description("text")]
    Text,

    /// <summary>
    /// The skeleton displays a circle shape.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="ProSkeleton.Width"/> and <see cref="ProSkeleton.Height"/> parameters to control its size.
    /// </remarks>
    [Description("circle")]
    Circle,

    /// <summary>
    /// The skeleton displays a rectangle shape.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="ProSkeleton.Width"/> and <see cref="ProSkeleton.Height"/> parameters to control its size.
    /// </remarks>
    [Description("rectangle")]
    Rectangle
}
