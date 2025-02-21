namespace Vehicle_Assembly.DTOs.Requests
{
    public class AuthenticateRequest
    {
        public required string email { get; set; }
        public required string password { get; set; }

    }
}
