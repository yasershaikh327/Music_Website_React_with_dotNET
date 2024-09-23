using Microsoft.AspNetCore.Mvc;

namespace ReactApp.Server.Areas.Error.Controllers
{
    [ApiController]
    [Area("Error")]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet("GetError")] // Ensure the action is defined correctly
        public IActionResult GetError(string errorMessage)
        {
            return BadRequest(new { message = errorMessage });
        }
    }

}
