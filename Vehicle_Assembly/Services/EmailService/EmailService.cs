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
            BodyBuilder bodyBuilder = new BodyBuilder();

            string emailTemplate = Path.Combine(Directory.GetCurrentDirectory(), "Views", "SendEmailView", "SendEmailTemplate.html");
            string emailBody = File.ReadAllText(emailTemplate);
            emailBody = emailBody.Replace("{{WorkerName}}", request.request.WorkerName)
                .Replace("{{model}}", request.request.model)
                .Replace("{{vehicle_id}}", "" + request.request.vehicle_id)
                .Replace("{{date}}", "" + request.request.date)
                .Replace("{{assignee_first_name}}", request.request.assignee_first_name)
                .Replace("{{assignee_last_name}}", request.request.assignee_last_name)
                .Replace("{{assignee_id}}", "" + request.request.assignee_id);
            bodyBuilder.HtmlBody = emailBody;

            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(request.request.WorkerName, request.request.email));
            message.Subject = "Assemble Job Assignment - " + request.request.date;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = bodyBuilder.HtmlBody };

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
                        data = new { message = "Notification Email sent successfully!" }
                    };
                }
                catch (Exception ex)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status500InternalServerError,
                        data = new { message = "Failed to send the Notification email.", error = ex.Message }
                    };
                }
            }

            return response;
        }

    }
}
