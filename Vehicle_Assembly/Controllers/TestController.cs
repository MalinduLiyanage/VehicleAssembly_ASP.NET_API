using Microsoft.AspNetCore.Mvc;

namespace Vehicle_Assembly.Controllers;
[Route("api/DB[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TestController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult TestDatabase()
    {
        return Ok(_context.Database.CanConnect());
    }
}

