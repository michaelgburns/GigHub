﻿using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : GigHub.Persistence.IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IGenresRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context    = context;
            Gigs        = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
            Followings  = new FollowingRepository(context);
            Genres      = new GenresRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}