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
    public class InitiativeMemberService : IInitiativeMemberService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InitiativeMemberService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<InitiativeMember> GetInitiativeMember(InitiativeMemberDto initiativeMemberForCreateDto)
        {
            var initiativeMember = await _unitOfWork.InitiativeMember.GetInitiativeMember(initiativeMemberForCreateDto.MemberId, initiativeMemberForCreateDto.InitiativeId);

            return initiativeMember;
        }

        public async Task<bool> AddInitiativeMember(InitiativeMemberDto initiativeMemberForCreateDto)
        {
            var newInitiativeMember = _mapper.Map<InitiativeMember>(initiativeMemberForCreateDto);
            newInitiativeMember.CreatedAt = DateTime.Now;
            return await _unitOfWork.InitiativeMember.AddInitiativeMember(newInitiativeMember);
        }

        public async Task<bool> RemoveInitiativeMember(InitiativeMember initiativeMember)
        {
            return await _unitOfWork.InitiativeMember.RemoveInitiativeMember(initiativeMember);
        }

        public async Task<List<ApplicationUser>> GetInitiativeMembers(string initiativeId)
        {
            return await _unitOfWork.InitiativeMember.GetInitiativeMembers(initiativeId);
        }

        public async Task<List<JoinedInitiativeForReturnDto>> GetJoinedInitiatives(string memberId)
        {
            return await _unitOfWork.InitiativeMember.GetJoinedInitiatives(memberId);
        }

        public async Task<bool> RateMember(InitiativeMemberDto initiativeMemberForRate)
        {
            var initiativeMember = await _unitOfWork.InitiativeMember
                .GetInitiativeMember(initiativeMemberForRate.MemberId, initiativeMemberForRate.InitiativeId);

            if (initiativeMemberForRate.Rating != 0 && initiativeMember.Rating == 0)
            {
                return await _unitOfWork.InitiativeMember.RateMember(initiativeMemberForRate);
            }

            if (initiativeMemberForRate.Rating != 0 && initiativeMember.Rating != 0)
            {
                var rating = (initiativeMember.Rating + initiativeMemberForRate.Rating) / 2;
                if (initiativeMember.Rating == rating)
                {
                    return true;
                }
                else
                {
                    initiativeMemberForRate.Rating = rating;
                }
            }
            return await _unitOfWork.InitiativeMember.RateMember(initiativeMemberForRate);
        }
    }
}