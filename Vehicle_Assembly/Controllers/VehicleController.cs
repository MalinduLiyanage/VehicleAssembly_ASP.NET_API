using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.Models;

namespace Vehicle_Assembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.vehicle.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest("Invalid vehicle data.");
            }

            if (string.IsNullOrWhiteSpace(vehicle.model) ||
                string.IsNullOrWhiteSpace(vehicle.color) ||
                string.IsNullOrWhiteSpace(vehicle.engine))
            {
                return BadRequest("All fields are required.");
            }

            _context.vehicle.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicles), new { id = vehicle.vehicle_id }, vehicle);
        }
    }
}
