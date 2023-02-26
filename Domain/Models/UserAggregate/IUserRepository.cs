using apiSocialWeb.Domain.DTOs;

namespace apiSocialWeb.Domain.Models.UserAggregate
{
    public interface IUserRepository
    {
        void Add(User user);

        List<UserDTO> Get(int pageNumber, int pageQuantity);

        User? Get(int id);
    }
}
