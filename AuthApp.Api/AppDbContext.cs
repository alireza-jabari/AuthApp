using Microsoft.EntityFrameworkCore;
namespace AuthApp;

public class AppDbContext:DbContext
{
    public DbSet<User> Users {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Persist Security Info=True;Initial Catalog=AuthDotnetDb;TrustServerCertificate=True");
        // optionsBuilder.UseSqlServer("Server=localhost;User Id=root;Password=12345;Persist Security Info=True;Initial Catalog=MeetDb;TrustServerCertificate=True");
    }
}