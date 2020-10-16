using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Constants;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeMemberController : ControllerBase
    {
        private readonly IInitiativeMemberService _initiativeMemberService;
        private readonly IAzureBusService _azureBusService;

        public InitiativeMemberController(IInitiativeMemberService initiativeMemberService, IAzureBusService azureBusService)
        {
            _initiativeMemberService = initiativeMemberService;
            _azureBusService = azureBusService;
        }

        [Authorize]
        [HttpGet("{initiativeId}")]
        public async Task<IActionResult> GetInitiativeMembers(string initiativeId)
        {
            var initiativeMembers = await _initiativeMemberService.GetInitiativeMembers(initiativeId);

            if (initiativeMembers.Any())
            {
                return Ok(initiativeMembers);
            }

            if (!initiativeMembers.Any())
            {
                return NoContent();
            }
            return BadRequest("Failed to get initiative members");
        }

        [Authorize]
        [HttpGet("joinedInitiatives/{memberId}")]
        public async Task<IActionResult> GetJoinedInitatives(string memberId)
        {
            var joinedInitiatives = await _initiativeMemberService.GetJoinedInitiatives(memberId);

            if (joinedInitiatives.Any())
            {
                return Ok(joinedInitiatives);
            }

            if (!joinedInitiatives.Any())
            {
                return NoContent();
            }
            return BadRequest("Failed to get initiative members");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddInitiativeMember(InitiativeMemberDto initiativeMemberForCreateDto)
        {
            var initiativeMember = await _initiativeMemberService.GetInitiativeMember(initiativeMemberForCreateDto);

            if (initiativeMember != null)
            {
                return BadRequest("Already a member");
            }

            if (initiativeMember == null)
            {
                var result = await _initiativeMemberService.AddInitiativeMember(initiativeMemberForCreateDto);

                if (result)
                {
                    await _azureBusService.SendEmailAsync(initiativeMemberForCreateDto.MemberId, "initiativeAssign");
                    return Ok();
                }
            }

            return BadRequest("Adding member is failed");
        }

        [Authorize]
        [HttpPost("rateMember")]
        public async Task<IActionResult> RateMember(InitiativeMemberDto initiativeMemberForCreateDto)
        {
            var result = await _initiativeMemberService.RateMember(initiativeMemberForCreateDto);

            if (!result)
            {
                return BadRequest("Failed to rate the member");
            }

            if (result)
            {
                return Ok();
            }

            return BadRequest("Failed");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpDelete("{initiativeId}/{memberId}")]
        public async Task<IActionResult> RemoveInitiativeMember(string initiativeId, string memberId)
        {
            var initiativeMemberDto = new InitiativeMemberDto();
            initiativeMemberDto.InitiativeId = initiativeId;
            initiativeMemberDto.MemberId = memberId;

            var existingInitiativeMember = await _initiativeMemberService.GetInitiativeMember(initiativeMemberDto);
            if (existingInitiativeMember == null)
            {
                return BadRequest("Initiative member is not exists");
            }

            if (existingInitiativeMember != null)
            {
                var result = await _initiativeMemberService.RemoveInitiativeMember(existingInitiativeMember);

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest("Failed to remove the member");
        }
    }
}