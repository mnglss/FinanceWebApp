using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if (!optionsBuilder.IsConfigured)
            // {                
            //     optionsBuilder.UseSqlServer("workstation id=AMWebApp.mssql.somee.com;packet size=4096;user id=AMDev_SQLLogin_1;pwd=u992pjac5p;data source=AMWebApp.mssql.somee.com;persist security info=False;initial catalog=AMWebApp;TrustServerCertificate=True");
            // }
        }


        public DbSet<Movement> Movements { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);           
        }
    }
}