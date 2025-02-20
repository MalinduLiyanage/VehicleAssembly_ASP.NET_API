using MimeKit;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;

namespace Vehicle_Assembly.Utilities.EmailService
{
    public interface IEmailService
    {
        public Task<BaseResponse> SendEmail(MimeMessage msg);
    }
}
