using Microsoft.AspNetCore.Mvc;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Services.EmailService;
using Vehicle_Assembly.Services.VehicleService;

namespace Vehicle_Assembly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task<BaseResponse> SendEmail(SendEmailRequest request)
        {
            return await emailService.SendEmail(request);
        }

    }
}
