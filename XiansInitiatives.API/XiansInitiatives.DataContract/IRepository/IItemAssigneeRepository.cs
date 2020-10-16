using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IItemAssigneeRepository
    {
        Task<List<ItemAssigneeDto>> GetAssignees(string itemId);

        Task<bool> InsertAssignees(ItemAssignee itemAssignee);
    }
}