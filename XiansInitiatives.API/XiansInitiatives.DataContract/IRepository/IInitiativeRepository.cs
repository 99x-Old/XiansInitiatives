using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IInitiativeRepository
    {
        Task<List<Initiative>> GetInitiatives(string yearId);

        Task<List<Initiative>> GetInitiativesByYear(int year);

        Task<bool> CreateInitiative(Initiative initiative);

        Task<InitiativeYear> GetInitiativeYearByYear(int Year);

        Task<Initiative> GetInitiative(string id);

        Task<Initiative> GetLeadInitiative(string memberId);

        Task<bool> UpdateInitiative(InitiativeForUpdateDto initiativeForUpdateDto);

        Task<bool> DeleteInitiative(Initiative initiative);
    }
}