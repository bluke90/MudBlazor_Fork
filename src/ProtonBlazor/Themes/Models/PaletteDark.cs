// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.Utilities;

namespace ProtonBlazor
{
    /// <summary>
    /// Represents a dark color palette.
    /// </summary>
    public class PaletteDark : Palette
    {
        /// <inheritdoc />
        public override ProColor Black { get; set; } = "#27272f";

        /// <inheritdoc />
        public override ProColor Primary { get; set; } = "#818CF8";

        /// <inheritdoc />
        public override ProColor Info { get; set; } = "#3299ff";

        /// <inheritdoc />
        public override ProColor Success { get; set; } = "#0bba83";

        /// <inheritdoc />
        public override ProColor Warning { get; set; } = "#ffa800";

        /// <inheritdoc />
        public override ProColor Error { get; set; } = "#f64e62";

        /// <inheritdoc />
        public override ProColor Dark { get; set; } = "#27272f";

        /// <inheritdoc />
        public override ProColor TextPrimary { get; set; } = "rgba(255,255,255, 0.70)";

        /// <inheritdoc />
        public override ProColor TextSecondary { get; set; } = "rgba(255,255,255, 0.50)";

        /// <inheritdoc />
        public override ProColor TextDisabled { get; set; } = "rgba(255,255,255, 0.2)";

        /// <inheritdoc />
        public override ProColor ActionDefault { get; set; } = "#adadb1";

        /// <inheritdoc />
        public override ProColor ActionDisabled { get; set; } = "rgba(255,255,255, 0.26)";

        /// <inheritdoc />
        public override ProColor ActionDisabledBackground { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override ProColor Background { get; set; } = "#32333d";

        /// <inheritdoc />
        public override ProColor BackgroundGray { get; set; } = "#27272f";

        /// <inheritdoc />
        public override ProColor Surface { get; set; } = "#373740";

        /// <inheritdoc />
        public override ProColor DrawerBackground { get; set; } = "#27272f";

        /// <inheritdoc />
        public override ProColor DrawerText { get; set; } = "rgba(255,255,255, 0.50)";

        /// <inheritdoc />
        public override ProColor DrawerIcon { get; set; } = "rgba(255,255,255, 0.50)";

        /// <inheritdoc />
        public override ProColor AppbarBackground { get; set; } = "#27272f";

        /// <inheritdoc />
        public override ProColor AppbarText { get; set; } = "rgba(255,255,255, 0.70)";

        /// <inheritdoc />
        public override ProColor LinesDefault { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override ProColor LinesInputs { get; set; } = "rgba(255,255,255, 0.3)";

        /// <inheritdoc />
        public override ProColor TableLines { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override ProColor TableStriped { get; set; } = "rgba(255,255,255, 0.2)";

        /// <inheritdoc />
        public override ProColor Divider { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override ProColor DividerLight { get; set; } = "rgba(255,255,255, 0.06)";

        /// <inheritdoc />
        public override ProColor Skeleton { get; set; } = "rgba(255,255,255, 0.11)";
    }
}
