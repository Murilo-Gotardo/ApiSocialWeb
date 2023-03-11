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
                "Server=postgresql://postgres:uH7v6fEBv7S3rqZb54hm@containers-us-west-51.railway.app:7889/railway;" +
                "Port=5432;Database=railway;" +
                "User Id=postgres;" +
                "Password=uH7v6fEBv7S3rqZb54hm;"
                ); 
    }
}
