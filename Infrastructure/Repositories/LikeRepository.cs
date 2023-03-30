using apiSocialWeb.Domain.Models.LikeAggregate;
using Microsoft.EntityFrameworkCore;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ConnectionContext _like = new ConnectionContext();
        public void Add(CommentLike like)
        {
            _like.CommentLike.Add(like);
            _like.SaveChanges();
        }


        public int GetRows(int id)
        {
            List<CommentLike> likes = _like.CommentLike
            .Where(p => p.PostId == id)
            .ToList();

            int count = likes.Count; // Obtem o número de linhas

            return count;
        }

        public async Task Delete(int likeId)
        {

            var like = await _like.CommentLike.FirstOrDefaultAsync(l => l.LikeId == likeId) ?? throw new Exception($"Comment with ID {likeId} not found.");
            _like.CommentLike.Remove(like);

            await _like.SaveChangesAsync();

        }
    }
}
