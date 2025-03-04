using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Vehicle_Assembly.DTOs.Requests.EmailRequests;
using Vehicle_Assembly.DTOs.Responses;

namespace Vehicle_Assembly.Utilities.EmailService.AssemblyEmail
{
    public class AssemblyEmailSender : MimeMessage
    {
        public AssemblyEmailSender(SendEmailRequest request, Stream? attachmentStream = null, string? attachmentFileName = null)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();

            string emailTemplate = Path.Combine(Directory.GetCurrentDirectory(), "..", "Vehicle_Assembly", "Views", "SendEmailView", "SendEmailTemplate.html");
            string emailBody = File.ReadAllText(emailTemplate);

            emailBody = emailBody.Replace("{{WorkerName}}", request.request.WorkerName)
                .Replace("{{model}}", request.request.model)
                .Replace("{{vehicle_id}}", request.request.vehicle_id.ToString())
                .Replace("{{date}}", request.request.date.ToString())
                .Replace("{{assignee_first_name}}", request.request.assignee_first_name)
                .Replace("{{assignee_last_name}}", request.request.assignee_last_name)
                .Replace("{{assignee_id}}", request.request.assignee_id.ToString());

            bodyBuilder.HtmlBody = emailBody;
            this.To.Add(new MailboxAddress(request.request.WorkerName, request.request.email));
            this.Subject = "Assemble Job Assignment - " + request.request.date;
            var multipart = new Multipart("mixed");

            var textPart = new TextPart(MimeKit.Text.TextFormat.Html) { Text = bodyBuilder.HtmlBody };
            multipart.Add(textPart);

            if (attachmentStream != null && !string.IsNullOrEmpty(attachmentFileName))
            {
                var attachment = new MimePart()
                {
                    Content = new MimeContent(attachmentStream),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = attachmentFileName
                };
                multipart.Add(attachment);
            }

            this.Body = multipart;
        }

    }
}
