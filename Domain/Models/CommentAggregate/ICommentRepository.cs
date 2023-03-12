
namespace apiSocialWeb.Domain.Models.CommentAggregate
{
    public interface ICommentRepository
    {

        void Add(Comment comment);

        List<Comment> GetPostComments(int id, int pageNumber, int pageQuantity);

        Comment? Get(int id);

        Task<bool> Put(int id, Comment comment);

        Task Delete(int id);
    }
}
