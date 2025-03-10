using MimeKit;
using Vehicle_Assembly.DTOs.Requests.EmailRequests;

namespace Vehicle_Assembly.Utilities.EmailService.AccountCreation
{
    public class AdminAccountCreation : MimeMessage
    {
        public AdminAccountCreation(AdminAccountEmailRequest request)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();

            string emailTemplate = Path.Combine(Directory.GetCurrentDirectory(), "Views", "SendEmailView", "AdminAccountEmail.html");
            string emailBody = File.ReadAllText(emailTemplate);
            emailBody = emailBody.Replace("{{AdminName}}", request.firstname + " " + request.lastname)
                .Replace("{{AdminEmail}}", request.email)
                .Replace("{{AdminPassword}}", "" + request.password);
            bodyBuilder.HtmlBody = emailBody;

            this.To.Add(new MailboxAddress(request.firstname + " " + request.lastname, request.email));
            this.Subject = "Admin Account Created";
            this.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = bodyBuilder.HtmlBody };
        }

    }

}
