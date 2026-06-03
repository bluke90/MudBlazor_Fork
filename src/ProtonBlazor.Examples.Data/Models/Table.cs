using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProtonBlazor.Examples.Data.Models;

public class Table
{
    [JsonPropertyName("table")]
    public IReadOnlyCollection<ElementGroup>? ElementGroups { get; set; }
}
