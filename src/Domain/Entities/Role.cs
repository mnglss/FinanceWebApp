namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        public List<UserRole> UserRoles { get; set; } = [];
    }
}