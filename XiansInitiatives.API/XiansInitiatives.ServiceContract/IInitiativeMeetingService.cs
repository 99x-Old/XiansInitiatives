using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.ServiceContract
{
    public interface IInitiativeMeetingService
    {
        Task<List<InitiativeMeetingDto>> GetInitiativeMeetings(string initiativeId);

        Task<bool> AddInitiativeMeeting(InitiativeMeetingDto initiativeMeetingDto);
    }
}