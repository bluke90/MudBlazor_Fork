// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using ProtonBlazor.Charts;


namespace ProtonBlazor
{
    /// <summary>
    /// Indicates the technique used to smooth lines connecting values in a <see cref="ProAxisLineChartBase{T, TOptions}"/>.
    /// </summary>
    public enum InterpolationOption
    {
        /// <summary>
        /// Lines are smoothed to pass through each data point as a natural spline.
        /// </summary>
        NaturalSpline,

        /// <summary>
        /// Lines are smoothed to pass through each data point as an end-point spline.
        /// </summary>
        EndSlope,

        /// <summary>
        /// Lines are smoothed to pass through each data point as a periodic spline.
        /// </summary>
        Periodic,

        /// <summary>
        /// Lines are straight from one point to the next.
        /// </summary>
        Straight
    }
}
