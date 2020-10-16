using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data.Repository
{
    internal class ItemAssigneeRepository : Repository<ItemAssignee>, IItemAssigneeRepository
    {
        private readonly IdentityDataContext _db;

        public ItemAssigneeRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public Task<List<ItemAssigneeDto>> GetAssignees(string itemId)
        {
            var result = from itemAssignee in _db.ItemAssignee
                         join user in _db.ApplicationUser
                         on itemAssignee.AssigneeId equals user.Id
                         where itemAssignee.ItemId.ToString() == itemId
                         select new ItemAssigneeDto
                         {
                             AssigneeId = itemAssignee.AssigneeId,
                             FullName = user.FullName
                         };

            return result.Distinct().ToListAsync();
        }

        public async Task<bool> InsertAssignees(ItemAssignee itemAssignee)
        {
            await _db.ItemAssignee.AddAsync(itemAssignee);

            return _db.SaveChanges() > 0;
        }
    }
}