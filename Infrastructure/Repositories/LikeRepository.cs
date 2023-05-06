using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Domain.Models.LikeAggregate;
using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ConnectionContext _like = new();

        public async Task Add(Like like)
        {
            try
            {
                await _like.Like.AddAsync(like);
                await _like.SaveChangesAsync();
            }
            catch (DbException ex) 
            {
                throw new DbUpdateException("Erro ao adicionar o like", ex);
            }
        }

        public async Task<int> GetRows(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID não pode ser menor ou igual a zero");

            try
            {
                List<Like> likes = await _like.Like
                .Where(p => p.PostId == id)
                .ToListAsync() ?? throw new ResourceNotFoundException(id);

                int count = likes.Count; // Obtem o número de linhas

                return count;
            }
            catch (DbException ex)
            {
                throw new DbUpdateException("Erro ao pegar a quantidade de likes", ex);
            }
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID não pode ser menor ou igual a zero");

            try
            {
                var like = await _like.Like.FirstOrDefaultAsync(l => l.LikeId == id);
                _like.Like.RemoveRange(like);
                var affectedRows = await _like.SaveChangesAsync();

                if (affectedRows == 0)
                    throw new ResourceNotFoundException(id);

            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A exclusão do like, de ID {id}, falhou", ex);
            }
        }
    }
}
