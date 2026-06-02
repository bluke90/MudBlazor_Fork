// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components.Web;

namespace ProtonBlazor
{
    /// <summary>
    /// Information about a swipe event when <see cref="ProSwipeArea.OnSwipeEnd"/> occurs.
    /// </summary>
    public class SwipeEventArgs
    {
        /// <summary>
        /// The information about the pointer.
        /// </summary>
        public PointerEventArgs TouchEventArgs { get; }

        /// <summary>
        /// The distance of the swipe gesture, in pixels.
        /// </summary>
        public double? SwipeDelta { get; }

        /// <summary>
        /// The <see cref="ProSwipeArea"/> which raised the swipe event.
        /// </summary>
        public ProSwipeArea Sender { get; }

        /// <summary>
        /// The direction of the swipe.
        /// </summary>
        public SwipeDirection SwipeDirection { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwipeEventArgs"/> class.
        /// </summary>
        /// <param name="touchEventArgs">The size, pressure, and tilt of the pointer.</param>
        /// <param name="swipeDirection">The direction of the swipe.</param>
        /// <param name="swipeDelta">The distance of the swipe movement, in pixels.</param>
        /// <param name="sender">The <see cref="ProSwipeArea" /> which originated the swipe event.</param>
        public SwipeEventArgs(PointerEventArgs touchEventArgs, SwipeDirection swipeDirection, double? swipeDelta, ProSwipeArea sender)
        {
            TouchEventArgs = touchEventArgs;
            SwipeDirection = swipeDirection;
            SwipeDelta = swipeDelta;
            Sender = sender;
        }
    }
}
