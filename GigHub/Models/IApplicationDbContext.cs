using System;
using System.Data.Entity;
namespace GigHub.Models
{
    public interface IApplicationDbContext
    {
        System.Data.Entity.DbSet<Attendance> Attendances { get; set; }
        System.Data.Entity.DbSet<Following> Followings { get; set; }
        System.Data.Entity.DbSet<Genre> Genres { get; set; }
        System.Data.Entity.DbSet<Gig> Gigs { get; set; }
        System.Data.Entity.DbSet<Notification> Notifications { get; set; }
        System.Data.Entity.DbSet<UserNotification> UserNotifications { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
    }
}
