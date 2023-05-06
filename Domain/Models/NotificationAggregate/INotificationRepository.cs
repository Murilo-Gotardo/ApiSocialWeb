namespace apiSocialWeb.Domain.Models.NotificationAggregate
{
    public interface INotificationRepository
    {
        Task Add(Notification notify);

        Task<List<Notification>> Get(int id);

        Task Delete(int id);

    }
}
