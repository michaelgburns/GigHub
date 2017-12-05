using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository : GigHub.Repositories.IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            var attendances = _context.Attendances.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList();
            return attendances;
        }

        public Attendance GetAttendance(int GigId, string userId)
        {            
            return _context.Attendances.SingleOrDefault(a => a.GigId == GigId && a.AttendeeId == userId);            
        }
    }
}