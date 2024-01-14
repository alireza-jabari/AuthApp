using Microsoft.EntityFrameworkCore;
namespace AuthApp;

public class AppDbContext:DbContext
{
    public DbSet<User> Users {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("");
    }
}