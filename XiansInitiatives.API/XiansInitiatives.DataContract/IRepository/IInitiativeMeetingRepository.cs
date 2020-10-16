using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IInitiativeMeetingRepository
    {
        Task<bool> AddInitiativeMeeting(InitiativeMeeting initiativeMeeting);

        Task<List<InitiativeMeetingDto>> GetInitiativeMeetings(string initiativeId);
    }
}