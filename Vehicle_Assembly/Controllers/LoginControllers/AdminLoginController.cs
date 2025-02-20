using Microsoft.AspNetCore.Mvc;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Services.AdminService;

namespace Vehicle_Assembly.Controllers.LoginControllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminLoginController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost]
        public BaseResponse LoginAdmin(AuthenticateRequest request)
        {
            return adminService.LoginAdmin(request);
        }


    }
}
