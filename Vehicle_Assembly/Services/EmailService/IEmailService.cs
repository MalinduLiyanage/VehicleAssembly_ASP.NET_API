using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;

namespace Vehicle_Assembly.Services.EmailService
{
    public interface IEmailService
    {
        public Task<BaseResponse> SendEmail(SendEmailRequest request);
    }
}
