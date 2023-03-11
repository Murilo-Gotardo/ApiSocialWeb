using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using Microsoft.EntityFrameworkCore;

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
                    Name = b.Name,
                    Email = b.Email,
                    Photo = b.Photo
                }).ToList();
        }

        public User? Get(int id)
        {
            return _user.User.Find(id);
        }

        public async Task<bool> Put(int id, User user)
        {
            var existingUser = await _user.User.FirstOrDefaultAsync(u => u.UserId == id);

            if (existingUser != null)
            {
                // Update the properties of the existing post
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Photo = user.Photo;

                _user.User.Update(existingUser);
                await _user.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task Delete(int userId)
        {

            var user = await _user.User.FirstOrDefaultAsync(u => u.UserId == userId) ?? throw new Exception($"Post with ID {userId} not found.");
            _user.User.Remove(user);

            await _user.SaveChangesAsync();
        }
    }
}
