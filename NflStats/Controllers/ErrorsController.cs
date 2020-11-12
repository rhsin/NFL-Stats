using Microsoft.AspNetCore.Mvc;

namespace NflStats.Controllers
{
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("/Error")]
        public IActionResult Error() => Problem();
    }
}
