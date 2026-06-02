// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.UnitTests;

public static class DialogExtensions
{
    public static ProDialogContainer GetDialogContainer(this IMudDialogInstance instance)
    {
        ArgumentNullException.ThrowIfNull(instance);

        if (instance is ProDialogContainer container)
        {
            return container;
        }

        throw new InvalidOperationException("Dialog instance not found!");
    }
}
