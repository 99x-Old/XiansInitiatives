using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Service
{
    public class ActionItemService : IActionItemService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureBusService _azureBusService;

        public ActionItemService(IMapper mapper, IUnitOfWork unitOfWork, IAzureBusService azureBusService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _azureBusService = azureBusService;
        }

        public async Task<bool> AddComment(CommentItemDto commentItemDto)
        {
            var commentItem = _mapper.Map<CommentItem>(commentItemDto);
            commentItem.CreatedAt = DateTime.Now;
            var result = await _unitOfWork.CommentItem.AddComment(commentItem);

            return result;
        }

        public async Task<bool> CreateActionItem(ActionItemForCreateDto actionItemForCreateDto)
        {
            var actionItem = _mapper.Map<ActionItem>(actionItemForCreateDto);
            actionItem.CreatedAt = DateTime.Now;

            var savedActionItem = await _unitOfWork.ActionItem.CreateActionItem(actionItem);

            if (savedActionItem != null)
            {
                foreach (var itemAssignee in actionItemForCreateDto.Assignees)
                {
                    itemAssignee.CreatedAt = DateTime.Now;
                    itemAssignee.ItemId = savedActionItem.Id;
                    var assigneeResult = await _unitOfWork.ItemAssignee.InsertAssignees(itemAssignee);
                    await _azureBusService.SendEmailAsync(itemAssignee.AssigneeId, "actionAssign");
                    if (!assigneeResult)
                    {
                        return assigneeResult;
                    }
                }
            }
            return true;
        }

        public async Task<List<ActionItemForReturnDto>> GetActionItems(string initiativeId)
        {
            var actionItems = await _unitOfWork.ActionItem.GetActionItems(initiativeId);

            if (actionItems != null)
            {
                foreach (var item in actionItems)
                {
                    var assignees = await _unitOfWork.ItemAssignee.GetAssignees(item.Id);
                    if (assignees != null)
                    {
                        item.Assignees = assignees;
                    }
                    else
                    {
                        return null;
                    }
                    var comments = await _unitOfWork.CommentItem.GetComments(item.Id);
                    if (comments != null)
                    {
                        item.CommentItem = comments;
                    }
                    else
                    {
                        return null;
                    }
                }
                return actionItems;
            }
            return null;
        }

        public async Task<List<ActionItemForReturnDto>> GetContribution(string memberId)
        {
            var actionItems = await _unitOfWork.ActionItem.GetContribution(memberId);

            if (actionItems != null)
            {
                foreach (var item in actionItems)
                {
                    var assignees = await _unitOfWork.ItemAssignee.GetAssignees(item.Id);
                    if (assignees != null)
                    {
                        item.Assignees = assignees;
                    }
                    else
                    {
                        return null;
                    }
                    var comments = await _unitOfWork.CommentItem.GetComments(item.Id);
                    if (comments != null)
                    {
                        item.CommentItem = comments;
                    }
                    else
                    {
                        return null;
                    }
                }
                return actionItems;
            }
            return null;
        }

        public async Task<bool> UpdateProgress(ActionItemForUpdate actionItemForUpdate)
        {
            var result = await _unitOfWork.ActionItem.UpdateProgress(actionItemForUpdate);

            if (result == true && actionItemForUpdate.Assignees.Any())
            {
                foreach (var assignee in actionItemForUpdate.Assignees)
                {
                    await _azureBusService.SendEmailAsync(assignee.AssigneeId, "stateChange");
                }
            }
            return result;
        }
    }
}