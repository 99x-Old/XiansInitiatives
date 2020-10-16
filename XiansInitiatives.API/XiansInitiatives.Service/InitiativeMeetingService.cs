using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Service
{
    public class InitiativeMeetingService : IInitiativeMeetingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InitiativeMeetingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddInitiativeMeeting(InitiativeMeetingDto initiativeMeetingDto)
        {
            var newMeeting = _mapper.Map<InitiativeMeeting>(initiativeMeetingDto);
            newMeeting.CreatedAt = DateTime.Now;
            newMeeting.ScheduledAt = DateTime.Now;
            return await _unitOfWork.InitiativeMeeting.AddInitiativeMeeting(newMeeting);
        }

        public async Task<List<InitiativeMeetingDto>> GetInitiativeMeetings(string initiativeId)
        {
            var initiativeMeeting = await _unitOfWork.InitiativeMeeting.GetInitiativeMeetings(initiativeId);

            return initiativeMeeting;
        }
    }
}