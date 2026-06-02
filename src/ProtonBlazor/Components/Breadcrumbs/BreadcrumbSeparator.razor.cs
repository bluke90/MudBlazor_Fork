// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ProtonBlazor;


/// <summary>
/// Represents a divider between breadcrumb items.
/// </summary>
/// <seealso cref="ProBreadcrumbs" />
/// <seealso cref="BreadcrumbItem" />
/// <seealso cref="BreadcrumbLink" />
public partial class BreadcrumbSeparator
{
    /// <summary>
    /// The parent breadcrumb component.
    /// </summary>
    [CascadingParameter]
    public ProBreadcrumbs? Parent { get; set; }
}
