
namespace Vehicle_Assembly.DTOs.Requests
{
    public class SendEmailRequest
    {
        public string recipientEmail { get; set; }
        public string recipientName { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
