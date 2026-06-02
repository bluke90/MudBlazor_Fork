// Copyright (c) 2019 Blazored
// Copyright (c) 2020 Adapted by Jonny Larsson, Meinrad Recheis and Contributors

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.Services;
using ProtonBlazor.State;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// An instance of a <see cref="ProDialog"/>.
    /// </summary>
    /// <remarks>
    /// When a <see cref="ProDialog"/> is shown, a new instance is created.  This instance can then be used to perform actions such as hiding the dialog programmatically.
    /// </remarks>
    /// <seealso cref="ProDialog"/>
    /// <seealso cref="ProDialogProvider"/>
    /// <seealso cref="DialogOptions"/>
    /// <seealso cref="DialogParameters{T}"/>
    /// <seealso cref="DialogReference"/>
    /// <seealso cref="DialogService"/>
    public partial class ProDialogContainer : ProComponentBase, IMudDialogInstanceInternal, IAsyncDisposable
    {
        private bool _disposed;
        private ProDialog? _dialog;
        private ElementReference _dialogContainerReference;
        private readonly ParameterState<DialogOptions> _dialogOptionsState;
        private readonly ParameterState<string?> _titleState;

        internal string ElementId { get; } = Identifier.Create("dialog");

        public ProDialogContainer()
        {
            var registerScope = CreateRegisterScope();
            _dialogOptionsState = registerScope.RegisterParameter<DialogOptions>(nameof(Options))
                .WithParameter(() => Options);
            _titleState = registerScope.RegisterParameter<string?>(nameof(Title))
                .WithParameter(() => Title);
        }

        [Inject]
        private IKeyInterceptorService KeyInterceptorService { get; set; } = null!;

        /// <summary>
        /// Displays this dialog right-to-left.
        /// </summary>
        [CascadingParameter(Name = "RightToLeft")]
        public bool RightToLeft { get; set; }

        [CascadingParameter]
        private ProDialogProvider Parent { get; set; } = null!;

        [CascadingParameter]
        private DialogOptions GlobalDialogOptions { get; set; } = DialogOptions.Default;

        /// <summary>
        /// The options used for this dialog.
        /// </summary>
        /// <remarks>
        /// Defaults to the options in the <see cref="ProDialog"/> or options passed during <see cref="DialogService.ShowAsync(Type)"/> methods.
        /// </remarks>
        [Parameter, ParameterState]
        [Category(CategoryTypes.Dialog.Misc)] // Behavior and Appearance
        public DialogOptions Options { get; set; } = DialogOptions.Default;

        /// <summary>
        /// The text displayed at the top of this dialog if <see cref="TitleContent" /> is not set.
        /// </summary>
        [Parameter, ParameterState]
        [Category(CategoryTypes.Dialog.Behavior)]
        public string? Title { get; set; }

        /// <summary>
        /// The custom content at the top of this dialog.
        /// </summary>
        /// <remarks>
        /// This content will display so long as <see cref="Title"/> is not set.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Dialog.Behavior)]
        public RenderFragment? TitleContent { get; set; }

        /// <summary>
        /// The content within this dialog.
        /// </summary>
        /// <remarks>
        /// Defaults to the content of the <see cref="ProDialog"/> being displayed.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Dialog.Behavior)]
        public RenderFragment? Content { get; set; }

        /// <inheritdoc />
        [Parameter]
        [Category(CategoryTypes.Dialog.Behavior)]
        public Guid Id { get; set; }

        /// <summary>
        /// The custom icon displayed in the upper-right corner for closing this dialog.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Icons.Material.Filled.Close"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Dialog.Appearance)]
        public string CloseIcon { get; set; } = Icons.Material.Filled.Close;

        protected string TitleClassname =>
            new CssBuilder("pro-dialog-title")
                .AddClass(_dialog?.TitleClass)
                .Build();

        protected string Classname =>
            new CssBuilder("pro-dialog")
                .AddClass(GetMaxWidth(), !GetFullScreen())
                .AddClass("pro-dialog-width-full", GetFullWidth() && !GetFullScreen())
                .AddClass("pro-dialog-fullscreen", GetFullScreen())
                .AddClass("pro-dialog-rtl", RightToLeft)
                .AddClass(_dialog?.Class)
                .Build();

        protected string BackgroundClassname =>
            new CssBuilder("pro-overlay-dialog")
                .AddClass($"pro-skip-overlay-section") // dialog overlay remains outside of Section
                .AddClass("pro-skip-overlay-positioning") // popovers try to position the overlay by zindex, this skips that behavior if a user puts the dialog provider above the popover provider
                .AddClass(GetBackgroundClass())
                .Build();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var options = new KeyInterceptorOptions(
                    "pro-dialog",
                    [
                        new("/./", subscribeDown: true, subscribeUp: true)
                    ]);

                await KeyInterceptorService.SubscribeAsync(ElementId, options, keys => keys
                    .OnKeyDown("Escape", HandleEscapeAsync)
                    .OnKeyDown("/./", HandleAnyKeyDownAsync)
                    .OnKeyUp("/./", HandleAnyKeyUpAsync));
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private Task HandleEscapeAsync()
        {
            if (GetCloseOnEscapeKey())
            {
                ((IMudDialogInstance)this).Cancel();
            }
            return Task.CompletedTask;
        }

        private async Task HandleAnyKeyDownAsync(KeyboardEventArgs args)
        {
            // Don't invoke callback for Escape - it's handled separately
            if (args.Key == "Escape")
                return;

            if (_dialog is not null && _dialog.OnKeyDown.HasDelegate)
            {
                await _dialog.OnKeyDown.InvokeAsync(args);
                // Note: we need to force a render here because the user will expect this blazor standard functionality.
                // Since the event originates from KeyInterceptor it will not cause a render automatically.
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task HandleAnyKeyUpAsync(KeyboardEventArgs args)
        {
            if (_dialog is not null && _dialog.OnKeyUp.HasDelegate)
            {
                await _dialog.OnKeyUp.InvokeAsync(args);
                // note: we need to force a render here because the user will expect this blazor standard functionality
                // Since the event originates from KeyInterceptor it will not cause a render automatically.
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task OnMouseUpAsync(MouseEventArgs args)
        {
            if (args.Button > 0)
                await RefocusDialogAsync();
        }

        private bool GetHideHeader()
        {
            if (GetDialogOptionsOrDefault.NoHeader.HasValue)
                return GetDialogOptionsOrDefault.NoHeader.Value;

            if (GlobalDialogOptions.NoHeader.HasValue)
                return GlobalDialogOptions.NoHeader.Value;

            return false;
        }

        private bool GetCloseButton()
        {
            if (GetDialogOptionsOrDefault.CloseButton.HasValue)
                return GetDialogOptionsOrDefault.CloseButton.Value;

            if (GlobalDialogOptions.CloseButton.HasValue)
                return GlobalDialogOptions.CloseButton.Value;

            return false;
        }

        private bool GetBackdropClick()
        {
            if (GetDialogOptionsOrDefault.BackdropClick.HasValue)
                return GetDialogOptionsOrDefault.BackdropClick.Value;

            if (GlobalDialogOptions.BackdropClick.HasValue)
                return GlobalDialogOptions.BackdropClick.Value;

            return true;
        }

        private bool GetCloseOnEscapeKey()
        {
            if (GetDialogOptionsOrDefault.CloseOnEscapeKey.HasValue)
                return GetDialogOptionsOrDefault.CloseOnEscapeKey.Value;

            if (GlobalDialogOptions.CloseOnEscapeKey.HasValue)
                return GlobalDialogOptions.CloseOnEscapeKey.Value;

            return false;
        }

        private async Task HandleBackgroundClickAsync(MouseEventArgs args)
        {
            if (!GetBackdropClick())
            {
                await RefocusDialogAsync();
                return;
            }

            if (_dialog is not null && _dialog.OnBackdropClick.HasDelegate)
            {
                await _dialog.OnBackdropClick.InvokeAsync(args);
                await RefocusDialogAsync();
            }
            else
            {
                ((IMudDialogInstance)this).Cancel();
            }
        }

        private async Task RefocusDialogAsync()
        {
            if (GetCloseOnEscapeKey() && !_disposed)
            {
                await _dialogContainerReference.FocusAsync();
            }
        }

        private string GetPosition()
        {
            DialogPosition position;

            if (GetDialogOptionsOrDefault.Position.HasValue)
            {
                position = GetDialogOptionsOrDefault.Position.Value;
            }
            else if (GlobalDialogOptions.Position.HasValue)
            {
                position = GlobalDialogOptions.Position.Value;
            }
            else
            {
                position = DialogPosition.Center;
            }
            return $"pro-dialog-{position.ToStringFast(true)}";
        }

        private string GetMaxWidth()
        {
            MaxWidth maxWidth;

            if (GetDialogOptionsOrDefault.MaxWidth.HasValue)
            {
                maxWidth = GetDialogOptionsOrDefault.MaxWidth.Value;
            }
            else if (GlobalDialogOptions.MaxWidth.HasValue)
            {
                maxWidth = GlobalDialogOptions.MaxWidth.Value;
            }
            else
            {
                maxWidth = MaxWidth.Small;
            }
            return $"pro-dialog-width-{maxWidth.ToStringFast(true)}";
        }

        private bool GetFullWidth() => GetDialogOptionsOrDefault.FullWidth ?? GlobalDialogOptions.FullWidth ?? false;

        private bool GetFullScreen() => GetDialogOptionsOrDefault.FullScreen ?? GlobalDialogOptions.FullScreen ?? false;

        private string? GetBackgroundClass() => GetDialogOptionsOrDefault.BackgroundClass ?? GlobalDialogOptions.BackgroundClass;

        private DialogOptions GetDialogOptionsOrDefault => _dialogOptionsState.Value ?? DialogOptions.Default;

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            if (IsJSRuntimeAvailable)
            {
                await KeyInterceptorService.UnsubscribeAsync(ElementId);
            }
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        string IMudDialogInstance.ElementId => ElementId;

        /// <inheritdoc />
        string? IMudDialogInstance.Title => _titleState.Value;

        /// <inheritdoc />
        DialogOptions IMudDialogInstance.Options => GetDialogOptionsOrDefault;

        /// <inheritdoc />
        async Task IMudDialogInstance.SetOptionsAsync(DialogOptions options)
        {
            await _dialogOptionsState.SetValueAsync(options);
            Parent.SetOptions(Id, options);
            await InvokeAsync(StateHasChanged);
        }

        /// <inheritdoc />
        async Task IMudDialogInstance.SetTitleAsync(string? title)
        {
            await _titleState.SetValueAsync(title);
            await InvokeAsync(StateHasChanged);
        }

        /// <inheritdoc />
        void IMudDialogInstance.Close()
        {
            ((IMudDialogInstance)this).Close(DialogResult.Ok<object?>(null));
        }

        /// <inheritdoc />
        void IMudDialogInstance.Close(DialogResult dialogResult)
        {
            Parent.DismissInstance(Id, dialogResult);
        }

        /// <inheritdoc />
        void IMudDialogInstance.Close<T>(T returnValue)
        {
            var dialogResult = DialogResult.Ok<T>(returnValue);
            Parent.DismissInstance(Id, dialogResult);
        }

        /// <inheritdoc />
        void IMudDialogInstance.Cancel() => ((IMudDialogInstance)this).Close(DialogResult.Cancel());

        /// <inheritdoc />
        void IMudDialogInstanceInternal.Register(ProDialog dialog)
        {
            _dialog = dialog;
            Class = dialog.Class;
            Style = dialog.Style;
            TitleContent = dialog.TitleContent;
            StateHasChanged();
        }

        /// <inheritdoc />
        void IMudDialogInstance.StateHasChanged() => StateHasChanged();

        /// <inheritdoc />
        void IMudDialogInstance.CancelAll()
        {
            Parent?.DismissAll();
        }
    }
}
