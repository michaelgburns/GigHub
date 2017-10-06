using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Linq;

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
        public IHttpActionResult Attend([FromBody] int gigId)
        {
            var userId = User.Identity.GetUserId();
            
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId))
            {
                return BadRequest("The attendance allready exists.");
            }

            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
