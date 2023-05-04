namespace apiSocialWeb.Domain.Models.LikeAggregate
{
    public interface ILikeRepository
    {

        void Add(PostLike like);

        int GetRows(int id);

        Task Delete(int id);
    }
}
