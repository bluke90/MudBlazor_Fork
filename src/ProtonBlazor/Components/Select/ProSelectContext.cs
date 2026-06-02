// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using ProtonBlazor.Extensions;
using ProtonBlazor.Interfaces;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

/// <summary>
/// Manages the communication between <see cref="ProSelect{T}"/> and <see cref="ProSelectItem{T}"/> components.
/// </summary>
/// <typeparam name="T">The type of value being selected.</typeparam>
internal sealed class ProSelectContext<T>
{
    private readonly ProSelect<T> _select;
    private readonly List<ProSelectItem<T>> _items = [];
    private readonly List<Func<IReadOnlyCollection<T?>, Task>> _selectionObservers = [];
    private readonly Dictionary<NullableObject<T?>, ProSelectItem<T>> _valueLookup = new();
    private readonly Dictionary<NullableObject<T?>, ProSelectItem<T>> _shadowLookup = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ProSelectContext{T}"/> class.
    /// </summary>
    /// <param name="select">The parent select component.</param>
    public ProSelectContext(ProSelect<T> select)
    {
        _select = select;
    }

    /// <summary>
    /// Gets the ordered list of all visible items in the dropdown.
    /// </summary>
    /// <remarks>
    /// Exposed as List to support FindIndex operations in the parent component.
    /// </remarks>
    public IReadOnlyList<ProSelectItem<T>> Items => _items;

    /// <summary>
    /// Gets all shadow items (including both visible and hidden items).
    /// </summary>
    public IReadOnlyCollection<ProSelectItem<T>> ShadowItems => _shadowLookup.Values;

    /// <summary>
    /// Gets whether multi-selection is enabled.
    /// </summary>
    public bool MultiSelection => _select.MultiSelection;

    /// <summary>
    /// Gets the current selected values.
    /// </summary>
    public IReadOnlyCollection<T?> SelectedValues => _select.GetSelectedValues() ?? Array.Empty<T?>();

    /// <summary>
    /// Gets the user-supplied value comparer from the parent <see cref="ProSelect{T}"/>, if any.
    /// </summary>
    public IEqualityComparer<T?>? Comparer => _select.Comparer;

    /// <summary>
    /// Registers an item as visible in the dropdown list.
    /// </summary>
    /// <param name="item">The item to register.</param>
    /// <returns><c>true</c> if the item is currently selected; otherwise <c>false</c>.</returns>
    public bool RegisterItem(ProSelectItem<T> item)
    {
        // Add to the ordered list of visible items
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }

        // Add to value lookup for fast access by value
        _valueLookup[item.Value] = item;

        // Note: Do NOT add to _shadowLookup here - that's only for shadow items
        // Shadow items are registered separately via RegisterShadowItem

