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
    public class InitiativeMemberServiceTests
    {
        private readonly InitiativeMemberService _service;

        private readonly Mock<IUnitOfWork> _initiativeMockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public InitiativeMemberServiceTests()
        {
            _service = new InitiativeMemberService(_mapperMock.Object, _initiativeMockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetInitiativeMember_ShouldReturnInitiativeMember_WhenInitiativeMemberExists()
        {
            //Arrange
            var initiativeId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var initiativeMemberDto = new InitiativeMemberDto
            {
                InitiativeId = initiativeId.ToString(),
                MemberId = memberId.ToString(),
                Rating = 5
            };

            var initiativeMember = new InitiativeMember
            {
                InitiativeId = initiativeId,
                MemberId = memberId.ToString()
            };
            _initiativeMockUnitOfWork.Setup(x => x.InitiativeMember.GetInitiativeMember(initiativeMemberDto.MemberId, initiativeMemberDto.InitiativeId)).ReturnsAsync(initiativeMember);

            //Act

            var initiativeMemberReturn = await _service.GetInitiativeMember(initiativeMemberDto);

            //Assert

            Assert.Equal(initiativeMemberDto.InitiativeId, initiativeMemberReturn.InitiativeId.ToString());
            Assert.IsType<InitiativeMember>(initiativeMemberReturn);
        }
    }
}