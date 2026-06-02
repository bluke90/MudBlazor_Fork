using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{

    /// <summary>
    /// A cell within a <see cref="ProTr" />, <see cref="ProTHeadRow"/>, or <see cref="ProTFootRow"/> row component.
    /// </summary>
    public partial class ProTd : ProComponentBase
    {
        protected string Classname =>
            new CssBuilder("pro-table-cell")
                .AddClass(Context?.Table?.CellClass)
                .AddClass("pro-table-cell-hide", HideSmall)
                .AddClass(Class)
                .Build();

        /// <summary>
        /// The current state of the <see cref="ProTable{T}"/> containing this group.
        /// </summary>
        [CascadingParameter]
        public TableContext? Context { get; set; }

        /// <summary>
        /// The content within this cell.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// The label for this cell when the table is in small-device mode.
        /// </summary>
        [Parameter]
        public string? DataLabel { get; set; }

        /// <summary>
        /// Hides this cell if the breakpoint is smaller than <see cref="ProTableBase.Breakpoint"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        public bool HideSmall { get; set; }
    }
}
