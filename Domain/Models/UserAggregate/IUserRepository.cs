namespace apiSocialWeb.Domain.Models.UserAggregate
{
    public interface IUserRepository
    {
        void Add(User user);

        List<User> Get(int pageNumber, int pageQuantity);

        User? Get(int id);

        Task<bool> Put(int id, User user);

        Task Delete(int id);
    }
}
