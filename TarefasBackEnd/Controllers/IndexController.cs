using Microsoft.AspNetCore.Mvc;

namespace TarefasBackEnd.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
