using System.Threading.Tasks;

namespace ProtonBlazor.Interfaces
{
    public interface INavigationEventReceiver
    {
        Task OnNavigation();
    }
}
