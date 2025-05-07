namespace Application.Models.Request
{
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
}