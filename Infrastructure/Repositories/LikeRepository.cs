using apiSocialWeb.Domain.Models.LikeAggregate;
using Microsoft.EntityFrameworkCore;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ConnectionContext _like = new ConnectionContext();
        public void Add(Like like)
        {
            _like.Like.Add(like);
            _like.SaveChanges();
        }


        public int GetRows(int id)
        {
            List<Like> likes = _like.Like
            .Where(p => p.PostId == id)
            .ToList();

            int count = likes.Count; // Obtem o número de linhas

            return count;
        }

        public async Task Delete(int likeId)
        {

            var like = await _like.Like.FirstOrDefaultAsync(l => l.LikeId == likeId) ?? throw new Exception($"Comment with ID {likeId} not found.");
            _like.Like.Remove(like);

            await _like.SaveChangesAsync();

        }
    }
}
