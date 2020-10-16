using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.Shared.Models;
using XiansInitiatives.Service;
using Xunit;
using XiansInitiatives.ServiceContract;
using Templates;

namespace XiansInitiatives.Tests
{
    public class InitiativeServiceTests
    {
        private readonly InitiativeService _service;

        private readonly Mock<IUnitOfWork> _initiativeMockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IAzureBusService> _mockAzureBusService = new Mock<IAzureBusService>();
        private readonly Mock<IRazorViewToStringRenderer> _mockRazorViewToStringRenderer = new Mock<IRazorViewToStringRenderer>();

        public InitiativeServiceTests()
        {
            _service = new InitiativeService(_mapperMock.Object, _initiativeMockUnitOfWork.Object, _mockAzureBusService.Object, _mockRazorViewToStringRenderer.Object);
        }

        [Fact]
        public async Task GetInitiative_ShouldReturnInitiative_WhenInitiativeExists()
        {
            //Arrange
            var initiativeId = Guid.NewGuid();
            var initiativeObj = new Initiative
            {
                Id = initiativeId,
                Name = "Music",
                CreatedAt = DateTime.Today
            };
            _initiativeMockUnitOfWork.Setup(x => x.Initiative.GetInitiative(initiativeId.ToString())).ReturnsAsync(initiativeObj);

            //Act

            var initiative = await _service.GetInitiative(initiativeId.ToString());

            //Assert
            Assert.Equal(initiativeId.ToString(), initiative.Id.ToString());
        }

        [Fact]
        public async Task GetInitiative_ShouldReturnNull_WhenInitiativeNotExists()
        {
            //Arrange

            _initiativeMockUnitOfWork.Setup(x => x.Initiative.GetInitiative(It.IsAny<Guid>().ToString())).ReturnsAsync(() => null);

            //Act

            var initiative = await _service.GetInitiative(Guid.NewGuid().ToString());

            //Assert
            Assert.Null(initiative);
        }

        [Fact]
        public async Task GetReviewCycles_ShouldReturnGetReviewCyclesList_WhenGetReviewCyclesExists()
        {
            //Arrange
            var initiativeId = Guid.NewGuid();
            var reviewCycle = new ReviewCycle
            {
                Id = Guid.NewGuid(),
                InitiativeId = initiativeId,
                CreatedAt = DateTime.Today,
                EvaluatorId = Guid.NewGuid().ToString(),
            };

            var reviewCycleList = new List<ReviewCycle>();
            reviewCycleList.Add(reviewCycle);

            _initiativeMockUnitOfWork.Setup(x => x.ReviewCycle.GetReviewCycles(initiativeId.ToString())).ReturnsAsync(reviewCycleList);

            //Act

            var reviewCycles = await _service.GetReviewCycles(initiativeId.ToString());

            //Assert
            Assert.IsType<List<ReviewCycle>>(reviewCycles);
        }

        //[Fact]
        //public async Task GetInitiativeProfile_ShouldReturnInitiativeForReturnDto_WhenInitiativeIsExists()
        //{
        //    //Arrange
        //    var initiativeId = Guid.NewGuid();
        //    var leadId = Guid.NewGuid();
        //    var coLeadId = Guid.NewGuid();
        //    var mentorId = Guid.NewGuid();
        //    var initiativeObj = new Initiative
        //    {
        //        Id = initiativeId,
        //        LeadId = leadId.ToString(),
        //        CoLeadId = coLeadId.ToString(),
        //        MentorId = mentorId.ToString(),
        //        Name = "Music",
        //        CreatedAt = DateTime.Today
        //    };

        //    var leadObj = new ApplicationUser
        //    {
        //        Id = leadId.ToString(),
        //        FirstName = "Lead",
        //        LastName = "User",
        //        Designation = "Software Engineer"
        //    };
        //    var leadDto = new UserForProfileDto
        //    {
        //        Id = leadId.ToString(),
        //        FirstName = "Lead",
        //        LastName = "User",
        //        Designation = "Software Engineer"
        //    };

        //    var coLeadObj = new ApplicationUser
        //    {
        //        Id = coLeadId.ToString(),
        //        FirstName = "CoLead",
        //        LastName = "User",
        //        Designation = "Software Engineer"
        //    };

        //    var mentorObj = new ApplicationUser
        //    {
        //        Id = mentorId.ToString(),
        //        FirstName = "Mentor",
        //        LastName = "User",
        //        Designation = "Software Engineer"
        //    };
        //    _initiativeMockUnitOfWork.Setup(x => x.User.GetUser(leadId.ToString())).ReturnsAsync(leadObj);
        //    _initiativeMockUnitOfWork.Setup(x => x.User.GetUser(coLeadId.ToString())).ReturnsAsync(coLeadObj);
        //    _initiativeMockUnitOfWork.Setup(x => x.User.GetUser(mentorId.ToString())).ReturnsAsync(mentorObj);

        //    _mapperMock.Setup(x => x.Map<UserForProfileDto>(leadObj)).ReturnsAsync(leadDto);
        //    _initiativeMockUnitOfWork.Setup(x => x.Initiative.GetInitiative(initiativeId.ToString())).ReturnsAsync(initiativeObj);

        //    //Act

        //    var initiative = await _service.GetInitiativeProfile(initiativeId.ToString());

        //    //Assert
        //    Assert.Equal(initiativeId.ToString(), initiative.Id.ToString());
        //}
    }
}