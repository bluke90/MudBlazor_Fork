// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.Services;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{

    /// <summary>
    /// Allows the user to select a single choice from a group of options. Use radio buttons (not switches) when only one item can be selected from a list.
    /// </summary>
    /// <typeparam name="T">The type of value being selected, often a <c>bool</c>.</typeparam>
    /// <seealso cref="ProCheckBox{T}" />
    /// <seealso cref="ProRadioGroup{T}" />
    /// <seealso cref="ProSwitch{T}"/>
    public partial class ProRadio<T> : ProBooleanInput<T>
    {
        private IProRadioGroup? _parent;
        private readonly string _elementId = Identifier.Create("radio");
        private readonly string _ariaId = Identifier.Create("radio-aria-");

        protected override string Classname => new CssBuilder("pro-input-control-boolean-input")
            .AddClass("pro-disabled", GetDisabledState())
            .AddClass("pro-readonly", GetReadOnlyState())
            .AddClass("pro-input-with-content", ChildContent is not null)
            .AddClass(Class)
            .Build();

        protected override string LabelClassname => new CssBuilder("pro-radio")
            .AddClass($"pro-disabled", GetDisabledState())
            .AddClass($"pro-readonly", GetReadOnlyState())
            .AddClass($"pro-input-content-placement-{ConvertPlacement(LabelPlacement).ToStringFast(true)}")
            .Build();

        protected override string IconClassname => new CssBuilder("pro-button-root pro-icon-button")
            .AddClass("pro-ripple pro-ripple-radio", Ripple && !GetDisabledState() && !GetReadOnlyState())
            .AddClass($"pro-{Color.ToStringFast(true)}-text hover:pro-{Color.ToStringFast(true)}-hover", !GetReadOnlyState() && !GetDisabledState() && (UncheckedColor == null || (UncheckedColor != null && Checked)))
            .AddClass($"pro-{UncheckedColor?.ToStringFast(true)}-text hover:pro-{UncheckedColor?.ToStringFast(true)}-hover", !GetReadOnlyState() && !GetDisabledState() && UncheckedColor != null && Checked == false)
            .AddClass("pro-radio-dense", Dense)
            .AddClass("pro-disabled", GetDisabledState())
            .AddClass("pro-readonly", GetReadOnlyState())
            .AddClass("pro-checked", Checked)
            .AddClass("pro-error-text", ProRadioGroup?.HasErrors)
            .Build();

        [Inject]
        private IKeyInterceptorService KeyInterceptorService { get; set; } = null!;

        /// <summary>
        /// The parent Radio Group
        /// </summary>
        [CascadingParameter]
        internal IProRadioGroup? IProRadioGroup
        {
            get => _parent;
            set
            {
                _parent = value;
                _parent?.CheckGenericTypeMatch(this);
            }
        }

        /// <summary>
        /// The color to use when in an unchecked state.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Radio.Appearance)]
        public Color? UncheckedColor { get; set; } = null;

        /// <summary>
        /// The Aria Label to be assigned to the radio button.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>. Used to improve accessibility for screen readers. Adds an <c>aria-labelledby</c> to the <c>input</c> element.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Radio.Appearance)]
        public string? AriaLabel { get; set; }

        /// <summary>
        /// Uses compact vertical padding.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Radio.Appearance)]
        public bool Dense { get; set; }

        /// <summary>
        /// The icon displayed when in a checked state.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Icons.Material.Filled.RadioButtonChecked"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Appearance)]
        public string CheckedIcon { get; set; } = Icons.Material.Filled.RadioButtonChecked;

        /// <summary>
        /// The icon displayed when in an unchecked state.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Icons.Material.Filled.RadioButtonUnchecked"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Appearance)]
        public string UncheckedIcon { get; set; } = Icons.Material.Filled.RadioButtonUnchecked;

        /// <summary>
        /// The icon to display for an indeterminate state.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Icons.Material.Filled.IndeterminateCheckBox"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Appearance)]
        public string IndeterminateIcon { get; set; } = Icons.Material.Filled.IndeterminateCheckBox;

        private string GetIcon()
        {
            return Checked switch
            {
                true => CheckedIcon,
                false => UncheckedIcon
            };
        }

        internal bool Checked { get; private set; }

        internal ProRadioGroup<T>? ProRadioGroup => (ProRadioGroup<T>?)IProRadioGroup;

        internal void SetChecked(bool value)
        {
            if (Checked != value)
            {
                Checked = value;
                StateHasChanged();
            }
        }

        /// <summary>
        /// Checks this radio button.
        /// </summary>
        /// <remarks>
        /// When part of a <see cref="ProRadioGroup{T}"/>, other values will be unchecked.
        /// </remarks>
        public Task SelectAsync()
        {
            if (ProRadioGroup is not null)
            {
                return ProRadioGroup.SetSelectedRadioAsync(this);
            }

            return Task.CompletedTask;
        }

        internal Task OnClickAsync()
        {
            if (GetDisabledState() || GetReadOnlyState() || (ProRadioGroup?.GetReadOnlyState() ?? false))
            {
                return Task.CompletedTask;
            }

            if (ProRadioGroup != null)
            {
                return ProRadioGroup.SetSelectedRadioAsync(this);
            }

            return Task.CompletedTask;
        }

        protected Task HandleKeyDownAsync(KeyboardEventArgs obj) => KeyInterceptorService.DispatchAsync(_elementId, KeyEventKind.Down, obj);

        private bool CanHandleKeys() => !GetDisabledState() && !GetReadOnlyState() && !(ProRadioGroup?.GetReadOnlyState() ?? false);

        private async Task HandleBackspaceAsync()
        {
            if (ProRadioGroup is not null)
            {
                await ProRadioGroup.ResetAsync();
            }
        }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (ProRadioGroup is not null)
            {
                await ProRadioGroup.RegisterRadioAsync(this);
            }
        }

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var options = new KeyInterceptorOptions(
                    "pro-button-root",
                    [
                        // prevent scrolling page
                        new(" ", preventDown: "key+none", preventUp: "key+none"),
                        new("Enter", preventDown: "key+none"),
                        new("NumpadEnter", preventDown: "key+none"),
                        new("Backspace", preventDown: "key+none")
                    ]);

                await KeyInterceptorService.SubscribeAsync(_elementId, options, keys => keys
                    .When(CanHandleKeys, builder => builder
                        .OnKeyDownAny(["Enter", "NumpadEnter", " "], SelectAsync)
                        .OnKeyDown("Backspace", HandleBackspaceAsync)));
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        /// <inheritdoc />
        protected override async ValueTask DisposeAsyncCore()
        {
            await base.DisposeAsyncCore();
            ProRadioGroup?.UnregisterRadio(this);
            if (IsJSRuntimeAvailable)
            {
                await KeyInterceptorService.UnsubscribeAsync(_elementId);
            }
        }
    }
}
