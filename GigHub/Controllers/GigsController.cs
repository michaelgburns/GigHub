using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;


namespace GigHub.Controllers
{
    /// <summary>
    /// Gigs controller
    /// </summary>
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly GenresRepository _genreReposiory;

        public GigsController()
        {
            _context              = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository        = new GigRepository(_context);
            _followingRepository  = new FollowingRepository(_context);
            _genreReposiory       = new GenresRepository(_context);
        }

        [Authorize]
        public ActionResult Mine()
        {            
            var gigs = _gigRepository.GetUpcommingGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();  

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
                ShowActions  = User.Identity.IsAuthenticated,
                Heading      = "Gigs I'm Attending",
                Attendances  = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }       

        
        [Authorize]
        public ActionResult Create()
        {
            var viewModel     = new GigFormViewModel();            
            viewModel.Genres  = _genreReposiory.GetGenres();
            viewModel.Heading = "Add a Gig.";
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _genreReposiory.GetGenres();
                viewModel.Heading = "Add a Gig.";
                return View("GigForm", viewModel);
            }      

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId  =  viewModel.Genre,
                Venue    = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {                     
            var gig = _gigRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();
            

            var viewModel = new GigFormViewModel
            {
                Id      = gig.Id,
                Heading = "Edit a Gig",
                Genres  = _genreReposiory.GetGenres(),
                Date    = gig.DateTime.ToString("d MMM yyyy"),
                Time    = gig.DateTime.ToString("HH:mm"),
                Genre   = gig.GenreId,
                Venue   = gig.Venue
            };
            
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres  = _genreReposiory.GetGenres();
                viewModel.Heading = "Add a Gig.";
                return View("GigForm", viewModel);
            }
            
            var gig      = _gigRepository.GetGigWithAttendees(viewModel.Id);
            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);   
           
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        public ActionResult Details(int id)
        {
            //var gig = _context.Gigs.Include(g => g.Artist)
            //                       .Include(g => g.Genre)
            //                       .SingleOrDefault(g => g.Id == id);

            var gig = _gigRepository.GetGigWithArtistWithGenre(id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                
                
                viewModel.IsAttending = _attendanceRepository.GetAttendance(gig.Id, userId) != null;                
                viewModel.IsFollowing = _followingRepository.GetFollowing(userId, gig.ArtistId) != null;
            }

            return View("Details", viewModel);

        }

        #region Private helper methods


        #endregion
    }  

}