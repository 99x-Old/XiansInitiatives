using System;
using System.Collections.Generic;
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
    public class ActionItemController : Controller
    {
        private readonly IActionItemService _actionItemService;

        public ActionItemController(IActionItemService actionItemService)
        {
            _actionItemService = actionItemService;
        }

        [Authorize(Roles = UserRoles.LeadRole)]
        [HttpPost]
        public async Task<IActionResult> CreateActionItem(ActionItemForCreateDto actionItemForCreateDto)
        {
            var result = await _actionItemService.CreateActionItem(actionItemForCreateDto);

            if (result)
            {
                return Ok();
            }
            return BadRequest("Failed to create the action item");
        }

        [Authorize(Roles = UserRoles.LeadRole)]
        [HttpPut]
        public async Task<IActionResult> UpdateProgress(ActionItemForUpdate actionItemForUpdate)
        {
            var result = await _actionItemService.UpdateProgress(actionItemForUpdate);

            if (result)
            {
                return Ok();
            }
            return BadRequest("Failed to update the action item");
        }

        [Authorize]
        [HttpGet("{initiativeId}")]
        public async Task<IActionResult> GetActionItems(string initiativeId)
        {
            var actionItems = await _actionItemService.GetActionItems(initiativeId);
            if (actionItems != null)
            {
                return Ok(actionItems);
            }

            if (actionItems == null)
            {
                return BadRequest("Action items were not found");
            }
            return BadRequest("Failed to get initiative years");
        }

        [Authorize]
        [HttpGet("contribution/{memberId}")]
        public async Task<IActionResult> GetContribution(string memberId)
        {
            var actionItems = await _actionItemService.GetContribution(memberId);
            if (actionItems != null)
            {
                return Ok(actionItems);
            }

            if (actionItems == null)
            {
                return BadRequest("Action items were not found");
            }
            return BadRequest("Failed to get initiative years");
        }

        [Authorize]
        [HttpPost("addcomment")]
        public async Task<IActionResult> Addcomment(CommentItemDto commentItemDto)
        {
            var result = await _actionItemService.AddComment(commentItemDto);

            if (result)
            {
                return Ok();
            }
            return BadRequest("Failed to create the action item");
        }
    }
}