using apiSocialWeb.Domain.Models.CommentAggregate;
using apiSocialWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ConnectionContext _comment = new();

        public async Task Add(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "O comentário não pode ser vazio");

            try
            {
                await _comment.Comment.AddAsync(comment);
                await _comment.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                throw new DbUpdateException("Erro ao adicionar o comentário", ex);
            }
        }

        public async Task<Comment?> Get(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do comentário não pode ser menor ou igual a zero");

            try
            {
                return await _comment.Comment.FindAsync(id) ?? throw new ResourceNotFoundException(id);
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"A pesquisa não retornou dados para o comnetário de ID {id}", ex);
            }

        }

        public async Task<List<Comment>> Get(int postId, int pageNumber, int pageQuantity)
        {
            if (postId <= 0)
                throw new ArgumentOutOfRangeException(nameof(postId), "O ID da postagem não pode ser menor ou igual a zero");

            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "O número da página não pode ser menor ou igual a zero");

            if (pageQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageQuantity), "A quantidade de itens por página não pode ser menor ou igual a zero");

            try
            {
                return await _comment.Comment
                    .Where(c => c.PostId == postId)
                    .OrderByDescending(c => c.CreatedDate)
                    .Skip((pageNumber - 1) * pageQuantity)
                    .Take(pageQuantity)
                    .ToListAsync() ?? throw new ResourceNotFoundException(postId);
            }
            catch (DbException ex)
            {
                throw new DbUpdateException($"Erro ao buscar commentários com o contexto de post, de ID {postId}", ex);
            }
        }

        public async Task<int> GetRows(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID não pode ser menor ou igual a zero");

            try
            {
                List<Comment> comment = await _comment.Comment
                .Where(p => p.PostId == id)
                .ToListAsync() ?? throw new ResourceNotFoundException(id);

                int count = comment.Count; // Obtem o número de linhas

                return count;

            }
            catch (DbException ex)
            {
                throw new DbUpdateException("Erro ao pegar a quantidade de comentários", ex);
            }
        }

        public async Task<bool> Put(int id, Comment comment)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do comentário não pode ser menor ou igual a zero");

            try
            {
                var existingComment = await _comment.Comment.FirstOrDefaultAsync(c => c.CommentId == id) ?? throw new ResourceNotFoundException(id);

                if (existingComment != null)
                {
                    existingComment.Icomment_txt = comment.Icomment_txt;

                    var entry = _comment.Comment.Update(existingComment);
                    await _comment.SaveChangesAsync();

                    var affectedRows = entry.State == EntityState.Modified ? 1 : 0;

                    if (affectedRows == 0)
                        throw new ResourceNotFoundException(id);

                    return true;
                }

                return false;
            } 
            catch (DbException ex) 
            {
                throw new DbUpdateException($"A atualização do comentário, de ID {id}, falhou", ex);
            }            
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID do comentário não pode ser menor ou igual a zero");

            try
            {
                var comment = await _comment.Comment.FirstOrDefaultAsync(c => c.CommentId == id) ?? throw new ResourceNotFoundException(id);
                _comment.Comment.RemoveRange(comment);
                var affectedRows = await _comment.SaveChangesAsync();

                if (affectedRows == 0) 
                    throw new ResourceNotFoundException(id);
            }
            catch (DbException ex) 
            {
                throw new DbUpdateException($"A exclusão do comentário, de ID {id}, falhou", ex);
            }
        }
    }
}
