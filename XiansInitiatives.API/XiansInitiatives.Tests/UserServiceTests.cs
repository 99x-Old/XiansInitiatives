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
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _service;

        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

        public UserServiceTests()
        {
            _service = new UserService(_mockMapper.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnInitiative_WhenUserExists()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var mockUser = new ApplicationUser
            {
                Id = userId.ToString(),
                FirstName = "Xian",
                LastName = "User",
                Designation = "Software Engineer"
            };

            var mockDto = new UserForProfileDto
            {
                Id = userId.ToString(),
                FirstName = "Xian",
                LastName = "User",
                Designation = "Software Engineer"
            };
            _mockMapper.Setup(x => x.Map<UserForProfileDto>(It.IsAny<ApplicationUser>())).Returns(mockDto);
            _mockUnitOfWork.Setup(x => x.User.GetUser(userId.ToString())).ReturnsAsync(mockUser);

            //Act

            var result = await _service.GetUserAsync(userId.ToString());

            //Assert
            Assert.Equal(userId.ToString(), result.Id.ToString());
        }

        [Fact]
        public async Task GetInitiative_ShouldReturnNull_WhenInitiativeNotExists()
        {
            //Arrange

            _mockUnitOfWork.Setup(x => x.User.GetUser(It.IsAny<Guid>().ToString())).ReturnsAsync(() => null);

            //Act

            var result = await _service.GetUserAsync(Guid.NewGuid().ToString());

            //Assert
            Assert.Null(result);
        }
    }
}