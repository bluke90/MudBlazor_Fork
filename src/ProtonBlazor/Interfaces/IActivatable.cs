using Microsoft.AspNetCore.Components.Web;

namespace ProtonBlazor.Interfaces
{
    public interface IActivatable
    {
        void Activate(object activator, MouseEventArgs args);
    }
}
