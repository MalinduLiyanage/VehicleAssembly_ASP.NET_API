using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Vehicle_Assembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssemblesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssemblesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAssembles(
            [FromQuery] int? vehicle_id,
            [FromQuery] int? worker_id)
        {
            var query = _context.assembles
                .Include(a => a.Vehicle)
                .Include(a => a.Worker)
                .AsQueryable();

            if (vehicle_id.HasValue)
            {
                query = query.Where(a => a.vehicle_id == vehicle_id.Value);
            }

            if (worker_id.HasValue)
            {
                query = query.Where(a => a.NIC == worker_id.Value);
            }

            var result = await query.Select(a => new
            {
                a.vehicle_id,
                VehicleModel = a.Vehicle.model,
                VehicleColor = a.Vehicle.color,
                VehicleEngine = a.Vehicle.engine,
                WorkerNIC = a.NIC,
                WorkerName = $"{a.Worker.firstname} {a.Worker.lastname}",
                WorkerJobRole = a.Worker.job_role,
                a.date,
                a.isCompleted
            }).ToListAsync();

            return Ok(result);
        }
    }
}
