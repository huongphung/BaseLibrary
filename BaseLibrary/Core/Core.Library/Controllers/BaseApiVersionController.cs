using Microsoft.AspNetCore.Mvc;

namespace Core.Library.Controllers
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiVersionController : ControllerBase
    {
    }
}
