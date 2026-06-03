// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProtonBlazor.Interfaces;
using ProtonBlazor.State;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents a column filter shown when <see cref="ProDataGrid{T}.FilterMode"/> is <see cref="DataGridFilterMode.ColumnFilterRow"/>.
    /// </summary>
    /// <typeparam name="T">The type of value managed by the <see cref="ProDataGrid{T}"/></typeparam>
    /// <seealso cref="ProDataGrid{T}"/>
    public partial class FilterHeaderCell<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : ProComponentBase
    {
        /// <summary>
        /// The <see cref="ProDataGrid{T}"/> containing this filter cell.
        /// </summary>
        [CascadingParameter]
        public ProDataGrid<T> DataGrid { get; set; } = null!;

        /// <summary>
        /// The column associated with this filter cell.
        /// </summary>
        [Parameter, EditorRequired]
        public required Column<T> Column { get; set; }

        /// <summary>
        /// The content within this filter cell.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private string Classname =>
            new CssBuilder(Column?.HeaderClass)
                .AddClass(Column?.HeaderClassname)
                .AddClass(Class)
                .AddClass("filter-header-cell")
                .Build();

        private string Stylename =>
            new StyleBuilder()
                .AddStyle(Column?.HeaderStyle)
                .AddStyle(Style)
                .Build();

        #region Computed Properties and Functions

        private Type? dataType
        {
            get
            {
                return Column?.PropertyType;
            }
        }

        private FieldType fieldType => FieldType.Identify(dataType);

        private IReadOnlyCollection<string> operators
        {
            get
            {
                return Column.GetFilterOperators(FieldType.Identify(dataType));
            }
        }

        private string? valueString => fieldType.IsString && Column.FilterContext.FilterDefinition?.Value is not null ? (string)Column.FilterContext.FilterDefinition.Value : default;
        private double? valueNumber => fieldType.IsNumber ? (double?)Column.FilterContext.FilterDefinition?.Value : default;
        private bool? valueBool => fieldType.IsBoolean && Column.FilterContext.FilterDefinition?.Value is not null ? (bool?)Column.FilterContext.FilterDefinition.Value : default;
        private Enum? valueEnum => fieldType.IsEnum && Column.FilterContext.FilterDefinition?.Value is not null ? (Enum)Column.FilterContext.FilterDefinition.Value : default;
        private DateTime? valueDateTimeForPicker => fieldType.IsDateTime ? (DateTime?)Column.FilterContext.FilterDefinition?.Value : default;
        private DateTime? valueDateOnlyForPicker => fieldType.IsDateOnly && Column.FilterContext.FilterDefinition?.Value != null ? ((DateOnly)Column.FilterContext.FilterDefinition.Value).ToDateTime(TimeOnly.MinValue) : null;
        private TimeSpan? valueTime => fieldType.IsDateTime && Column.FilterContext.FilterDefinition?.Value is not null ? ((DateTime?)Column.FilterContext.FilterDefinition.Value).Value.TimeOfDay : null;
        private string? @operator => Column.FilterContext.FilterDefinition?.Operator ?? operators.FirstOrDefault();

        private string chosenOperatorStyle(string o)
        {
            return o == @operator ? "color:var(--pro-palette-primary-text);background-color:var(--pro-palette-primary)" : "";
        }

        #endregion

        #region Events

        private async Task ChangeOperatorAsync(string o)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            Column.FilterContext.FilterDefinition.Operator = o;
            await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
        }

        internal async Task StringValueChangedAsync(string value)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            Column.FilterContext.FilterDefinition.Value = value;
            await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
        }

        internal async Task NumberValueChangedAsync(double? value)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            Column.FilterContext.FilterDefinition.Value = value;
            await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
        }

        internal async Task EnumValueChangedAsync(Enum value)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            Column.FilterContext.FilterDefinition.Value = value;
            await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
        }

        internal async Task BoolValueChangedAsync(bool? value)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            Column.FilterContext.FilterDefinition.Value = value;
            await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
        }

        internal async Task DateTimeValueChangedAsync(DateTime? value)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            // For DateTime fields, handle both date and time components
            if (value != null)
            {
                var date = value.Value.Date;

                // get the time component and add it to the date.
                if (valueTime != null)
                {
                    date = date.Add(valueTime.Value);
                }

                Column.FilterContext.FilterDefinition.Value = date;
            }
            else
            {
                Column.FilterContext.FilterDefinition.Value = value;
            }

            await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
        }

        internal async Task DateOnlyValueChangedAsync(DateTime? value)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            // For DateOnly fields, convert DateTime to DateOnly
            if (value != null)
            {
                var dateOnly = DateOnly.FromDateTime(value.Value);
                Column.FilterContext.FilterDefinition.Value = dateOnly;
            }
            else
            {
                Column.FilterContext.FilterDefinition.Value = null;
            }
            await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
        }

        internal async Task TimeValueChangedAsync(TimeSpan? value)
        {
            Debug.Assert(Column.FilterContext.FilterDefinition is not null);
            if (valueDateTimeForPicker != null)
            {
                var date = valueDateTimeForPicker.Value.Date;

                // get the time component and add it to the date.
                if (value != null)
                {
                    date = date.Add(value.Value);
                }

                Column.FilterContext.FilterDefinition.Value = date;
                await ApplyFilterAsync(Column.FilterContext.FilterDefinition);
            }
        }

        internal async Task ApplyFilterAsync(IFilterDefinition<T> filterDefinition)
        {
            Debug.Assert(DataGrid is not null);
            if (DataGrid.FilterDefinitions.All(x => x.Id != filterDefinition.Id))
                DataGrid.FilterDefinitions.Add(filterDefinition);
            if (DataGrid.HasServerData)
                await DataGrid.ReloadServerData();

            DataGrid.GroupItems();
            await DataGrid.NotifyFilterChangedAsync();
            ((IProStateHasChanged)DataGrid).StateHasChanged();
        }

        private async Task ClearFilterAsync()
        {
            if (Column.FilterContext.FilterDefinition is not null)
            {
                await ClearFilterAsync(Column.FilterContext.FilterDefinition);
            }
        }

        internal async Task ClearFilterAsync(IFilterDefinition<T> filterDefinition)
        {
            Debug.Assert(DataGrid is not null);
            await DataGrid.RemoveFilterAsync(filterDefinition.Id);
        }

        #endregion
    }
}
