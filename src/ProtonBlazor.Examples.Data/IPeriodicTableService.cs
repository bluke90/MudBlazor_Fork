using System.Collections.Generic;
using System.Threading.Tasks;
using ProtonBlazor.Examples.Data.Models;

namespace ProtonBlazor.Examples.Data;

public interface IPeriodicTableService
{
    Task<IEnumerable<Element>> GetElements();

    Task<IEnumerable<Element>> GetElements(string search);
}
