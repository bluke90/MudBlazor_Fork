// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using BytexDigital.Blazor.Components.CookieConsent.Dialogs.Prompt.Default;

namespace ProtonBlazor.Docs.Pages.Consent.Prompt
{
#nullable enable
    public class ProCookieConsentPromptVariant : CookieConsentDefaultPromptVariant
    {
        /// <inheritdoc />
        public override Type ComponentType { get; set; } = typeof(ProCookieConsentPrompt);
    }
}
