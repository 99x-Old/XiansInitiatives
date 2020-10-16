using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IEvaluationCriteriaRepository
    {
        Task<List<EvaluationCriteriaDto>> GetEvaluationCriterions(string reviewCycleId);

        Task<bool> CreateEvaluationCriterion(EvaluationCriteria evaluationCriteria);

        Task<bool> UpdateEvaluationCriterion(string id);

        Task<bool> RemoveEvaluationCriteria(string id);
    }
}