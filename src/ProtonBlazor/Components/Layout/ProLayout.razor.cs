using ProtonBlazor.Utilities;

namespace ProtonBlazor;


/// <summary>
/// A component which defines a common structure for multiple pages.
/// </summary>
/// <remarks>
/// Layouts often contain <see cref="ProAppBar"/> and <see cref="ProDrawer"/> components.  The <see cref="ProMainContent"/> component is used to contain page content.  
/// In your layout component, but above this component, add <see cref="ProThemeProvider"/>, <see cref="ProPopoverProvider"/>, <see cref="ProDialogProvider"/>, and <see cref="ProSnackbarProvider"/> components to enable all ProtonBlazor features.
/// </remarks>
/// <seealso cref="ProMainContent"/>
public partial class ProLayout : ProDrawerContainer
{
    protected override string Classname =>
        new CssBuilder("pro-layout")
            .AddClass(base.Classname)
            .Build();

    public ProLayout()
    {
        Fixed = true;
    }
}
