using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Constants;
using XiansInitiatives.Shared.Dtos;

namespace XiansInitiatives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeMeetingController : ControllerBase
    {
        private readonly IInitiativeMeetingService _initiativeMeetingService;
        private readonly IAzureBusService _azureBusService;

        public InitiativeMeetingController(IInitiativeMeetingService initiativeMeetingService, IAzureBusService azureBusService)
        {
            _initiativeMeetingService = initiativeMeetingService;
            _azureBusService = azureBusService;
        }

        [Authorize]
        [HttpGet("{initiativeId}")]
        public async Task<IActionResult> GetInitativeMeeetings(string initiativeId)
        {
            var initiativeMembers = await _initiativeMeetingService.GetInitiativeMeetings(initiativeId);

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

        [Authorize(Roles = UserRoles.LeadRole)]
        [HttpPost]
        public async Task<IActionResult> AddInitiativeMeeting(InitiativeMeetingDto initiativeMeetingDto)
        {
            var result = await _initiativeMeetingService.AddInitiativeMeeting(initiativeMeetingDto);

            if (result)
            {
                return Ok();
            }

            return BadRequest("Meeting saving failed");
        }

        [Authorize(Roles = UserRoles.LeadRole)]
        [HttpPost("invite/{channelId}")]
        public async Task<IActionResult> SendInvitations(string channelId)
        {
            if (channelId != null)
            {
                await _azureBusService.SendMeetingInvitationsAsync(channelId);
                return Ok();
            }

            return BadRequest("Failed to send the invitations");
        }
    }
}