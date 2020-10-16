using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IInitiativeMemberRepository
    {
        Task<InitiativeMember> GetInitiativeMember(string MemberId, string InitiativeId);

        Task<List<ApplicationUser>> GetInitiativeMembers(string initiativeId);

        Task<List<JoinedInitiativeForReturnDto>> GetJoinedInitiatives(string memberId);

        Task<bool> AddInitiativeMember(InitiativeMember initiativeMember);

        Task<bool> RateMember(InitiativeMemberDto initiativeMemberForRate);

        Task<bool> RemoveInitiativeMember(InitiativeMember initiativeMember);

        int GetNumberOfMembers(string initiativeId);        

        Task<List<UserForProfileDto>> GetTopContributors(string initiativeId);
    }
}