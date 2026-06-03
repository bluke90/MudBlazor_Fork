using System;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.Interfaces;

namespace ProtonBlazor
{
    public class ActivatableCallback : IActivatable
    {
        public Action<object, MouseEventArgs>? ActivateCallback { get; set; }

        public void Activate(object sender, MouseEventArgs args)
        {
            ActivateCallback?.Invoke(sender, args);
        }
    }
}
