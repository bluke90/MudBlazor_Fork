using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace ProtonBlazor;

/// <summary>
/// Indicates the clipping behavior of a <see cref="ProDrawer"/> when inside of a <see cref="ProLayout"/>.
/// </summary>
[EnumExtensions]
public enum DrawerClipMode
{
    /// <summary>
    /// The drawer will display over the <see cref="ProAppBar"/> and other content.
    /// </summary>
    [Description("never")]
    Never,

    /// <summary>
    /// The drawer will display underneath the <see cref="ProAppBar"/> and push content to the side when opening.
    /// </summary>
    [Description("docked")]
    Docked,

    /// <summary>
    /// The drawer will display underneath the <see cref="ProAppBar"/> and display over content when opened.
    /// </summary>
    [Description("always")]
    Always
}
