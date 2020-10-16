using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data.Repository
{
    public class ActionItemRepository : Repository<ActionItem>, IActionItemRepository
    {
        private readonly IdentityDataContext _db;

        public ActionItemRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ActionItem> CreateActionItem(ActionItem actionItem)
        {
            await _db.ActionItem.AddAsync(actionItem);
            if (_db.SaveChanges() > 0)
            {
                return actionItem;
            }
            return null;
        }

        public async Task<List<ActionItemForReturnDto>> GetActionItems(string intiativeId)
        {
            var query = _db.ActionItem.AsQueryable();

            var actionItemList = await query.Where(ai => ai.InitiativeId.ToString() == intiativeId).Select(ai => new ActionItemForReturnDto
            {
                Id = ai.Id.ToString(),
                Item = ai.Item,
                Description = ai.Description,
                Deadline = ai.Deadline.ToString("dd/MM/yyyy"),
                Progress = ai.Progress,
                Start = ai.CreatedAt.ToString("dd/MM/yyyy")
            }).ToListAsync();

            return actionItemList;
        }

        public Task<List<ActionItemForReturnDto>> GetContribution(string memberId)
        {
            var result = from actionItem in _db.ActionItem
                         join itemAssignee in _db.ItemAssignee
                         on actionItem.Id equals itemAssignee.ItemId
                         where itemAssignee.AssigneeId == memberId
                         select new ActionItemForReturnDto
                         {
                             Id = actionItem.Id.ToString(),
                             Item = actionItem.Item,
                             Description = actionItem.Description,
                             Deadline = actionItem.Deadline.ToString("dd/MM/yyyy"),
                             Progress = actionItem.Progress,
                             Start = actionItem.CreatedAt.ToString("dd/MM/yyyy")
                         };
            return result.Distinct().ToListAsync();
        }

        public int GetNumberOfKeyMembers(string initiativeId)
        {
            var result = from initiative in _db.Initiative
                         join actionItem in _db.ActionItem
                         on initiative.Id equals actionItem.InitiativeId
                         join itemAssignee in _db.ItemAssignee
                         on actionItem.Id equals itemAssignee.ItemId
                         where initiative.Id.ToString() == initiativeId

                         select new ItemAssignee
                         {
                             AssigneeId = itemAssignee.AssigneeId
                         };
            return result.Distinct().Count();
        }

        public async Task<ProgressForDashboardDto> GetProgressReport(string intiativeId)
        {
            var progressForDashboardDto = new ProgressForDashboardDto();
            var query = _db.ActionItem.AsQueryable();
            progressForDashboardDto.Total = await query.Where(ai => ai.InitiativeId.ToString() == intiativeId).CountAsync();
            progressForDashboardDto.Completed = await query.Where(ai => ai.InitiativeId.ToString() == intiativeId && ai.Progress == 100).CountAsync();
            progressForDashboardDto.Inprogress = progressForDashboardDto.Total - progressForDashboardDto.Completed;
            return progressForDashboardDto;
        }

        public async Task<bool> UpdateProgress(ActionItemForUpdate actionItemForUpdate)
        {
            var actionItem = await _db.ActionItem.FirstOrDefaultAsync(i => i.Id.ToString() == actionItemForUpdate.Id);

            if (actionItem != null)
            {
                actionItem.Progress = actionItemForUpdate.Progress;
                return _db.SaveChanges() > 0;
            }

            return false;
        }
    }
}