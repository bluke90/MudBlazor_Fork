// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using ProtonBlazor.Utilities;

namespace ProtonBlazor
{

    /// <summary>
    /// Displays items in chronological order.
    /// </summary>
    /// <seealso cref="ProTimelineItem"/>
    public partial class ProTimeline : ProBaseItemsControl<ProTimelineItem>
    {
        protected string Classnames =>
            new CssBuilder("pro-timeline")
                .AddClass($"pro-timeline-{TimelineOrientation.ToStringFast(true)}")
                .AddClass($"pro-timeline-position-{ConvertTimelinePosition().ToStringFast(true)}")
                .AddClass($"pro-timeline-reverse", Reverse && TimelinePosition == TimelinePosition.Alternate)
                .AddClass($"pro-timeline-align-{TimelineAlign.ToStringFast(true)}")
                .AddClass($"pro-timeline-modifiers", Modifiers)
                .AddClass($"pro-timeline-rtl", RightToLeft)
                .AddClass(Class)
                .Build();

        /// <summary>
        /// Displays content right-to-left.
        /// </summary>
        [CascadingParameter(Name = "RightToLeft")]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// The orientation of the timeline and its items.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="TimelineOrientation.Vertical"/>.<br />
        /// When set to <see cref="TimelineOrientation.Vertical"/>, <see cref="TimelinePosition"/> can be set to <c>Left</c>, <c>Right</c>, <c>Alternate</c>, <c>Start</c>, or <c>End</c>.<br />
        /// When set to <see cref="TimelineOrientation.Horizontal"/>, <see cref="TimelinePosition"/> can be set to <c>Top</c>, <c>Bottom</c>, or <c>Alternate</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Timeline.Behavior)]
        public TimelineOrientation TimelineOrientation { get; set; } = TimelineOrientation.Vertical;

        /// <summary>
        /// The position the timeline and how its items are displayed.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="TimelinePosition.Alternate"/>.<br />
        /// Can be set to <c>Left</c>, <c>Right</c>, <c>Alternate</c>, <c>Start</c>, or <c>End</c> when <see cref="TimelineOrientation"/> is <see cref="TimelineOrientation.Vertical"/>.<br />
        /// Can be set to <c>Top</c>, <c>Bottom</c>, or <c>Alternate</c> when <see cref="TimelineOrientation"/> is <see cref="TimelineOrientation.Horizontal"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Timeline.Behavior)]
        public TimelinePosition TimelinePosition { get; set; } = TimelinePosition.Alternate;

        /// <summary>
        /// The position of each item's dot relative to its text.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="TimelineAlign.Default"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Timeline.Behavior)]
        public TimelineAlign TimelineAlign { get; set; } = TimelineAlign.Default;

        /// <summary>
        /// Reverses the order of items when <see cref="TimelinePosition"/> is <see cref="TimelinePosition.Alternate"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Timeline.Behavior)]
        public bool Reverse { get; set; } = false;

        /// <summary>
        /// Enables modifiers for items, such as adding a caret for a <see cref="ProCard"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Timeline.Behavior)]
        public bool Modifiers { get; set; } = true;

        private TimelinePosition ConvertTimelinePosition()
        {
            if (TimelineOrientation == TimelineOrientation.Vertical)
            {
                return TimelinePosition switch
                {
                    TimelinePosition.Left => RightToLeft ? TimelinePosition.End : TimelinePosition.Start,
                    TimelinePosition.Right => RightToLeft ? TimelinePosition.Start : TimelinePosition.End,
                    TimelinePosition.Top => TimelinePosition.Alternate,
                    TimelinePosition.Bottom => TimelinePosition.Alternate,
                    _ => TimelinePosition
                };
            }

            return TimelinePosition switch
            {
                TimelinePosition.Start => TimelinePosition.Alternate,
                TimelinePosition.Left => TimelinePosition.Alternate,
                TimelinePosition.Right => TimelinePosition.Alternate,
                TimelinePosition.End => TimelinePosition.Alternate,
                _ => TimelinePosition
            };
        }
    }
}
