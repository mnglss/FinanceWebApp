using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configuration
{
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.ToTable("Movements", "FinanceWebApp");
            builder.HasKey(e => e.Id);            
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Amount)
                .IsRequired();
            builder.Property(e => e.Date)
                .IsRequired()
                .HasDefaultValue(DateOnly.FromDateTime(DateTime.UtcNow));
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Year)
                .IsRequired();
            builder.Property(e => e.Month)
                .IsRequired();
            builder.HasOne(e => e.User)
                .WithMany(e => e.Movements)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: specify delete behavior if needed
            builder.HasIndex(e => new { e.Year, e.Month, e.UserId }) // Composite index for Year, Month, and UserId
                .HasDatabaseName("IX_Year_Month_UserId"); // Optional: specify index name if needed
        }
    }
}