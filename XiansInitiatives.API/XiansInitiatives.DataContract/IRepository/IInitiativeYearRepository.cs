using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IInitiativeYearRepository
    {
        Task<InitiativeYear> GetInitiativeYearByYear(int year);

        Task<IEnumerable<InitiativeYear>> GetInitiativeYears();

        Task<bool> CreateInitiativeYear(InitiativeYear initiativeYear);
    }
}