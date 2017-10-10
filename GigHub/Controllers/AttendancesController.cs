using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Linq;
using GigHub.DTOs;

namespace GigHub.Controllers
{

    [Authorize]
    public class AttendancesController : ApiController
    {

        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDTO dto)
        {
            var userId = User.Identity.GetUserId();
            
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId ))
            {
                return BadRequest("The attendance allready exists.");
            }

            var attendance = new Attendance
            {
                GigId      = dto.GigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
