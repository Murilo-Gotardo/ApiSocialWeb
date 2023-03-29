using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.CommentAggregate;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public interface IPostRepository
    {
        void Add(Posts post);

        List<Posts> Get(int pageNumber, int pageQuantity);

        List<Posts> GetUserPost(int userId, int pageNumber, int pageQuantity);

        Posts? Get(int id);

        Task<bool> Put(int id, Posts post);

        Task Delete(int id);
    }
}