namespace apiSocialWeb.Domain.Models.NotificationAggregate
{
    public interface INotificationRepository
    {

        void Add(Notification notify);

        List<Notification>? Get(int id);

        Task Delete(int id);

    }
}
