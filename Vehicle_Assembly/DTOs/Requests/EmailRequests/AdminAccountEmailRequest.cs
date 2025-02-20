namespace Vehicle_Assembly.DTOs.Requests.EmailRequests
{
    public class AdminAccountEmailRequest
    {
        public int NIC { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string? password { get; set; }
    }
}
