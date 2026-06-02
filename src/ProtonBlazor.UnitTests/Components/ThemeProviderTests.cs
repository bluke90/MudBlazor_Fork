using System.Globalization;
using AngleSharp.Html.Dom;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using NUnit.Framework;
using ProtonBlazor.Extensions;
using ProtonBlazor.UnitTests.TestComponents.ThemeProvider;
using ProtonBlazor.Utilities;

namespace ProtonBlazor.UnitTests.Components
{
#nullable enable
    [TestFixture]
    public class ThemeProviderTests : BunitTest
    {
        [Test]
        [TestCase("en-us")]
        [TestCase("de-DE")]
        [TestCase("he-IL")]
        [TestCase("ar-ER")]
        public void DifferentCultures(string cultureString)
        {
            var culture = new CultureInfo(cultureString, false);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            var comp = Context.Render<ProThemeProvider>();

            var styleNodes = comp.Nodes.OfType<IHtmlStyleElement>().ToArray();
            styleNodes.Should().HaveCount(3);

            var rootStyleNode = styleNodes[2];

            var expectedLines = new[] {
                ":root{",
                "--pro-palette-black: rgba(39,44,52,1);",
                "--pro-palette-white: rgba(255,255,255,1);",
                "--pro-palette-primary: rgba(89,74,226,1);",
                "--pro-palette-primary-rgb: 89,74,226;",
                "--pro-palette-primary-text: rgba(255,255,255,1);",
                "--pro-palette-primary-darken: rgb(62,44,221);",
                "--pro-palette-primary-lighten: rgb(118,106,231);",
                "--pro-palette-primary-hover: rgba(89,74,226,0.058823529411764705);",
                "--pro-palette-secondary: rgba(255,64,129,1);",
                "--pro-palette-secondary-rgb: 255,64,129;",
                "--pro-palette-secondary-text: rgba(255,255,255,1);",
                "--pro-palette-secondary-darken: rgb(255,31,105);",
                "--pro-palette-secondary-lighten: rgb(255,102,153);",
                "--pro-palette-secondary-hover: rgba(255,64,129,0.058823529411764705);",
                "--pro-palette-tertiary: rgba(30,200,165,1);",
                "--pro-palette-tertiary-rgb: 30,200,165;",
                "--pro-palette-tertiary-text: rgba(255,255,255,1);",
                "--pro-palette-tertiary-darken: rgb(25,169,140);",
                "--pro-palette-tertiary-lighten: rgb(42,223,187);",
                "--pro-palette-tertiary-hover: rgba(30,200,165,0.058823529411764705);",
                "--pro-palette-info: rgba(33,150,243,1);",
                "--pro-palette-info-rgb: 33,150,243;",
                "--pro-palette-info-text: rgba(255,255,255,1);",
                "--pro-palette-info-darken: rgb(12,128,223);",
                "--pro-palette-info-lighten: rgb(71,167,245);",
                "--pro-palette-info-hover: rgba(33,150,243,0.058823529411764705);",
                "--pro-palette-success: rgba(0,200,83,1);",
                "--pro-palette-success-rgb: 0,200,83;",
                "--pro-palette-success-text: rgba(255,255,255,1);",
                "--pro-palette-success-darken: rgb(0,163,68);",
                "--pro-palette-success-lighten: rgb(0,235,98);",
                "--pro-palette-success-hover: rgba(0,200,83,0.058823529411764705);",
                "--pro-palette-warning: rgba(255,152,0,1);",
                "--pro-palette-warning-rgb: 255,152,0;",
                "--pro-palette-warning-text: rgba(255,255,255,1);",
                "--pro-palette-warning-darken: rgb(214,129,0);",
                "--pro-palette-warning-lighten: rgb(255,167,36);",
                "--pro-palette-warning-hover: rgba(255,152,0,0.058823529411764705);",
                "--pro-palette-error: rgba(244,67,54,1);",
                "--pro-palette-error-rgb: 244,67,54;",
                "--pro-palette-error-text: rgba(255,255,255,1);",
                "--pro-palette-error-darken: rgb(242,28,13);",
                "--pro-palette-error-lighten: rgb(246,96,85);",
                "--pro-palette-error-hover: rgba(244,67,54,0.058823529411764705);",
                "--pro-palette-dark: rgba(66,66,66,1);",
                "--pro-palette-dark-rgb: 66,66,66;",
                "--pro-palette-dark-text: rgba(255,255,255,1);",
                "--pro-palette-dark-darken: rgb(46,46,46);",
                "--pro-palette-dark-lighten: rgb(87,87,87);",
                "--pro-palette-dark-hover: rgba(66,66,66,0.058823529411764705);",
                "--pro-palette-text-primary: rgba(66,66,66,1);",
                "--pro-palette-text-primary-rgb: 66,66,66;",
                "--pro-palette-text-secondary: rgba(0,0,0,0.5372549019607843);",
                "--pro-palette-text-secondary-rgb: 0,0,0;",
                "--pro-palette-text-disabled: rgba(0,0,0,0.3764705882352941);",
                "--pro-palette-text-disabled-rgb: 0,0,0;",
                "--pro-palette-action-default: rgba(0,0,0,0.5372549019607843);",
                "--pro-palette-action-default-hover: rgba(0,0,0,0.058823529411764705);",
                "--pro-palette-action-disabled: rgba(0,0,0,0.25882352941176473);",
                "--pro-palette-action-disabled-background: rgba(0,0,0,0.11764705882352941);",
                "--pro-palette-surface: rgba(255,255,255,1);",
                "--pro-palette-surface-rgb: 255,255,255;",
                "--pro-palette-background: rgba(255,255,255,1);",
                "--pro-palette-background-gray: rgba(245,245,245,1);",
                "--pro-palette-drawer-background: rgba(255,255,255,1);",
                "--pro-palette-drawer-text: rgba(66,66,66,1);",
                "--pro-palette-drawer-icon: rgba(97,97,97,1);",
                "--pro-palette-appbar-background: rgba(89,74,226,1);",
                "--pro-palette-appbar-text: rgba(255,255,255,1);",
                "--pro-palette-lines-default: rgba(0,0,0,0.11764705882352941);",
                "--pro-palette-lines-inputs: rgba(189,189,189,1);",
                "--pro-palette-table-lines: rgba(224,224,224,1);",
                "--pro-palette-table-striped: rgba(0,0,0,0.0196078431372549);",
                "--pro-palette-table-hover: rgba(0,0,0,0.0392156862745098);",
                "--pro-palette-divider: rgba(224,224,224,1);",
                "--pro-palette-divider-rgb: 224,224,224;",
                "--pro-palette-divider-light: rgba(0,0,0,0.8);",
                "--pro-palette-skeleton: rgba(0,0,0,0.10980392156862745);",
                "--pro-palette-gray-default: #9E9E9E;",
                "--pro-palette-gray-light: #BDBDBD;",
                "--pro-palette-gray-lighter: #E0E0E0;",
                "--pro-palette-gray-dark: #757575;",
                "--pro-palette-gray-darker: #616161;",
                "--pro-palette-overlay-dark: rgba(33,33,33,0.4980392156862745);",
                "--pro-palette-overlay-light: rgba(255,255,255,0.4980392156862745);",
                "--pro-palette-border-opacity: 1;",
                "--pro-ripple-color: var(--pro-palette-text-primary);",
                "--pro-ripple-opacity: 0.1;",
                "--pro-ripple-opacity-secondary: 0.2;",
                "--pro-elevation-0: none;",
                "--pro-elevation-1: 0px 2px 1px -1px rgba(0,0,0,0.2),0px 1px 1px 0px rgba(0,0,0,0.14),0px 1px 3px 0px rgba(0,0,0,0.12);",
                "--pro-elevation-2: 0px 3px 1px -2px rgba(0,0,0,0.2),0px 2px 2px 0px rgba(0,0,0,0.14),0px 1px 5px 0px rgba(0,0,0,0.12);",
                "--pro-elevation-3: 0px 3px 3px -2px rgba(0,0,0,0.2),0px 3px 4px 0px rgba(0,0,0,0.14),0px 1px 8px 0px rgba(0,0,0,0.12);",
                "--pro-elevation-4: 0px 2px 4px -1px rgba(0,0,0,0.2),0px 4px 5px 0px rgba(0,0,0,0.14),0px 1px 10px 0px rgba(0,0,0,0.12);",
                "--pro-elevation-5: 0px 3px 5px -1px rgba(0,0,0,0.2),0px 5px 8px 0px rgba(0,0,0,0.14),0px 1px 14px 0px rgba(0,0,0,0.12);",
                "--pro-elevation-6: 0px 3px 5px -1px rgba(0,0,0,0.2),0px 6px 10px 0px rgba(0,0,0,0.14),0px 1px 18px 0px rgba(0,0,0,0.12);",
                "--pro-elevation-7: 0px 4px 5px -2px rgba(0,0,0,0.2),0px 7px 10px 1px rgba(0,0,0,0.14),0px 2px 16px 1px rgba(0,0,0,0.12);",
                "--pro-elevation-8: 0px 5px 5px -3px rgba(0,0,0,0.2),0px 8px 10px 1px rgba(0,0,0,0.14),0px 3px 14px 2px rgba(0,0,0,0.12);",
                "--pro-elevation-9: 0px 5px 6px -3px rgba(0,0,0,0.2),0px 9px 12px 1px rgba(0,0,0,0.14),0px 3px 16px 2px rgba(0,0,0,0.12);",
                "--pro-elevation-10: 0px 6px 6px -3px rgba(0,0,0,0.2),0px 10px 14px 1px rgba(0,0,0,0.14),0px 4px 18px 3px rgba(0,0,0,0.12);",
                "--pro-elevation-11: 0px 6px 7px -4px rgba(0,0,0,0.2),0px 11px 15px 1px rgba(0,0,0,0.14),0px 4px 20px 3px rgba(0,0,0,0.12);",
                "--pro-elevation-12: 0px 7px 8px -4px rgba(0,0,0,0.2),0px 12px 17px 2px rgba(0,0,0,0.14),0px 5px 22px 4px rgba(0,0,0,0.12);",
                "--pro-elevation-13: 0px 7px 8px -4px rgba(0,0,0,0.2),0px 13px 19px 2px rgba(0,0,0,0.14),0px 5px 24px 4px rgba(0,0,0,0.12);",
                "--pro-elevation-14: 0px 7px 9px -4px rgba(0,0,0,0.2),0px 14px 21px 2px rgba(0,0,0,0.14),0px 5px 26px 4px rgba(0,0,0,0.12);",
                "--pro-elevation-15: 0px 8px 9px -5px rgba(0,0,0,0.2),0px 15px 22px 2px rgba(0,0,0,0.14),0px 6px 28px 5px rgba(0,0,0,0.12);",
                "--pro-elevation-16: 0px 8px 10px -5px rgba(0,0,0,0.2),0px 16px 24px 2px rgba(0,0,0,0.14),0px 6px 30px 5px rgba(0,0,0,0.12);",
                "--pro-elevation-17: 0px 8px 11px -5px rgba(0,0,0,0.2),0px 17px 26px 2px rgba(0,0,0,0.14),0px 6px 32px 5px rgba(0,0,0,0.12);",
                "--pro-elevation-18: 0px 9px 11px -5px rgba(0,0,0,0.2),0px 18px 28px 2px rgba(0,0,0,0.14),0px 7px 34px 6px rgba(0,0,0,0.12);",
                "--pro-elevation-19: 0px 9px 12px -6px rgba(0,0,0,0.2),0px 19px 29px 2px rgba(0,0,0,0.14),0px 7px 36px 6px rgba(0,0,0,0.12);",
                "--pro-elevation-20: 0px 10px 13px -6px rgba(0,0,0,0.2),0px 20px 31px 3px rgba(0,0,0,0.14),0px 8px 38px 7px rgba(0,0,0,0.12);",
                "--pro-elevation-21: 0px 10px 13px -6px rgba(0,0,0,0.2),0px 21px 33px 3px rgba(0,0,0,0.14),0px 8px 40px 7px rgba(0,0,0,0.12);",
                "--pro-elevation-22: 0px 10px 14px -6px rgba(0,0,0,0.2),0px 22px 35px 3px rgba(0,0,0,0.14),0px 8px 42px 7px rgba(0,0,0,0.12);",
                "--pro-elevation-23: 0px 11px 14px -7px rgba(0,0,0,0.2),0px 23px 36px 3px rgba(0,0,0,0.14),0px 9px 44px 8px rgba(0,0,0,0.12);",
                "--pro-elevation-24: 0px 11px 15px -7px rgba(0,0,0,0.2),0px 24px 38px 3px rgba(0,0,0,0.14),0px 9px 46px 8px rgba(0,0,0,0.12);",
                "--pro-elevation-25: 0 5px 5px -3px rgba(0,0,0,.06), 0 8px 10px 1px rgba(0,0,0,.042), 0 3px 14px 2px rgba(0,0,0,.036);",
                "--pro-default-borderradius: 4px;",
                "--pro-drawer-width-left: 240px;",
                "--pro-drawer-width-right: 240px;",
                "--pro-drawer-width-mini-left: 56px;",
                "--pro-drawer-width-mini-right: 56px;",
                "--pro-appbar-height: 64px;",
                "--pro-typography-default-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-default-size: .875rem;",
                "--pro-typography-default-weight: 400;",
                "--pro-typography-default-lineheight: 1.43;",
                "--pro-typography-default-letterspacing: .01071em;",
                "--pro-typography-default-text-transform: none;",
                "--pro-typography-h1-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-h1-size: 6rem;",
                "--pro-typography-h1-weight: 300;",
                "--pro-typography-h1-lineheight: 1.167;",
                "--pro-typography-h1-letterspacing: -.01562em;",
                "--pro-typography-h1-text-transform: none;",
                "--pro-typography-h2-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-h2-size: 3.75rem;",
                "--pro-typography-h2-weight: 300;",
                "--pro-typography-h2-lineheight: 1.2;",
                "--pro-typography-h2-letterspacing: -.00833em;",
                "--pro-typography-h2-text-transform: none;",
                "--pro-typography-h3-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-h3-size: 3rem;",
                "--pro-typography-h3-weight: 400;",
                "--pro-typography-h3-lineheight: 1.167;",
                "--pro-typography-h3-letterspacing: 0;",
                "--pro-typography-h3-text-transform: none;",
                "--pro-typography-h4-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-h4-size: 2.125rem;",
                "--pro-typography-h4-weight: 400;",
                "--pro-typography-h4-lineheight: 1.235;",
                "--pro-typography-h4-letterspacing: .00735em;",
                "--pro-typography-h4-text-transform: none;",
                "--pro-typography-h5-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-h5-size: 1.5rem;",
                "--pro-typography-h5-weight: 400;",
                "--pro-typography-h5-lineheight: 1.334;",
                "--pro-typography-h5-letterspacing: 0;",
                "--pro-typography-h5-text-transform: none;",
                "--pro-typography-h6-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-h6-size: 1.25rem;",
                "--pro-typography-h6-weight: 500;",
                "--pro-typography-h6-lineheight: 1.6;",
                "--pro-typography-h6-letterspacing: .0075em;",
                "--pro-typography-h6-text-transform: none;",
                "--pro-typography-subtitle1-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-subtitle1-size: 1rem;",
                "--pro-typography-subtitle1-weight: 400;",
                "--pro-typography-subtitle1-lineheight: 1.75;",
                "--pro-typography-subtitle1-letterspacing: .00938em;",
                "--pro-typography-subtitle1-text-transform: none;",
                "--pro-typography-subtitle2-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-subtitle2-size: .875rem;",
                "--pro-typography-subtitle2-weight: 500;",
                "--pro-typography-subtitle2-lineheight: 1.57;",
                "--pro-typography-subtitle2-letterspacing: .00714em;",
                "--pro-typography-subtitle2-text-transform: none;",
                "--pro-typography-body1-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-body1-size: 1rem;",
                "--pro-typography-body1-weight: 400;",
                "--pro-typography-body1-lineheight: 1.5;",
                "--pro-typography-body1-letterspacing: .00938em;",
                "--pro-typography-body1-text-transform: none;",
                "--pro-typography-body2-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-body2-size: .875rem;",
                "--pro-typography-body2-weight: 400;",
                "--pro-typography-body2-lineheight: 1.43;",
                "--pro-typography-body2-letterspacing: .01071em;",
                "--pro-typography-body2-text-transform: none;",
                "--pro-typography-button-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-button-size: .875rem;",
                "--pro-typography-button-weight: 500;",
                "--pro-typography-button-lineheight: 1.75;",
                "--pro-typography-button-letterspacing: .02857em;",
                "--pro-typography-button-text-transform: uppercase;",
                "--pro-typography-caption-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-caption-size: .75rem;",
                "--pro-typography-caption-weight: 400;",
                "--pro-typography-caption-lineheight: 1.66;",
                "--pro-typography-caption-letterspacing: .03333em;",
                "--pro-typography-caption-text-transform: none;",
                "--pro-typography-overline-family: Roboto, Helvetica, Arial, sans-serif;",
                "--pro-typography-overline-size: .75rem;",
                "--pro-typography-overline-weight: 400;",
                "--pro-typography-overline-lineheight: 2.66;",
                "--pro-typography-overline-letterspacing: .08333em;",
                "--pro-typography-overline-text-transform: none;",
                "--pro-zindex-drawer: 1100;",
                "--pro-zindex-appbar: 1300;",
                "--pro-zindex-dialog: 1400;",
                "--pro-zindex-popover: 1200;",
                "--pro-zindex-snackbar: 1500;",
                "--pro-zindex-tooltip: 1600;",
                "--pro-native-html-color-scheme: light;",
                "}"
            };

            var styleLines = rootStyleNode.InnerHtml.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            styleLines.Should().BeEquivalentTo(expectedLines);
        }

