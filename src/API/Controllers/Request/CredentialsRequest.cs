namespace API.Controllers.Request
{
    public class CredentialsRequest
    {
        public CredDto Credentials { get; set; } = null!;

    }
    public class CredDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }


}
