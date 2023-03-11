using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace apiSocialWeb.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Posts> Posts { get; set; } = default!;

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=postgresql://postgres:Q3JY8PwEg6zqxMUque5M@containers-us-west-17.railway.app:6345/railway;" +
                "Port=6545;Database=railway;" +
                "User Id=postgres;" +
                "Password=Q3JY8PwEg6zqxMUque5M;"
                ); 
    }
}
