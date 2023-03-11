using apiSocialWeb.Domain.DTOs;

namespace apiSocialWeb.Domain.Models.UserAggregate
{
    public interface IUserRepository
    {
        void Add(Client user);

        List<UserDTO> Get(int pageNumber, int pageQuantity);

        Client? Get(int id);

        Task<bool> Put(int id, Client user);

        Task Delete(int id);
    }
}
