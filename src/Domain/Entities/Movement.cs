namespace Domain.Entities
{
    public class Movement
    {
        public int Id { get; set; }
        public required int Year { get; init; }
        public required int Month { get; init; } 

        public required DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public string Category { get; set; } = string.Empty; // e.g., "Food", "Transport", etc.
        public string Description { get; set; } = string.Empty; // e.g., "Salary", "Bonus", etc.
        public required double Amount { get; set; }

        public int UserId { get; set; } // Foreign key to UserEntity
        public User? User { get; set; } // Navigation property

        public Movement(int year, int month, double amount, DateOnly date, string description, string category)
        {
            Id = 0; 
            Year = year;
            Month = month;
            Amount = amount;
            Date = date;
            Description = description;
            Category = category;
        }
    }
}