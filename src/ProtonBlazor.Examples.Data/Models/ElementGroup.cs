using System.Collections.Generic;

namespace ProtonBlazor.Examples.Data.Models;

public class ElementGroup
{
    public string? Wiki { get; set; }

    public IReadOnlyCollection<Element>? Elements { get; set; }
}
