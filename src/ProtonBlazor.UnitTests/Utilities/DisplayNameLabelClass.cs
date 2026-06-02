// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.UnitTests.Utilities
{
    public class DisplayNameLabelClass
    {
        [Label("Date LabelAttribute")]
        public DateTime? Date { get; set; }
        [Label("Boolean LabelAttribute")]
        public bool Boolean { get; set; }
        [Label("String LabelAttribute")]
        public string String { get; set; }
    }
}
