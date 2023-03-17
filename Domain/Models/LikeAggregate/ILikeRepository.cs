namespace apiSocialWeb.Domain.Models.LikeAggregate
{
    public interface ILikeRepository
    {

        void Add(Like like);

        int Get(int id);

        Task Delete(int id);
    }
}
