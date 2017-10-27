using AutoMapper;
using GigHub.DTOs;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            // Not the mapping pattern below is important. 
            // A Notification has a Gig that it needs to map, So the Gig mapping needs to be before the Notification
            // A Gig has a Application user that it need to map, So the mapping for Application user must come first.
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();

            return notifications.Select(Mapper.Map<Notification,NotificationDto>);
        }

    }
}
