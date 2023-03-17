using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.NotificationAggregate;
using Microsoft.EntityFrameworkCore;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ConnectionContext _notify = new ConnectionContext();
        public void Add(Notification notify)
        {
            _notify.Notifications.Add(notify);
            _notify.SaveChanges();
        }


        public List<Notification> Get(int id)
        {
            return _notify.Notifications
            .Where(p => p.PostId == id)
            .ToList();

        }

        public async Task Delete(int id)
        {

            var notify = await _notify.Notifications.FirstOrDefaultAsync(n => n.NotificationId == id) ?? throw new Exception($"Comment with ID {id} not found.");
            _notify.Notifications.Remove(notify);

            await _notify.SaveChangesAsync();

        }
    }
}
