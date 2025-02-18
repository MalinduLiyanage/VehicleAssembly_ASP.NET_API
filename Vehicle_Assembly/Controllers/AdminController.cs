using Microsoft.AspNetCore.Mvc;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Services.AdminService;
using Vehicle_Assembly.Services.VehicleService;

namespace Vehicle_Assembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public BaseResponse AdminList()
        {
            return adminService.GetAdmins();
        }

        [HttpPost]
        public BaseResponse AddAdmin(PutAdminRequest request)
        {
            return adminService.PutAdmin(request);
        }


    }
}