        [Test]
        public void IsDarkMode()
        {
            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.IsDarkMode, true));
            comp.Should().NotBeNull();
            comp.Instance.GetState(x => x.IsDarkMode).Should().BeTrue();
        }

        [Test]
        public void CustomThemeDarkMode()
        {
            var myCustomTheme = new ProTheme
            {
                PaletteDark = new PaletteDark
                {
                    Primary = Colors.Blue.Lighten1,
                    Secondary = "#F50057"
                }
            };
            myCustomTheme.PaletteDark.Primary.Should().Be(new ProColor(Colors.Blue.Lighten1));// Set by user
            myCustomTheme.PaletteDark.Error.Should().Be(new ProColor("#f64e62"));// Default dark overwritten from light
            myCustomTheme.PaletteDark.White.Should().Be(new ProColor(Colors.Shades.White));// Equal in dark and light.
            myCustomTheme.PaletteDark.Secondary.Should().Be(new ProColor("#F50057"));// Setting not in PaletteDark()
        }

        [Test]
        public void CustomThemeDarkModePrimaryDerivateColor()
        {
            // ensure it is backwards compatible by setting Palette() instead of PaletteDark()
            var myCustomTheme = new ProTheme()
            {
                PaletteDark = new PaletteDark()
                {
                    Primary = Colors.Green.Darken1,
                }
            };
            var expectedDarkerColor = new ProColor(Colors.Green.Darken1).ColorRgbDarken();
            myCustomTheme.PaletteDark.Primary.Should().Be(new ProColor(Colors.Green.Darken1));// Set by user
            myCustomTheme.PaletteDark.PrimaryDarken.Should().Be(expectedDarkerColor.ToString(ProColorOutputFormats.RGB));// Set by user

        }

        [Test]
        public void CustomThemeDefault()
        {
            var defaultTheme = new ProTheme();

            //Dark theme
            defaultTheme.PaletteDark.Should().BeOfType<PaletteDark>();
            defaultTheme.PaletteDark.Primary.Should().Be(new ProColor("#776be7"));
            defaultTheme.PaletteDark.Error.Should().Be(new ProColor("#f64e62"));
            defaultTheme.PaletteDark.White.Should().Be(new ProColor(Colors.Shades.White));

            //Light theme
            // Note we're testing against the base type
            defaultTheme.PaletteLight.Should().BeAssignableTo<Palette>();
            defaultTheme.PaletteLight.Primary.Should().Be(new ProColor("#594AE2"));
            defaultTheme.PaletteLight.Error.Should().Be(new ProColor(Colors.Red.Default));
            defaultTheme.PaletteLight.White.Should().Be(new ProColor(Colors.Shades.White));
        }

        [Test]
        public async Task WatchSystemDarkMode()
        {
            var systemMockValue = false;
            Task SystemChangedResult(bool newValue)
            {
                systemMockValue = newValue;
                return Task.CompletedTask;
            }
            var comp = Context.Render<ProThemeProvider>();
            await comp.Instance.WatchSystemDarkModeAsync(SystemChangedResult);
            await comp.Instance.SystemDarkModeChangedAsync(true);
            systemMockValue.Should().BeTrue();
        }

        [Test]
        [TestCase("")]
        [TestCase("root")]
        [TestCase("host")]
        [TestCase(":root")]
        [TestCase(":host")]
        public void PseudoCssScope(string scope)
        {
            var mudTheme = new ProTheme
            {
                PseudoCss = new PseudoCss
                {
                    Scope = scope
                }
            };
            var comp = Context.Render<ProThemeProvider>(parameters => parameters.Add(p => p.Theme, mudTheme));
            comp.Should().NotBeNull();

            var styleNodes = comp.Nodes.OfType<IHtmlStyleElement>().ToArray();

            var rootStyleNode = styleNodes[2];

            var styleLines = rootStyleNode.InnerHtml.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            if (string.IsNullOrEmpty(scope))
            {
                scope = ":root";
            }

            if (!scope.StartsWith(':'))
            {
                scope = $":{scope}";
            }

            styleLines.Should().Contain($"{scope}{{");
        }

        [Test]
        public void PseudoCssRootColor()
        {
            const string Scope = ":root";
            var mudTheme = new ProTheme
            {
                PaletteDark = new PaletteDark
                {
                    Primary = Colors.Green.Darken1,
                },
                PseudoCss = new PseudoCss
                {
                    Scope = Scope
                }
            };
            var comp = Context.Render<ProThemeProvider>(
                parameters =>
                    parameters.Add(p => p.Theme, mudTheme)
                        .Add(p => p.IsDarkMode, true)
            );
            comp.Should().NotBeNull();

            var styleNodes = comp.Nodes.OfType<IHtmlStyleElement>().ToArray();

            var rootStyleNode = styleNodes[2];

            var styleLines = rootStyleNode.InnerHtml.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            styleLines.Should().Contain($"{Scope}{{");

            var expectedPrimaryColor = Colors.Green.Darken1;
            var expectedPrimaryColorAsRgba = new ProColor(expectedPrimaryColor).ToString(ProColorOutputFormats.RGBA);
            var expectedPrimaryLine = $"--pro-palette-primary: {expectedPrimaryColorAsRgba};";
            styleLines.Should().Contain(expectedPrimaryLine);
            var expectedPrimaryDarkenColor = new ProColor(expectedPrimaryColor).ColorRgbDarken();
            var expectedPrimaryDarkenColorAsRgb = expectedPrimaryDarkenColor.ToString(ProColorOutputFormats.RGB);
            var expectedPrimaryDarkenLine = $"--pro-palette-primary-darken: {expectedPrimaryDarkenColorAsRgb};";
            styleLines.Should().Contain(expectedPrimaryDarkenLine);
        }

        [Test]
        public async Task ObserveSystemDarkModeChange()
        {
            // Arrange & Act
            Context.JSInterop.SetupVoid("mudThemeProvider.stopWatchingDarkMode");
            Context.JSInterop.SetupVoid("mudThemeProvider.watchDarkMode");
            var themeProvider = Context.Render<ThemeProviderObserveSystemDarkModeChangeTest>();

            // Assert
            Context.JSInterop.VerifyNotInvoke("mudThemeProvider.watchDarkMode");
            Context.JSInterop.VerifyNotInvoke("mudThemeProvider.stopWatchingDarkMode");

            // Act
            await themeProvider.InvokeAsync(themeProvider.Instance.EnableObserve);

            // Assert
            Context.JSInterop.VerifyInvoke("mudThemeProvider.watchDarkMode", 1);
            Context.JSInterop.VerifyNotInvoke("mudThemeProvider.stopWatchingDarkMode");

            // Act
            await themeProvider.InvokeAsync(themeProvider.Instance.DisableObserve);

            // Assert
            Context.JSInterop.VerifyInvoke("mudThemeProvider.watchDarkMode", 1);
            Context.JSInterop.VerifyInvoke("mudThemeProvider.stopWatchingDarkMode", 1);
        }

        [Test]
        public async Task Dispose_ShouldInvokeJs()
        {
            // Arrange
            Context.JSInterop.SetupVoid("mudThemeProvider.stopWatchingDarkMode");
            Context.Render<ProThemeProvider>();

            //Act
            await Context.DisposeComponentsAsync();

            // Assert
            Context.JSInterop.VerifyInvoke("mudThemeProvider.stopWatchingDarkMode");
        }

        [Test]
        public void RenderComponent_ShouldInvokeJs()
        {
            // Act & Arrange
            Context.JSInterop.SetupVoid("mudThemeProvider.watchDarkMode");
            Context.Render<ProThemeProvider>();

            // Assert
            Context.JSInterop.VerifyInvoke("mudThemeProvider.watchDarkMode");
        }

        [Test]
        public void ThemeProvider_ShouldHave_ClassName()
        {
            const string Scope = ":root";
            var mudTheme = new ProTheme
            {
                PaletteDark = new PaletteDark
                {
                    Primary = Colors.Green.Darken1,
                },
                PseudoCss = new PseudoCss
                {
                    Scope = Scope
                }
            };
            var comp = Context.Render<ProThemeProvider>(
                parameters =>
                    parameters.Add(p => p.Theme, mudTheme)
                        .Add(p => p.IsDarkMode, true)
            );
            comp.Should().NotBeNull();

            var styleNodes = comp.Nodes.OfType<IHtmlStyleElement>().ToArray();

            var rootStyleNode = styleNodes[2];
            rootStyleNode.ClassName.Should().Be("pro-theme-provider");
        }

        [Test]
        public void CurrentPalette_ShouldBeLight_WhenNotInDarkMode()
        {
            // Arrange & Act
            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.IsDarkMode, false));

            // Assert
            comp.Instance.GetState(x => x.CurrentPalette).Should().NotBeNull();
            comp.Instance.GetState(x => x.CurrentPalette).Should().BeOfType<PaletteLight>();
        }

        [Test]
        public void CurrentPalette_ShouldBeDark_WhenInDarkMode()
        {
            // Arrange & Act
            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.IsDarkMode, true));

            // Assert
            comp.Instance.GetState(x => x.CurrentPalette).Should().NotBeNull();
            comp.Instance.GetState(x => x.CurrentPalette).Should().BeOfType<PaletteDark>();
        }

        [Test]
        public async Task CurrentPalette_ShouldUpdateToLight_WhenDarkModeChangesToFalse()
        {
            // Arrange
            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.IsDarkMode, true));

            // Verify initial state
            comp.Instance.GetState(x => x.CurrentPalette).Should().BeOfType<PaletteDark>();

            // Act
            await comp.SetParametersAndRenderAsync(parameters => parameters
                .Add(p => p.IsDarkMode, false));

            // Assert
            comp.Instance.GetState(x => x.CurrentPalette).Should().BeOfType<PaletteLight>();
        }

        [Test]
        public async Task CurrentPalette_ShouldUpdateToDark_WhenDarkModeChangesToTrue()
        {
            // Arrange
            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.IsDarkMode, false));

            // Verify initial state
            comp.Instance.GetState(x => x.CurrentPalette).Should().BeOfType<PaletteLight>();

            // Act
            await comp.SetParametersAndRenderAsync(parameters => parameters
                .Add(p => p.IsDarkMode, true));

            // Assert
            comp.Instance.GetState(x => x.CurrentPalette).Should().BeOfType<PaletteDark>();
        }

        [Test]
        public void CurrentPalette_ShouldUseCustomTheme_WhenProvided()
        {
            // Arrange
            var customTheme = new ProTheme
            {
                PaletteLight = new PaletteLight
                {
                    Primary = Colors.Green.Default
                }
            };

            // Act
            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.Theme, customTheme)
                .Add(p => p.IsDarkMode, false));

            // Assert
            var currentPalette = comp.Instance.GetState(x => x.CurrentPalette);
            currentPalette.Should().NotBeNull();
            currentPalette!.Primary.Should().Be(new ProColor(Colors.Green.Default));
        }

        [Test]
        public async Task CurrentPaletteChanged_ShouldFire_WhenDarkModeChanges()
        {
            // Arrange
            Palette? capturedPalette = null;
            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.IsDarkMode, false)
                .Add(p => p.CurrentPaletteChanged, EventCallback.Factory.Create<Palette?>(this, palette => capturedPalette = palette)));

            // Act
            await comp.SetParametersAndRenderAsync(parameters => parameters
                .Add(p => p.IsDarkMode, true));

            // Assert
            capturedPalette.Should().NotBeNull();
            capturedPalette.Should().BeOfType<PaletteDark>();
        }

        [Test]
        public async Task CurrentPalette_ShouldUpdate_WhenThemeChanges()
        {
            // Arrange
            var theme1 = new ProTheme
            {
                PaletteLight = new PaletteLight
                {
                    Primary = Colors.Blue.Default
                }
            };

            var theme2 = new ProTheme
            {
                PaletteLight = new PaletteLight
                {
                    Primary = Colors.Red.Default
                }
            };

            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.Theme, theme1)
                .Add(p => p.IsDarkMode, false));

            // Verify initial state
            comp.Instance.GetState(x => x.CurrentPalette)!.Primary.Should().Be(new ProColor(Colors.Blue.Default));

            // Act
            await comp.SetParametersAndRenderAsync(parameters => parameters
                .Add(p => p.Theme, theme2));

            // Assert
            comp.Instance.GetState(x => x.CurrentPalette)!.Primary.Should().Be(new ProColor(Colors.Red.Default));
        }

        [Test]
        public async Task CurrentPalette_ShouldReflectBothDarkModeAndThemeChanges()
        {
            // Arrange
            var customTheme = new ProTheme
            {
                PaletteLight = new PaletteLight
                {
                    Primary = Colors.Blue.Default
                },
                PaletteDark = new PaletteDark
                {
                    Primary = Colors.Green.Default
                }
            };

            var comp = Context.Render<ProThemeProvider>(parameters => parameters
                .Add(p => p.Theme, customTheme)
                .Add(p => p.IsDarkMode, false));

            // Verify light mode
            comp.Instance.GetState(x => x.CurrentPalette)!.Primary.Should().Be(new ProColor(Colors.Blue.Default));

            // Act - switch to dark mode
            await comp.SetParametersAndRenderAsync(parameters => parameters
                .Add(p => p.IsDarkMode, true));

            // Assert - should use dark palette
            comp.Instance.GetState(x => x.CurrentPalette)!.Primary.Should().Be(new ProColor(Colors.Green.Default));
        }

        [Test]
        public async Task CurrentPalette_Binding_ShouldWorkInTestComponent()
        {
            // Arrange
            var comp = Context.Render<ThemeProviderCurrentPaletteTest>();

            // Initial state - light mode
            comp.Instance.GetCurrentPalette().Should().BeOfType<PaletteLight>();

            // Act - toggle to dark mode
            await comp.InvokeAsync(() => comp.Find("button").Click());

            // Assert - should be dark palette
            comp.Instance.GetCurrentPalette().Should().BeOfType<PaletteDark>();

            // Act - toggle back to light mode
            await comp.InvokeAsync(() => comp.Find("button").Click());

            // Assert - should be light palette again
            comp.Instance.GetCurrentPalette().Should().BeOfType<PaletteLight>();
        }
    }
}
