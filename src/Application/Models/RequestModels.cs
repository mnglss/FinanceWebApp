namespace Application.Models
{
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
    public record UserUpdateRequest(int Id, string FirstName, string LastName, string Email);
    public record AssignRoleRequest(int userId, int roleId);
    public record RemoveRoleRequest(int userId, int roleId);

    public record MovementRequest(int id, int year, int month, double amount, string date, string description, string category, int userId);
}
