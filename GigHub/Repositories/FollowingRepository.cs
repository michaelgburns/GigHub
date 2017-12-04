using GigHub.Models;
using System.Linq;

namespace GigHub.Repositories
{ 
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, string artistId)
        {            
            return _context.Followings.SingleOrDefault(f => f.FolloweeId == artistId && f.FolloweeId == userId);
        }
    }
}