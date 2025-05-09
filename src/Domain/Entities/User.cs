namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }        
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly UpdatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public List<Movement>? Movements { get; set; }
        public List<UserRole>? UserRoles { get; set; }


    }
}