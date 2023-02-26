using apiSocialWeb.Domain.DTOs;

namespace apiSocialWeb.Domain.Models.CommentsAggregate
{
    public interface ICommentsRepository
    {
        void Add(Comments comment);

        List<CommentsDTO> Get(int pageNumber, int pageQuantity);

        Comments? Get(int id);
    }
}
