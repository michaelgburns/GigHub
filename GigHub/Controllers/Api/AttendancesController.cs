using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
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

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendances.SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);

            if (attendance == null) return NotFound();

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
