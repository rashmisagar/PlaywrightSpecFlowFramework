using Microsoft.EntityFrameworkCore;
using PlaywrightSpecFlowFramework.Utils;

namespace PlaywrightSpecFlowFramework.Data;

public class DatabaseContext : DbContext
{
    public DbSet<SearchTerm> SearchTerm { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }
}