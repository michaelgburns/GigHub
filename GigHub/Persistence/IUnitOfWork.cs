using System;
namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        GigHub.Repositories.IAttendanceRepository Attendances { get; }
        void Complete();
        GigHub.Repositories.IFollowingRepository Followings { get; }
        GigHub.Repositories.IGenresRepository Genres { get; }
        GigHub.Repositories.IGigRepository Gigs { get; }
    }
}
