using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configuration
{
    public class LogAuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable("LogAudits", "Log");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Date)
                .IsRequired();
        }
    }
}
