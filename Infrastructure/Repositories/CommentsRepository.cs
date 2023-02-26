using apiSocialWeb.Domain.DTOs;
using apiSocialWeb.Domain.Models.CommentsAggregate;

namespace apiSocialWeb.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {

        private readonly ConnectionContext _comment = new ConnectionContext();
        public void Add(Comments comment)
        {
            _comment.Comments.Add(comment);
            _comment.SaveChanges();
        }

        public List<CommentsDTO> Get(int pageNumber, int pageQuantity)
        {
            return _comment.Comments.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Select(b =>
                new CommentsDTO()
                {
                    Id = b.CommentId,
                    Name = b.Name
                }).ToList();
        }

        public Comments? Get(int id)
        {
            return _comment.Comments.Find(id);
        }
    }
}