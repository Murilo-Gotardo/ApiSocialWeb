namespace apiSocialWeb.Domain.Models.UserAggregate
{
    public interface IUserRepository
    {
        Task Add(User user);

        Task<User?> Get(int id);

        Task<List<User>> Get(int pageNumber, int pageQuantity);

        Task<bool> Put(int id, User user);

        Task Delete(int id);
    }
}
