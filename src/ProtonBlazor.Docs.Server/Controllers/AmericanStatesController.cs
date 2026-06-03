using Microsoft.AspNetCore.Mvc;
using ProtonBlazor.Examples.Data;

namespace ProtonBlazor.Docs.Server.Controllers;

[Route("wasm/webapi/[controller]")]
[Route("webapi/[controller]")]
[ApiController]
public class AmericanStatesController : ControllerBase
{
    [HttpGet("{search}")]
    public IEnumerable<string> Get(string search) => AmericanStates.GetStates(search);

    [HttpGet]
    public IEnumerable<string> Get() => AmericanStates.GetStates();
}
