namespace GigHub.Repositories
{
    public interface IFollowingRepository
    {
        GigHub.Models.Following GetFollowing(string userId, string artistId);
    }
}
