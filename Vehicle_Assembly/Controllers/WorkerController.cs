using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.Models;

namespace Vehicle_Assembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerModel>>> GetWorkers()
        {
            return await _context.worker.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<WorkerModel>> PostWorker(WorkerModel worker)
        {
            if (worker == null)
            {
                return BadRequest("Invalid worker data.");
            }

            if ((worker.NIC) <= 0 ||
                string.IsNullOrWhiteSpace(worker.firstname) ||
                string.IsNullOrWhiteSpace(worker.lastname) ||
                string.IsNullOrWhiteSpace(worker.email) ||
                string.IsNullOrWhiteSpace(worker.address) ||
                string.IsNullOrWhiteSpace(worker.job_role))
            {
                return BadRequest("All fields are required.");
            }

            _context.worker.Add(worker);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkers), new { id = worker.NIC }, worker);
        }
    }
}
