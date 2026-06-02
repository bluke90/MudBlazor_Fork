// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// A drag handle that restricts drag-and-drop initiation to a specific child element
/// inside a <see cref="ProDynamicDropItem{T}"/>.
/// </summary>
/// <typeparam name="T">The type of item being dragged.</typeparam>
/// <remarks>
/// Place this component anywhere inside a <see cref="ProDropContainer{T}.ItemRenderer"/>. Once
/// registered, the parent item's full-element draggable behavior is suppressed so that
/// only interactions with the handle element start a drag-and-drop transaction.
/// <para>
/// Example — make only the card header draggable:
/// <code lang="razor">
/// &lt;ProDropZone T="MyItem" ...&gt;
///     &lt;ItemRenderer&gt;
///         &lt;ProCard&gt;
///             &lt;ProCardHeader&gt;
///                 &lt;ProDragHandle T="MyItem"&gt;
///                     &lt;ProIcon Icon="@Icons.Material.Filled.DragIndicator" /&gt;
///                 &lt;/ProDragHandle&gt;
///                 &lt;ProText&gt;@context.Title&lt;/ProText&gt;
///             &lt;/ProCardHeader&gt;
///             &lt;ProCardContent&gt;...&lt;/ProCardContent&gt;
///         &lt;/ProCard&gt;
///     &lt;/ItemRenderer&gt;
/// &lt;/ProDropZone&gt;
/// </code>
/// </para>
/// </remarks>
public partial class ProDragHandle<T> : ProComponentBase, IDisposable where T : notnull
{
    private bool _disposedValue = false;

    /// <summary>
    /// The parent drop item provided by the <see cref="ProDynamicDropItem{T}"/> ancestor.
    /// </summary>
    [CascadingParameter]
    private ProDynamicDropItem<T>? DropItem { get; set; }

    /// <summary>
    /// The content displayed inside the drag handle.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.DropZone.Appearance)]
    public RenderFragment? ChildContent { get; set; }

    protected string Classname =>
        new CssBuilder("pro-drag-handle")
            .AddClass(Class)
            .Build();

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (DropItem is null)
        {
            throw new InvalidOperationException(
                $"{nameof(ProDragHandle<T>)} must be placed inside a {nameof(ProDynamicDropItem<T>)}.");
        }

        base.OnInitialized();
        DropItem.RegisterDragHandle();
    }

    private Task OnDragStartedAsync() => DropItem?.DragStartedAsync() ?? Task.CompletedTask;

    private Task OnDragEndedAsync(DragEventArgs e) => DropItem?.DragEndedAsync() ?? Task.CompletedTask;

    private Task OnTouchStartedAsync(TouchEventArgs e) => DropItem?.TouchStartedAsync(e) ?? Task.CompletedTask;

    private Task OnTouchMovedAsync(TouchEventArgs e) => DropItem?.TouchMovedAsync(e) ?? Task.CompletedTask;

    private Task OnTouchEndedAsync(TouchEventArgs e) => DropItem?.TouchEndedAsync(e) ?? Task.CompletedTask;

    /// <summary>
    /// Releases resources used by this drag handle and unregisters it from the parent item.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                DropItem?.UnregisterDragHandle();
            }

            _disposedValue = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
