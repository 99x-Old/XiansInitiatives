using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Models;
using Microsoft.EntityFrameworkCore;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.Data.Repository
{
    public class EvaluationCriteriaRepository : Repository<EvaluationCriteria>, IEvaluationCriteriaRepository
    {
        private readonly IdentityDataContext _db;

        public EvaluationCriteriaRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> CreateEvaluationCriterion(EvaluationCriteria evaluationCriteria)
        {
            await _db.EvaluationCriteria.AddAsync(evaluationCriteria);
            return _db.SaveChanges() > 0;
        }

        public Task<List<EvaluationCriteriaDto>> GetEvaluationCriterions(string reviewCycleId)
        {
            var result = from evaluationCriterion in _db.EvaluationCriteria
                         join reviewCycle in _db.ReviewCycle
                         on evaluationCriterion.ReviewCycleId equals reviewCycle.Id
                         join user in _db.ApplicationUser
                         on reviewCycle.EvaluatorId equals user.Id
                         where reviewCycle.Id.ToString() == reviewCycleId
                         select new EvaluationCriteriaDto
                         {
                             Id = evaluationCriterion.Id.ToString(),
                             CreatedAt = evaluationCriterion.CreatedAt,
                             Weight = evaluationCriterion.Weight,
                             Score = evaluationCriterion.Score,
                             Criteria = evaluationCriterion.Criteria,
                             ReviewerName = user.FullName,
                             Justification = evaluationCriterion.Justification
                         };

            return result.ToListAsync();
        }

        public async Task<bool> RemoveEvaluationCriteria(string id)
        {
            var existingCriteria = await _db.EvaluationCriteria.FirstOrDefaultAsync(i => i.Id.ToString() == id);

            if (existingCriteria == null)
            {
                return false;
            }

            _db.EvaluationCriteria.Remove(existingCriteria);
            return _db.SaveChanges() > 0;
        }

        public Task<bool> UpdateEvaluationCriterion(string id)
        {
            throw new NotImplementedException();
        }
    }
}