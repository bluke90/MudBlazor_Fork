using System;
using Microsoft.AspNetCore.Components;

namespace ProtonBlazor;

// used in ProCollapse
[EventHandler("ontransitionend", typeof(EventArgs), enableStopPropagation: true, enablePreventDefault: false)]
public static class EventHandlers;
