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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string path = Path.GetFullPath("appsettings.json");
            AppSettingsEncryptionHelper.DecryptAppSettings(path);

            string connectionString = AppSettingsEncryptionHelper.GetConnectionString(path);

            if (connectionString != null)
            {
                optionsBuilder.UseNpgsql(connectionString);

                AppSettingsEncryptionHelper.EncryptAppSettings(path);
            }
            else
            {
                throw new Exception("Database connection string environment variable not set.");
            }
        }

        //"Server=localhost;Port=5432;Database=railway;Username=postgres;Password=1234;"

        public ConnectionContext() { }
    }
}