        // Check if this item's value is currently selected
        return _select.MultiSelection switch
        {
            true => _select.GetSelectedValues()?.Contains(item.Value) == true,
            false => _select.ReadValue?.Equals(item.Value) == true
        };
    }

    /// <summary>
    /// Unregisters an item from the dropdown list.
    /// </summary>
    /// <param name="item">The item to unregister.</param>
    public void UnregisterItem(ProSelectItem<T> item)
    {
        _items.Remove(item);
        _valueLookup.Remove(item.Value);
        // Note: Do NOT remove from _shadowLookup - that's managed by shadow items
    }

    /// <summary>
    /// Registers an item for value-to-RenderFragment lookup only (not visible in dropdown).
    /// </summary>
    /// <remarks>
    /// Used for items with HideContent=true that provide RenderFragments for selected values
    /// that may not be in the visible dropdown list.
    /// </remarks>
    /// <param name="item">The item to register.</param>
    public void RegisterShadowItem(ProSelectItem<T>? item)
    {
        if (item is null)
        {
            return;
        }

        _shadowLookup[item.Value] = item;
        _select.InvalidateFitContent();
    }

    /// <summary>
    /// Unregisters a shadow item.
    /// </summary>
    /// <param name="item">The item to unregister.</param>
    public void UnregisterShadowItem(ProSelectItem<T>? item)
    {
        if (item is null)
        {
            return;
        }

        _shadowLookup.Remove(item.Value);
        _select.InvalidateFitContent();
    }

    /// <summary>
    /// Updates the visible-item lookup when a registered item's <see cref="ProSelectItem{T}.Value"/> parameter changes.
    /// </summary>
    /// <remarks>
    /// The lookup is keyed by value captured at registration time. When the value parameter is later mutated
    /// (e.g. the parent swaps the underlying data collection while reusing the same component instances),
    /// the old key would otherwise point to a stale <see cref="ProSelectItem{T}"/> reference and the new value
    /// would not be found at all.
    /// </remarks>
    public void OnItemValueChanged(ProSelectItem<T> item, T? oldValue, T? newValue)
    {
        var oldKey = new NullableObject<T?>(oldValue);
        if (_valueLookup.TryGetValue(oldKey, out var existing) && ReferenceEquals(existing, item))
        {
            _valueLookup.Remove(oldKey);
        }

        _valueLookup[newValue] = item;
    }

    /// <summary>
    /// Updates the shadow lookup when a registered shadow item's <see cref="ProSelectItem{T}.Value"/> parameter changes.
    /// </summary>
    /// <remarks>
    /// Also requests a re-render of the parent <see cref="ProSelect{T}"/>: <c>GetSelectedValuePresenter</c> runs
    /// during the parent's render pass, before child <c>SetParametersAsync</c> has propagated the new value,
    /// so a second render is needed for the correct <see cref="ProSelectItem{T}.ChildContent"/> to be emitted.
    /// </remarks>
    public void OnShadowItemValueChanged(ProSelectItem<T> item, T? oldValue, T? newValue)
    {
        var oldKey = new NullableObject<T?>(oldValue);
        if (_shadowLookup.TryGetValue(oldKey, out var existing) && ReferenceEquals(existing, item))
        {
            _shadowLookup.Remove(oldKey);
        }

        _shadowLookup[newValue] = item;
        _select.InvalidateFitContent();
        ((IMudStateHasChanged)_select).StateHasChanged();
    }

    /// <summary>
    /// Attempts to get an item by its value from visible items.
    /// </summary>
    public bool TryGetItemByValue(T? value, [NotNullWhen(true)] out ProSelectItem<T>? item)
    {
        return _valueLookup.TryGetValue(value, out item);
    }

    /// <summary>
    /// Attempts to get an item by its value from all items (including shadow items).
    /// </summary>
    public bool TryGetShadowItemByValue(T? value, [NotNullWhen(true)] out ProSelectItem<T>? item)
    {
        return _shadowLookup.TryGetValue(value, out item);
    }

    /// <summary>
    /// Subscribes to selection changes.
    /// </summary>
    /// <param name="observer">The callback to invoke when selection changes.</param>
    /// <returns>A disposable subscription that can be used to unsubscribe.</returns>
    public IDisposable SubscribeToSelectionChanges(Func<IReadOnlyCollection<T?>, Task> observer)
    {
        _selectionObservers.Add(observer);
        return new SelectionSubscription(this, observer);
    }

    /// <summary>
    /// Unsubscribes an observer from selection changes.
    /// </summary>
    private void Unsubscribe(Func<IReadOnlyCollection<T?>, Task> observer)
    {
        _selectionObservers.Remove(observer);
    }

    /// <summary>
    /// Notifies all observers of a selection change.
    /// </summary>
    public async Task NotifySelectionChangedAsync()
    {
        var selectedValues = SelectedValues;
        for (var i = _selectionObservers.Count - 1; i >= 0; i--)
        {
            await _selectionObservers[i](selectedValues);
        }
    }

    private sealed class SelectionSubscription(ProSelectContext<T> context, Func<IReadOnlyCollection<T?>, Task> observer)
        : IDisposable
    {
        private ProSelectContext<T>? _context = context;
        private Func<IReadOnlyCollection<T?>, Task>? _observer = observer;

        public void Dispose()
        {
            var contextCopy = _context;
            var observerCopy = _observer;

            if (contextCopy is null || observerCopy is null)
            {
                return;
            }

            _context = null;
            _observer = null;

            contextCopy.Unsubscribe(observerCopy);
        }
    }
}
