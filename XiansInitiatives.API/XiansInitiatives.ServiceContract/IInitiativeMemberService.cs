using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.ServiceContract
{
    public interface IInitiativeMemberService
    {
        Task<InitiativeMember> GetInitiativeMember(InitiativeMemberDto initiativeMemberForCreateDto);

        Task<List<ApplicationUser>> GetInitiativeMembers(string initiativeId);

        Task<List<JoinedInitiativeForReturnDto>> GetJoinedInitiatives(string memberId);

        Task<bool> AddInitiativeMember(InitiativeMemberDto initiativeMemberForCreateDto);

        Task<bool> RateMember(InitiativeMemberDto initiativeMemberForRate);

        Task<bool> RemoveInitiativeMember(InitiativeMember initiativeMember);
    }
}