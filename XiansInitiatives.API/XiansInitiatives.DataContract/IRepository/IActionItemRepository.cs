using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IActionItemRepository
    {
        Task<List<ActionItemForReturnDto>> GetActionItems(string intiativeId);

        Task<List<ActionItemForReturnDto>> GetContribution(string memberId);

        Task<ActionItem> CreateActionItem(ActionItem actionItem);

        Task<ProgressForDashboardDto> GetProgressReport(string intiativeId);

        Task<bool> UpdateProgress(ActionItemForUpdate actionItemForUpdate);

        int GetNumberOfKeyMembers(string initiativeId);
    }
}