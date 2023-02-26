using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.UserAggregate;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ConnectionContext _user = new ConnectionContext();
        public void Add(User user)
        {
            _user.User.Add(user);
            _user.SaveChanges();
        }

        public List<UserDTO> Get(int pageNumber, int pageQuantity)
        {
            return _user.User.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Select(b => 
                new UserDTO()
                {
                    Id = b.UserId,
                    Name = b.Name
                }).ToList();
        }

        public User? Get(int id)
        {
            return _user.User.Find(id);
        }
    }
}
