using Microsoft.AspNetCore.Mvc;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Services.AssembleService;
using Vehicle_Assembly.Services.AssmblyService;

namespace Vehicle_Assembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssemblesController : ControllerBase
    {
        private readonly IAssembleService assembleService;

        public AssemblesController(IAssembleService assembleService)
        {
            this.assembleService = assembleService;
        }

        [HttpGet]
        public BaseResponse GetAssembles([FromQuery] int? vehicle_id, [FromQuery] int? worker_id, [FromQuery] int? assignee_id)
        {
            return assembleService.GetAssembles(vehicle_id, worker_id, assignee_id);
        }

        [HttpPost]
        public BaseResponse CreateAssemble(PutAssembleRequest request)
        {
            return assembleService.CreateAssemble(request);
        }
    }
}
