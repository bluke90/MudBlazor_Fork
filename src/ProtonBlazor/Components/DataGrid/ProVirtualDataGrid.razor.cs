// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor;

public partial class ProVirtualDataGrid<TItem> : ProComponentBase, IDisposable
{
    [Parameter, EditorRequired] public IServerDataAdapter<TItem> Adapter { get; set; } = null!;
    [Parameter, EditorRequired] public IReadOnlyList<ProDataGridColumn<TItem>> Columns { get; set; } = [];

    [Parameter] public int PageSize { get; set; } = 25;
    [Parameter] public int[] PageSizeOptions { get; set; } = [10, 25, 50, 100];
    [Parameter] public bool ShowPagination { get; set; } = true;
    [Parameter] public bool ShowFilter { get; set; } = true;
    [Parameter] public string? FilterPlaceholder { get; set; }
    [Parameter] public EventCallback<TItem> OnRowClick { get; set; }
    [Parameter] public Func<TItem, object>? RowKey { get; set; }
    [Parameter] public Func<TItem, string?>? RowClass { get; set; }
    [Parameter] public RenderFragment? EmptyContent { get; set; }

    private IReadOnlyList<TItem> _items = [];
    private int _totalCount;
    private int _page;
    private int _pageSize;
    private bool _loading;
    private string? _sortField;
    private bool _sortDescending;
    private string? _filterText;
    private string? _error;
    private CancellationTokenSource? _cts;
    private System.Threading.Timer? _filterTimer;

    private int TotalPages => _pageSize > 0 ? (int)Math.Ceiling(_totalCount / (double)_pageSize) : 1;

    private string _containerClass => new CssBuilder("pro-sdg")
        .AddClass(Class)
        .Build();

    protected override void OnInitialized() => _pageSize = PageSize;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        _loading = true;
        _error = null;
        StateHasChanged();

        try
        {
            var request = new ServerDataRequest
            {
                Page = _page,
                PageSize = _pageSize,
                SortField = _sortField,
                SortDescending = _sortDescending,
                Filter = string.IsNullOrWhiteSpace(_filterText) ? null : _filterText.Trim(),
            };

            var result = await Adapter.GetDataAsync(request, token);

            if (!token.IsCancellationRequested)
            {
                _items = result.Items;
                _totalCount = result.TotalCount;
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            if (!token.IsCancellationRequested)
                _error = ex.Message;
        }
        finally
        {
            if (!token.IsCancellationRequested)
            {
                _loading = false;
                StateHasChanged();
            }
        }
    }

    private async Task HandleSort(ProDataGridColumn<TItem> col)
    {
        if (col.SortField is null) return;
        if (_sortField == col.SortField)
            _sortDescending = !_sortDescending;
        else
        {
            _sortField = col.SortField;
            _sortDescending = false;
        }
        _page = 0;
        await LoadDataAsync();
    }

    private void HandleFilterInput(ChangeEventArgs e)
    {
        _filterText = e.Value?.ToString();
        _page = 0;

        _filterTimer?.Dispose();
        _filterTimer = new System.Threading.Timer(
            async _ => await InvokeAsync(LoadDataAsync),
            state: null,
            dueTime: 350,
            period: System.Threading.Timeout.Infinite);
    }

    private async Task HandlePageSizeChange(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var size))
        {
            _pageSize = size;
            _page = 0;
            await LoadDataAsync();
        }
    }

    private async Task PrevPage()
    {
        if (_page > 0) { _page--; await LoadDataAsync(); }
    }

    private async Task NextPage()
    {
        if (_page < TotalPages - 1) { _page++; await LoadDataAsync(); }
    }

    private async Task GoToPage(int page)
    {
        _page = page;
        await LoadDataAsync();
    }

    private async Task HandleRowClick(TItem item)
    {
        if (OnRowClick.HasDelegate)
            await OnRowClick.InvokeAsync(item);
    }

    private string GetRowClass(TItem item) =>
        new CssBuilder("pro-sdg-row")
            .AddClass("pro-sdg-row--clickable", OnRowClick.HasDelegate)
            .AddClass(RowClass?.Invoke(item))
            .Build();

    private string GetHeaderClass(ProDataGridColumn<TItem> col) =>
        new CssBuilder("pro-sdg-th")
            .AddClass("pro-sdg-th--sortable", col.SortField is not null)
            .AddClass("pro-sdg-th--sorted", col.SortField is not null && col.SortField == _sortField)
            .AddClass($"pro-sdg-align-{col.Align.ToString().ToLowerInvariant()}")
            .Build();

    private string GetCellClass(ProDataGridColumn<TItem> col) =>
        new CssBuilder("pro-sdg-td")
            .AddClass(col.CssClass)
            .AddClass($"pro-sdg-align-{col.Align.ToString().ToLowerInvariant()}")
            .Build();

    private string? GetAriaSortValue(ProDataGridColumn<TItem> col)
    {
        if (col.SortField is null || col.SortField != _sortField) return null;
        return _sortDescending ? "descending" : "ascending";
    }

    private string GetSortIndicator(ProDataGridColumn<TItem> col)
    {
        if (col.SortField != _sortField) return "⇅";
        return _sortDescending ? "↓" : "↑";
    }

    private IEnumerable<int> GetPageNumbers()
    {
        var total = TotalPages;
        if (total <= 7) return Enumerable.Range(0, total);

        var pages = new List<int> { 0 };

        if (_page > 2) pages.Add(-1);

        for (var i = Math.Max(1, _page - 1); i <= Math.Min(total - 2, _page + 1); i++)
            pages.Add(i);

        if (_page < total - 3) pages.Add(-1);
        pages.Add(total - 1);

        return pages;
    }

    /// <summary>Forces a fresh data load. Useful after an external mutation.</summary>
    public Task RefreshAsync()
    {
        _page = 0;
        return LoadDataAsync();
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _filterTimer?.Dispose();
    }
}
