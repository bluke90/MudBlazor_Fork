// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ProtonBlazor.Analyzers.Internal;

internal static class SyntaxNodeExtensions
{
    public static ClassDeclarationSyntax? FindClass(this SyntaxNode? node)
    {
        while (node is not null && node is not ClassDeclarationSyntax)
        {
            node = node.Parent;
        }

        return (ClassDeclarationSyntax?)node;
    }
}
