using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.NotificationAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace apiSocialWeb.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Posts> Posts { get; set; } = default!;
        public DbSet<Comment> Comment { get; set; } = default!;
        public DbSet<Like> Like { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;

        private string? ConnectionString { get; set; }

        //private readonly string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONECTION_STRING_DEVELOPMENT").Replace(" ", "");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"{ConnectionString.Replace(" ", "")}");

        //"Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;"

        public ConnectionContext() { }

        public ConnectionContext(string? connectionString) 
        {
            ConnectionString = connectionString;
        }
    }
}
