using AutoMapper;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserForProfileDto>();
            CreateMap<ApplicationUser, UsersForAdminDto>();
            CreateMap<UsersForAdminDto, ApplicationUser>();
            CreateMap<InitiativeYearForCreateDto, InitiativeYear>();
            CreateMap<InitiativeForCreateDto, Initiative>();
            CreateMap<InitiativeForUpdateDto, Initiative>();
            CreateMap<InitiativeMemberDto, InitiativeMember>();
            CreateMap<Initiative, InitiativeForReturnDto>();
            CreateMap<UserForProfileDto, ApplicationUser>();
            CreateMap<ActionItemForCreateDto, ActionItem>();
            CreateMap<ItemAssigneeDto, ItemAssignee>();
            CreateMap<CommentItemDto, CommentItem>();
            CreateMap<ReviewCycleDto, ReviewCycle>();
            CreateMap<EvaluationCriteriaDto, EvaluationCriteria>();
            CreateMap<InitiativeMeetingDto, InitiativeMeeting>();
        }
    }
}