using apiSocialWeb.Domain.Models.CommentAggregate;
using Microsoft.EntityFrameworkCore;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ConnectionContext _comment = new ConnectionContext();
        public void Add(Comment comment)
        {
            _comment.Comment.Add(comment);
            _comment.SaveChanges();
        }

        public List<Comment> GetPostComments(int id, int pageNumber, int pageQuantity)
        {
            return _comment.Comment
            .Where(c => c.PostId == id)
            .OrderByDescending(c => c.CreatedDate)
            .Skip((pageNumber - 1) * pageQuantity)
            .Take(pageQuantity)
            .ToList();
        }

        public int GetRows(int id)
        {
            List<Comment> comment = _comment.Comment
            .Where(p => p.PostId == id)
            .ToList();

            int count = comment.Count; // Obtem o número de linhas

            return count;
        }


        public Comment? Get(int id)
        {
            return _comment.Comment.Find(id);
        }

        public async Task<bool> Put(int id, Comment comment)
        {
            var existingComment = await _comment.Comment.FirstOrDefaultAsync(c => c.CommentId == id);

            if (existingComment != null)
            {
                // Update the properties of the existing comment
                existingComment.Icomment = comment.Icomment;


                _comment.Comment.Update(existingComment);
                await _comment.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task Delete(int commentId)
        {

            var post = await _comment.Comment.FirstOrDefaultAsync(c => c.CommentId == commentId) ?? throw new Exception($"Comment with ID {commentId} not found.");
            _comment.Comment.Remove(post);

            await _comment.SaveChangesAsync();

        }
    }
}
