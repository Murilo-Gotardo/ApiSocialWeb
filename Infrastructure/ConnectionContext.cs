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
                "Server=localhost;" +
                "Port=5432;Database=socialWeb;" +
                "User Id=postgres;" +
                "Password=1234;"
                ); 
    }
}
