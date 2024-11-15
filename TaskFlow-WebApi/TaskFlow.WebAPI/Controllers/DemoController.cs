using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemoController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Good Job ");
    }
}