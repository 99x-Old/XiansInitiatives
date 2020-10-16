using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Data.Repository
{
    public class ReviewCycleRepository : Repository<ReviewCycle>, IReviewCycleRepository
    {
        private readonly IdentityDataContext _db;

        public ReviewCycleRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> CreateReviewCycle(ReviewCycle reviewCycle)
        {
            await _db.ReviewCycle.AddAsync(reviewCycle);
            return _db.SaveChanges() > 0;
        }

        public Task<List<ReviewCycle>> GetReviewCycles(string initativeId)
        {
            var result = from reviewCycle in _db.ReviewCycle
                         join user in _db.ApplicationUser
                         on reviewCycle.EvaluatorId equals user.Id
                         where reviewCycle.InitiativeId.ToString() == initativeId
                         select new ReviewCycle
                         {
                             Id = reviewCycle.Id,
                             CreatedAt = reviewCycle.CreatedAt,
                             ScheduledAt = reviewCycle.ScheduledAt,
                             Description = reviewCycle.Description
                         };

            return result.ToListAsync();
        }

        public async Task<List<ReviewCycle>> GetUpComingReviewCycles(string initativeId)
        {
            var query = _db.ReviewCycle.AsQueryable();
            var result = await query.Where(r => r.ScheduledAt > DateTime.Now && r.InitiativeId.ToString() == initativeId).ToListAsync();
            return result;
        }

        public async Task<bool> RemoveReviewCycle(string id)
        {
            var existingReviewCycle = await _db.ReviewCycle.FirstOrDefaultAsync(i => i.Id.ToString() == id);

            if (existingReviewCycle == null)
            {
                return false;
            }

            _db.ReviewCycle.Remove(existingReviewCycle);
            return _db.SaveChanges() > 0;
        }
    }
}