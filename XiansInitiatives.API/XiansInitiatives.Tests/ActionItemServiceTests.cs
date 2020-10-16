using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.Service;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Dtos;
using Xunit;

namespace XiansInitiatives.Tests
{
    public class ActionItemServiceTests
    {
        private readonly ActionItemService _service;

        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<IAzureBusService> _mockAzureBusService = new Mock<IAzureBusService>();

        public ActionItemServiceTests()
        {
            _service = new ActionItemService(_mockMapper.Object, _mockUnitOfWork.Object, _mockAzureBusService.Object);
        }

        [Fact]
        public async Task GetActionItems_ShouldReturnActionItemForReturnDtoList_WhenActionItemsExists()
        {
            //Arrange
            var initiativeId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var mockActionItem = new ActionItemForReturnDto
            {
                Id = itemId.ToString(),
                Item = "Test Item",
                Description = "Test Description",
                Progress = 50,
                Deadline = DateTime.Now.ToString(),
                Start = DateTime.Now.ToString(),
            };

            var mockActionItemsList = new List<ActionItemForReturnDto>();
            mockActionItemsList.Add(mockActionItem);

            _mockUnitOfWork.Setup(x => x.ActionItem.GetActionItems(initiativeId.ToString())).ReturnsAsync(mockActionItemsList);

            var mockAssigneesList = new List<ItemAssigneeDto>();
            mockAssigneesList.Add(new ItemAssigneeDto { AssigneeId = Guid.NewGuid().ToString(), FullName = "Test user_1", ItemId = itemId.ToString() });
            mockAssigneesList.Add(new ItemAssigneeDto { AssigneeId = Guid.NewGuid().ToString(), FullName = "Test user_2", ItemId = itemId.ToString() });
            mockAssigneesList.Add(new ItemAssigneeDto { AssigneeId = Guid.NewGuid().ToString(), FullName = "Test user_3", ItemId = itemId.ToString() });

            _mockUnitOfWork.Setup(x => x.ItemAssignee.GetAssignees(itemId.ToString())).ReturnsAsync(mockAssigneesList);

            var mockCommentsList = new List<CommentItemDto>();
            mockCommentsList.Add(new CommentItemDto { ItemId = itemId.ToString(), CommenterId = Guid.NewGuid().ToString(), Comment = "Test comment_1" });
            mockCommentsList.Add(new CommentItemDto { ItemId = itemId.ToString(), CommenterId = Guid.NewGuid().ToString(), Comment = "Test comment_2" });
            mockCommentsList.Add(new CommentItemDto { ItemId = itemId.ToString(), CommenterId = Guid.NewGuid().ToString(), Comment = "Test comment_3" });

            _mockUnitOfWork.Setup(x => x.CommentItem.GetComments(itemId.ToString())).ReturnsAsync(mockCommentsList);

            //Act

            var result = await _service.GetActionItems(initiativeId.ToString());

            //Assert
            Assert.IsType<List<ActionItemForReturnDto>>(result);
        }

        [Fact]
        public async Task GetActionItems_ShouldReturnNull_WhenInitiativeNotExists()
        {
            //Arrange

            _mockUnitOfWork.Setup(x => x.ActionItem.GetActionItems(It.IsAny<Guid>().ToString())).ReturnsAsync(() => null);

            //Act

            var result = await _service.GetActionItems(Guid.NewGuid().ToString());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetContribution_ShouldReturnActionItemForReturnDtoList_WhenMemberExists()
        {
            //Arrange
            var memberId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var mockActionItem = new ActionItemForReturnDto
            {
                Id = itemId.ToString(),
                Item = "Test Item",
                Description = "Test Description",
                Progress = 50,
                Deadline = DateTime.Now.ToString(),
                Start = DateTime.Now.ToString(),
            };

            var mockActionItemsList = new List<ActionItemForReturnDto>();
            mockActionItemsList.Add(mockActionItem);

            _mockUnitOfWork.Setup(x => x.ActionItem.GetContribution(memberId.ToString())).ReturnsAsync(mockActionItemsList);

            var mockAssigneesList = new List<ItemAssigneeDto>();
            mockAssigneesList.Add(new ItemAssigneeDto { AssigneeId = memberId.ToString(), FullName = "Test user_1", ItemId = itemId.ToString() });
            mockAssigneesList.Add(new ItemAssigneeDto { AssigneeId = Guid.NewGuid().ToString(), FullName = "Test user_2", ItemId = itemId.ToString() });
            mockAssigneesList.Add(new ItemAssigneeDto { AssigneeId = Guid.NewGuid().ToString(), FullName = "Test user_3", ItemId = itemId.ToString() });

            _mockUnitOfWork.Setup(x => x.ItemAssignee.GetAssignees(itemId.ToString())).ReturnsAsync(mockAssigneesList);

            var mockCommentsList = new List<CommentItemDto>();
            mockCommentsList.Add(new CommentItemDto { ItemId = itemId.ToString(), CommenterId = Guid.NewGuid().ToString(), Comment = "Test comment_1" });
            mockCommentsList.Add(new CommentItemDto { ItemId = itemId.ToString(), CommenterId = Guid.NewGuid().ToString(), Comment = "Test comment_2" });
            mockCommentsList.Add(new CommentItemDto { ItemId = itemId.ToString(), CommenterId = Guid.NewGuid().ToString(), Comment = "Test comment_3" });

            _mockUnitOfWork.Setup(x => x.CommentItem.GetComments(itemId.ToString())).ReturnsAsync(mockCommentsList);

            //Act

            var result = await _service.GetContribution(memberId.ToString());

            //Assert
            Assert.IsType<List<ActionItemForReturnDto>>(result);
        }

        [Fact]
        public async Task GetContribution_ShouldReturnNull_WhenMemberNotExists()
        {
            //Arrange

            _mockUnitOfWork.Setup(x => x.ActionItem.GetContribution(It.IsAny<Guid>().ToString())).ReturnsAsync(() => null);

            //Act

            var result = await _service.GetContribution(Guid.NewGuid().ToString());

            //Assert
            Assert.Null(result);
        }
    }
}