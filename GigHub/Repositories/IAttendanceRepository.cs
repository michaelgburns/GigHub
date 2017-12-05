namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {
        GigHub.Models.Attendance GetAttendance(int GigId, string userId);
        System.Collections.Generic.IEnumerable<GigHub.Models.Attendance> GetFutureAttendances(string userId);
    }
}
