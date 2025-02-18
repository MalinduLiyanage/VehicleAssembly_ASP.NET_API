using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;

namespace Vehicle_Assembly.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<BaseResponse> SendEmail(SendEmailRequest request)
        {
            BaseResponse response = new BaseResponse();

            var emailSettings = configuration.GetSection("EmailSettings");
            String senderEmail = emailSettings["SenderEmail"];
            String senderName = emailSettings["SenderName"];
            String senderPassword = emailSettings["SenderPassword"];
            String smtpServer = emailSettings["SmtpServer"];
            int smtpPort = int.Parse(emailSettings["SmtpPort"]);

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(request.recipientName, request.recipientEmail));
            message.Subject = request.subject;
            message.Body = new TextPart("plain") { Text = request.body };

            //message.To.Add(new MailboxAddress("request.recipientName", "email@gmail.com"));
            //message.Subject = "equest.subject";
            //message.Body = new TextPart("plain") { Text = "request.body" };

            using (SmtpClient client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(senderEmail, senderPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { message = "Email sent successfully!" }
                    };
                }
                catch (Exception ex)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status500InternalServerError,
                        data = new { message = "Failed to send email.", error = ex.Message }
                    };
                }
            }

            return response;
        }

    }
}
