using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.ServiceContract
{
    public interface IActionItemService
    {
        Task<List<ActionItemForReturnDto>> GetActionItems(string initiativeId);

        Task<List<ActionItemForReturnDto>> GetContribution(string memberId);

        Task<bool> CreateActionItem(ActionItemForCreateDto actionItemForCreateDto);

        Task<bool> AddComment(CommentItemDto commentItemDto);

        Task<bool> UpdateProgress(ActionItemForUpdate actionItemForUpdate);
    }
}