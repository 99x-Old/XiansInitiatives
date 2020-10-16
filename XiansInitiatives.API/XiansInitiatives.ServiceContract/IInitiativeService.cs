using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.ServiceContract
{
    public interface IInitiativeService
    {
        Task<bool> CreateInitiativeYear(InitiativeYearForCreateDto initiativeYearForCreateDto);

        Task<bool> CreateInitiative(InitiativeForCreateDto initiativeForCreateDto);

        Task<bool> CreateEvaluationCriterion(EvaluationCriteriaDto evaluationCriteriaDto);

        Task<bool> CreateReviewCycle(ReviewCycleDto reviewCycleDto);

        Task<InitiativeYear> GetInitiativeYearByYear(int year);

        Task<IEnumerable<InitiativeYear>> GetInitiativeYears();

        Task<List<Initiative>> GetInitiatives(string yearId);

        Task<List<ReviewCycle>> GetReviewCycles(string initativeId);

        Task<List<EvaluationCriteriaDto>> GetEvaluationCriterions(string reviewCycleId);

        Task<List<Initiative>> GetInitiativesByYear();

        Task<Initiative> GetInitiative(string id);

        Task<InitiativeForReturnDto> GetInitiativeProfile(string id);

        Task<Initiative> GetLeadInitiative(string memberId);

        Task<bool> UpdateInitiative(InitiativeForUpdateDto initiativeForUpdateDto);

        Task<bool> DeleteInitiative(Initiative initiative);

        Task<bool> RemoveReviewCycle(string id);

        Task<bool> RemoveEvaluationCriteria(string id);

        Task<ReportForDashboardDto> GetInitiativeReport(string initiativeId);

        Task<bool> GenerateNewsLetter();
    }
}