using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.EntityFramework.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        internal Iconfiguration _configuration;
        public AppDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("workstation id=AMWebApp.mssql.somee.com;packet size=4096;user id=AMDev_SQLLogin_1;pwd=u992pjac5p;data source=AMWebApp.mssql.somee.com;persist security info=False;initial catalog=AMWebApp;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}