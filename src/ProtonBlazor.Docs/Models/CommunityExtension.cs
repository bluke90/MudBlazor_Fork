// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace ProtonBlazor.Docs.Models;

[DebuggerDisplay($"Name = {nameof(Name)}")]
public class CommunityExtension
{
    public string AvatarImageSrc => @$"_content/ProtonBlazor.Docs/images/extensions/{GitHubUserPath}.{GitHubRepoPath}.webp";

    public required string Category { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required string Link { get; set; }

    public required string GitHubUserPath { get; set; }

    public required string GitHubRepoPath { get; set; }

    public string GitHubLink => @$"https://github.com/{GitHubUserPath}/{GitHubRepoPath}";
}
