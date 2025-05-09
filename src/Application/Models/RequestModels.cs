namespace Application.Models
{
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
    public record UserUpdateRequest(int Id, string FirstName, string LastName, string Email);
    public record AssignRoleRequest(int userId, int roleId);
}
