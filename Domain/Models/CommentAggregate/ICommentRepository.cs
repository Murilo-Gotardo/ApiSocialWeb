
namespace apiSocialWeb.Domain.Models.CommentAggregate
{
    public interface ICommentRepository
    {

        Task Add(Comment comment);

        Task<Comment?> Get(int id);

        Task<List<Comment>> Get(int id, int pageNumber, int pageQuantity);

        Task<int> GetRows(int id);

        Task<bool> Put(int id, Comment comment);

        Task Delete(int id);
    }
}
