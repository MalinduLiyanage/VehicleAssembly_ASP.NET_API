using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Services.VehicleService;
using Vehicle_Assembly.Services.WorkerService;

namespace Vehicle_Assembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService workerService;

        public WorkerController(IWorkerService workerService)
        {
            this.workerService = workerService;
        }

        [HttpGet]
        public BaseResponse WorkerList()
        {
            return workerService.GetWorkers();
        }

        [HttpPost]
        public BaseResponse AddWorker(PutWorkerRequest request)
        {
            return workerService.PutWorker(request);
        }
    }
}
