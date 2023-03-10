using apiSocialWeb.Domain.DTOs;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public interface IPostRepository
    {
        void Add(Posts post);

        List<PostDTO> Get(int pageNumber, int pageQuantity);

        Posts? Get(int id);

        Task<bool> Put(int id, Posts post);

        Task Delete(int id);
    }
}