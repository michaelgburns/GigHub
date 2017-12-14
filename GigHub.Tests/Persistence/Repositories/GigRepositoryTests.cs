using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Repositories;
using Moq;
using GigHub.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using GigHub.Tests.Extensions;
using FluentAssertions;


namespace GigHub.Tests.Persistence.Repositories
{

    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialise()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);

            _repository     = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcommingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(10), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcommingGigsByArtist("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(10), ArtistId = "1" };            

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcommingGigsByArtist(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(10), ArtistId = "1" };

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcommingGigsByArtist(gig.ArtistId);

            gigs.Should().HaveCount(1);
            gigs.Should().Contain(gig);
            gigs.ToArray()[0].Should().BeSameAs(gig);
        }

    }   
   
}
