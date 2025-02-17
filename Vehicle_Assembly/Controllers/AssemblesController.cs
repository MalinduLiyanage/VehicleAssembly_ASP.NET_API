using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.Models;
using System.Threading.Tasks;

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

        // POST: api/assembles
        [HttpPost]
        public async Task<ActionResult<AssembleModel>> CreateAssemble([FromBody] AssembleRequest newAssembleRequest)
        {
            // Validate request body fields (vehicle_id, nic, date, isCompleted)
            if (newAssembleRequest.vehicle_id <= 0 || newAssembleRequest.nic <= 0 || newAssembleRequest.date == default || newAssembleRequest.isCompleted == null)
            {
                return BadRequest(new { message = "All fields are mandatory!" });
            }

            // Check if the vehicle exists
            var vehicleExists = await _context.vehicle.AnyAsync(v => v.vehicle_id == newAssembleRequest.vehicle_id);
            if (!vehicleExists)
            {
                return BadRequest(new { message = "Invalid Vehicle ID. Vehicle does not exist." });
            }

            // Check if the worker exists
            var workerExists = await _context.worker.AnyAsync(w => w.NIC == newAssembleRequest.nic);
            if (!workerExists)
            {
                return BadRequest(new { message = "Invalid NIC. Worker does not exist." });
            }

            // Create the Assemble record
            var newAssemble = new AssembleModel
            {
                vehicle_id = newAssembleRequest.vehicle_id,
                NIC = newAssembleRequest.nic,
                date = newAssembleRequest.date,
                isCompleted = newAssembleRequest.isCompleted
            };

            _context.assembles.Add(newAssemble);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssembles), new { vehicle_id = newAssemble.vehicle_id, worker_id = newAssemble.NIC }, newAssemble);
        }
    }
    
}
