// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents a palette of colors used throughout the application.
    /// </summary>
    [JsonDerivedType(typeof(PaletteLight), typeDiscriminator: nameof(PaletteLight))]
    [JsonDerivedType(typeof(PaletteDark), typeDiscriminator: nameof(PaletteDark))]
    public abstract class Palette
    {
        private ProColor? _primaryDarken;
        private ProColor? _primaryLighten;
        private ProColor? _secondaryDarken;
        private ProColor? _secondaryLighten;
        private ProColor? _tertiaryDarken;
        private ProColor? _tertiaryLighten;
        private ProColor? _infoDarken;
        private ProColor? _infoLighten;
        private ProColor? _successDarken;
        private ProColor? _successLighten;
        private ProColor? _warningDarken;
        private ProColor? _warningLighten;
        private ProColor? _errorDarken;
        private ProColor? _errorLighten;
        private ProColor? _darkDarken;
        private ProColor? _darkLighten;

        /// <summary>
        /// The black color.
        /// </summary>
        public virtual ProColor Black { get; set; } = "#272c34";

        /// <summary>
        /// The white color.
        /// </summary>
        public virtual ProColor White { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The primary color.
        /// </summary>
        public virtual ProColor Primary { get; set; } = "#594AE2";

        /// <summary>
        /// The contrast text color for the primary color.
        /// </summary>
        public virtual ProColor PrimaryContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The secondary color.
        /// </summary>
        public virtual ProColor Secondary { get; set; } = Colors.Pink.Accent2;

        /// <summary>
        /// The contrast text color for the secondary color.
        /// </summary>
        public virtual ProColor SecondaryContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The tertiary color.
        /// </summary>
        public virtual ProColor Tertiary { get; set; } = "#1EC8A5";

        /// <summary>
        /// The contrast text color for the tertiary color.
        /// </summary>
        public virtual ProColor TertiaryContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The info color.
        /// </summary>
        public virtual ProColor Info { get; set; } = Colors.Blue.Default;

        /// <summary>
        /// The contrast text color for the info color.
        /// </summary>
        public virtual ProColor InfoContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The success color.
        /// </summary>
        public virtual ProColor Success { get; set; } = Colors.Green.Accent4;

        /// <summary>
        /// The contrast text color for the success color.
        /// </summary>
        public virtual ProColor SuccessContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The warning color.
        /// </summary>
        public virtual ProColor Warning { get; set; } = Colors.Orange.Default;

        /// <summary>
        /// The contrast text color for the warning color.
        /// </summary>
        public virtual ProColor WarningContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The error color.
        /// </summary>
        public virtual ProColor Error { get; set; } = Colors.Red.Default;

        /// <summary>
        /// The contrast text color for the error color.
        /// </summary>
        public virtual ProColor ErrorContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The dark color.
        /// </summary>
        public virtual ProColor Dark { get; set; } = Colors.Gray.Darken3;

        /// <summary>
        /// The contrast text color for the dark color.
        /// </summary>
        public virtual ProColor DarkContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The primary text color.
        /// </summary>
        public virtual ProColor TextPrimary { get; set; } = Colors.Gray.Darken3;

        /// <summary>
        /// The secondary text color.
        /// </summary>
        public virtual ProColor TextSecondary { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.54).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The disabled text color.
        /// </summary>
        public virtual ProColor TextDisabled { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.38).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The default action color.
        /// </summary>
        public virtual ProColor ActionDefault { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.54).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The disabled action color.
        /// </summary>
        public virtual ProColor ActionDisabled { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.26).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The background color for disabled actions.
        /// </summary>
        public virtual ProColor ActionDisabledBackground { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.12).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The background color.
        /// </summary>
        public virtual ProColor Background { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The gray background color.
        /// </summary>
        public virtual ProColor BackgroundGray { get; set; } = Colors.Gray.Lighten4;

        /// <summary>
        /// The surface color.
        /// </summary>
        public virtual ProColor Surface { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The drawer background color.
        /// </summary>
        public virtual ProColor DrawerBackground { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The drawer text color.
        /// </summary>
        public virtual ProColor DrawerText { get; set; } = Colors.Gray.Darken3;

        /// <summary>
        /// The drawer icon color.
        /// </summary>
        public virtual ProColor DrawerIcon { get; set; } = Colors.Gray.Darken2;

        /// <summary>
        /// The appbar background color.
        /// </summary>
        public virtual ProColor AppbarBackground { get; set; } = "#594AE2";

        /// <summary>
        /// The appbar text color.
        /// </summary>
        public virtual ProColor AppbarText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The default color for lines.
        /// </summary>
        public virtual ProColor LinesDefault { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.12).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The color for input lines.
        /// </summary>
        public virtual ProColor LinesInputs { get; set; } = Colors.Gray.Lighten1;

        /// <summary>
        /// The color for table lines.
        /// </summary>
        public virtual ProColor TableLines { get; set; } = new ProColor(Colors.Gray.Lighten2).SetAlpha(1.0).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The color for striped rows in a table.
        /// </summary>
        public virtual ProColor TableStriped { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.02).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The color for table rows on hover.
        /// </summary>
        public virtual ProColor TableHover { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.04).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The color for dividers.
        /// </summary>
        public virtual ProColor Divider { get; set; } = Colors.Gray.Lighten2;

        /// <summary>
        /// The light color for dividers.
        /// </summary>
        public virtual ProColor DividerLight { get; set; } = new ProColor(Colors.Shades.Black).SetAlpha(0.8).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The color for skeletons.
        /// </summary>
        public virtual ProColor Skeleton { get; set; } = new ProColor("rgba(0, 0, 0, 0.11)").ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The darkened value of the primary color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string PrimaryDarken
        {
            get => (_primaryDarken ??= Primary.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _primaryDarken = value;
        }

        /// <summary>
        /// The lightened value of the primary color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string PrimaryLighten
        {
            get => (_primaryLighten ??= Primary.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _primaryLighten = value;
        }

        /// <summary>
        /// The darkened value of the secondary color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string SecondaryDarken
        {
            get => (_secondaryDarken ??= Secondary.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _secondaryDarken = value;
        }

        /// <summary>
        /// The lightened value of the secondary color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string SecondaryLighten
        {
            get => (_secondaryLighten ??= Secondary.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _secondaryLighten = value;
        }

        /// <summary>
        /// The darkened value of the tertiary color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string TertiaryDarken
        {
            get => (_tertiaryDarken ??= Tertiary.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _tertiaryDarken = value;
        }

        /// <summary>
        /// The lightened value of the tertiary color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string TertiaryLighten
        {
            get => (_tertiaryLighten ??= Tertiary.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _tertiaryLighten = value;
        }

        /// <summary>
        /// The darkened value of the info color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string InfoDarken
        {
            get => (_infoDarken ??= Info.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _infoDarken = value;
        }

        /// <summary>
        /// The lightened value of the info color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string InfoLighten
        {
            get => (_infoLighten ??= Info.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _infoLighten = value;
        }

        /// <summary>
        /// The darkened value of the success color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string SuccessDarken
        {
            get => (_successDarken ??= Success.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _successDarken = value;
        }

        /// <summary>
        /// The lightened value of the success color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string SuccessLighten
        {
            get => (_successLighten ??= Success.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _successLighten = value;
        }

        /// <summary>
        /// The darkened value of the warning color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string WarningDarken
        {
            get => (_warningDarken ??= Warning.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _warningDarken = value;
        }

        /// <summary>
        /// The lightened value of the warning color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string WarningLighten
        {
            get => (_warningLighten ??= Warning.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _warningLighten = value;
        }

        /// <summary>
        /// The darkened value of the error color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string ErrorDarken
        {
            get => (_errorDarken ??= Error.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _errorDarken = value;
        }

        /// <summary>
        /// The lightened value of the error color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string ErrorLighten
        {
            get => (_errorLighten ??= Error.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _errorLighten = value;
        }

        /// <summary>
        /// The darkened value of the dark color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string DarkDarken
        {
            get => (_darkDarken ??= Dark.ColorRgbDarken()).ToString(ProColorOutputFormats.RGB);
            set => _darkDarken = value;
        }

        /// <summary>
        /// The lightened value of the dark color.<br/>
        /// This is calculated using <see cref="ProColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string DarkLighten
        {
            get => (_darkLighten ??= Dark.ColorRgbLighten()).ToString(ProColorOutputFormats.RGB);
            set => _darkLighten = value;
        }

        /// <summary>
        /// The opacity value for most borders.
        /// </summary>
        public virtual double BorderOpacity { get; set; } = 1.0;

        /// <summary>
        /// The opacity value for hover effect.
        /// </summary>
        public virtual double HoverOpacity { get; set; } = 0.06;

        /// <summary>
        /// The opacity for the ripple effect.
        /// </summary>
        public virtual double RippleOpacity { get; set; } = 0.1;

        /// <summary>
        /// The opacity for the ripple effect on specific elements like filled buttons.
        /// </summary>
        public virtual double RippleOpacitySecondary { get; set; } = 0.2;

        /// <summary>
        /// The default gray color.
        /// </summary>
        public virtual string GrayDefault { get; set; } = Colors.Gray.Default;

        /// <summary>
        /// The lightened gray color.
        /// </summary>
        public virtual string GrayLight { get; set; } = Colors.Gray.Lighten1;

        /// <summary>
        /// The further lightened gray color.
        /// </summary>
        public virtual string GrayLighter { get; set; } = Colors.Gray.Lighten2;

        /// <summary>
        /// The darkened gray color.
        /// </summary>
        public virtual string GrayDark { get; set; } = Colors.Gray.Darken1;

        /// <summary>
        /// The further darkened gray color.
        /// </summary>
        public virtual string GrayDarker { get; set; } = Colors.Gray.Darken2;

        /// <summary>
        /// The dark overlay color.
        /// </summary>
        public virtual string OverlayDark { get; set; } = new ProColor("#212121").SetAlpha(0.5).ToString(ProColorOutputFormats.RGBA);

        /// <summary>
        /// The light overlay color.
        /// </summary>
        public virtual string OverlayLight { get; set; } = new ProColor(Colors.Shades.White).SetAlpha(0.5).ToString(ProColorOutputFormats.RGBA);
    }
}
