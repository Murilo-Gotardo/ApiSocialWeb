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

        //readonly string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        //readonly string connectionString = "Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=containers-us-west-34.railway.app;Port=7093;Database=railway;Username=postgres;Password=keQQ1Yh79Xwgq92yDmeu;");

        //"Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;"
        //"Server=containers-us-west-142.railway.app;Port=6449;Database=railway;Username=postgres;Password=wY3e8oaev6DbYeDyTYDg;"
    }
}
