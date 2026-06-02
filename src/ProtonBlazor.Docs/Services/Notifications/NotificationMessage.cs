// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace ProtonBlazor.Docs.Services.Notifications;

public record NotificationMessage(string Id, string Title, string Except, string Category, DateTime PublishDate, string ImgUrl, IEnumerable<NotificationAuthor> Authors, Type ContentComponent);
