using Microsoft.AspNetCore.Mvc;
using Playground.API.Filters;

namespace Playground.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}
