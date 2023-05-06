namespace apiSocialWeb.Domain.Models.PostsAggregate
{
    public interface IPostRepository
    {
        Task Add(Posts post);

        Task<Posts> Get(int id);

        Task<List<Posts>> Get(int pageNumber, int pageQuantity);

        Task<List<Posts>> Get(int userId, int pageNumber, int pageQuantity);

        Task<bool> Put(int id, Posts post);

        Task Delete(int id);
    }
}