using apiSocialWeb.Domain.DTOs;

namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public interface IPostRepository
    {
        void Add(Posts post);

        List<PostDTO> Get(int pageNumber, int pageQuantity);

        Posts? Get(int id);
    }
}