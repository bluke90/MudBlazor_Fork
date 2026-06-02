// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents a cell displayed at the bottom of a column.
    /// </summary>
    /// <typeparam name="T">The kind of data managed by this footer.</typeparam>
    public partial class FooterCell<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : ProComponentBase
    {
        /// <summary>
        /// The <see cref="ProDataGrid{T}"/> which contains this footer cell.
        /// </summary>
        [CascadingParameter]
        public ProDataGrid<T>? DataGrid { get; set; }

        /// <summary>
        /// The column related to this footer cell.
        /// </summary>
        [Parameter]
        public Column<T>? Column { get; set; }

        /// <summary>
        /// The content within this footer cell.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// The current values related to this footer cell.
        /// </summary>
        [Parameter]
        public IEnumerable<T>? CurrentItems { get; set; }

        private string Classname =>
            new CssBuilder(Column?.FooterClassname)
                .AddClass(Column?.FooterClassFunc?.Invoke(items ?? Enumerable.Empty<T>()))
                .AddClass(Column?.FooterClass)
                .AddClass(Class)
                .Build();

        private string Stylename =>
            new StyleBuilder()
                .AddStyle(Column?.FooterStyleFunc?.Invoke(items ?? Enumerable.Empty<T>()))
                .AddStyle(Column?.FooterStyle)
                .AddStyle(Style)
                .AddStyle("font-weight", "600")
                .Build();

        internal IEnumerable<T> items
        {
            get
            {
                Debug.Assert(DataGrid is not null);
                return CurrentItems ?? DataGrid.CurrentPageItems;
            }
        }
    }
}
