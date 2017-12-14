using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Security.Claims;
using GigHub.Controllers.Api;
using GigHub.Persistence;
using Moq;
using GigHub.Tests.Controllers.Extensions;
using GigHub.Repositories;
using FluentAssertions;
using System.Web.Http.Results;
using GigHub.Models;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        public GigsControllerTests()
        {

            _mockRepository = new Mock<IGigRepository>();
            
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUow.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");

        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result =  _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGig_ShouldReturnUnauthorised()
        {
            var gig = new Gig { ArtistId = _userId + "--"};
            
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
