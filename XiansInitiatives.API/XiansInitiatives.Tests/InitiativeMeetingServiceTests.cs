using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.Service;
using XiansInitiatives.Shared.Dtos;
using Xunit;

namespace XiansInitiatives.Tests
{
    public class InitiativeMeetingServiceTests
    {
        private readonly InitiativeMeetingService _service;

        private readonly Mock<IUnitOfWork> _initiativeMockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public InitiativeMeetingServiceTests()
        {
            _service = new InitiativeMeetingService(_mapperMock.Object, _initiativeMockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetInitiativeMeetings_ShouldReturnInitiativeMeetingDtoList_WhenMettingsExists()
        {
            //Arrange
            var initiativeId = Guid.NewGuid().ToString();
            var InitiativeMeeting = new InitiativeMeetingDto
            {
                Id = Guid.NewGuid().ToString(),
                InitiativeId = initiativeId,
                CreatedAt = DateTime.Today,
                ScheduledAt = DateTime.Today,
                MeetingNotes = "Meeting note",
                OrganizerName = "Organizer",
                OrganizerId = Guid.NewGuid().ToString(),
            };

            var InitiativeMeetingList = new List<InitiativeMeetingDto>();
            InitiativeMeetingList.Add(InitiativeMeeting);

            _initiativeMockUnitOfWork.Setup(x => x.InitiativeMeeting.GetInitiativeMeetings(initiativeId)).ReturnsAsync(InitiativeMeetingList);

            //Act

            var initiativeMeetings = await _service.GetInitiativeMeetings(initiativeId);

            //Assert
            Assert.IsType<List<InitiativeMeetingDto>>(initiativeMeetings);
        }

        [Fact]
        public async Task GetInitiativeMeetings__ShouldReturnNull_WhenInitiativeMeetingsNotExists()
        {
            //Arrange

            _initiativeMockUnitOfWork.Setup(x => x.InitiativeMeeting.GetInitiativeMeetings(It.IsAny<Guid>().ToString())).ReturnsAsync(() => null);

            //Act

            var initiativeMeerings = await _service.GetInitiativeMeetings(Guid.NewGuid().ToString());

            //Assert
            Assert.Null(initiativeMeerings);
        }
    }
}