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
    public class InitiativeController : ControllerBase
    {
        private readonly IInitiativeService _initiativeService;

        public InitiativeController(IInitiativeService initiativeService)
        {
            _initiativeService = initiativeService;
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpPost("year")]
        public async Task<IActionResult> CreateInitiativeYear(InitiativeYearForCreateDto initiativeYearForCreateDto)
        {
            var initiativeYear = await _initiativeService.GetInitiativeYearByYear(initiativeYearForCreateDto.Year);
            if (initiativeYear != null)
            {
                return BadRequest("Initiative year already exists");
            }
            var result = await _initiativeService.CreateInitiativeYear(initiativeYearForCreateDto);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest("Initiative year creation failed");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpPost("newinitiative")]
        public async Task<IActionResult> CreateInitiative(InitiativeForCreateDto initiativeForCreateDto)
        {
            var result = await _initiativeService.CreateInitiative(initiativeForCreateDto);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest("Initiative  creation failed");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpGet("initiativeyears")]
        public async Task<IActionResult> GetInitiativeYears()
        {
            var initiativeYears = await _initiativeService.GetInitiativeYears();
            if (initiativeYears != null)
            {
                return Ok(initiativeYears);
            }
            return BadRequest("Failed to get initiative years");
        }

        [Authorize(Roles = UserRoles.LeadRole)]
        [HttpGet("checklead/{memberId}")]
        public async Task<IActionResult> GetLeadInitiative(string memberId)
        {
            var initiative = await _initiativeService.GetLeadInitiative(memberId);
            if (initiative != null)
            {
                return Ok(initiative);
            }
            if (initiative == null)
            {
                return Ok(initiative);
            }
            return BadRequest("You have not assigned to any initiative as a lead yet");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpGet("initiativeyear/{yearId}")]
        public async Task<IActionResult> GetInitiatives(string yearId)
        {
            var initiatives = await _initiativeService.GetInitiatives(yearId);
            if (initiatives.Any())
            {
                return Ok(initiatives);
            }

            if (!initiatives.Any())
            {
                return NoContent();
            }
            return BadRequest("Failed to get initiative list");
        }

        [Authorize]
        [HttpGet("currentyear")]
        public async Task<IActionResult> GetInitiativesByYear()
        {
            var initiatives = await _initiativeService.GetInitiativesByYear();
            if (initiatives.Any())
            {
                return Ok(initiatives);
            }

            if (!initiatives.Any())
            {
                return NoContent();
            }
            return BadRequest("Failed to get initiative list");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInitiative(string Id)
        {
            var initiative = await _initiativeService.GetInitiative(Id);
            if (initiative != null)
            {
                return Ok(initiative);
            }

            if (initiative == null)
            {
                return BadRequest("Initiative not exists");
            }
            return BadRequest("Failed to get initiative list");
        }

        [Authorize]
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetInitiativeProfile(string id)
        {
            var initiative = await _initiativeService.GetInitiativeProfile(id);
            if (initiative != null)
            {
                return Ok(initiative);
            }

            if (initiative == null)
            {
                return BadRequest("Initiative not exists");
            }
            return BadRequest("Failed to get initiative list");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInitiative(string id)
        {
            var existingInitiative = await _initiativeService.GetInitiative(id);
            if (existingInitiative == null)
            {
                return BadRequest("Initiative not exists");
            }

            if (existingInitiative != null)
            {
                var result = await _initiativeService.DeleteInitiative(existingInitiative);

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest("Failed to delete Initiative");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpPut("updateinitiative")]
        public async Task<IActionResult> UpdateInitiative(InitiativeForUpdateDto initiativeForUpdateDto)
        {
            var existingInitiative = await _initiativeService.GetInitiative(initiativeForUpdateDto.Id);
            if (existingInitiative == null)
            {
                return BadRequest("Initiative not exists");
            }

            if (existingInitiative != null)
            {
                var result = await _initiativeService.UpdateInitiative(initiativeForUpdateDto);

                if (result)
                {
                    return Ok();
                }
            }
            return BadRequest("Initiative update failed");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpGet("review/{initiativeId}")]
        public async Task<IActionResult> GetReviewCycles(string initiativeId)
        {
            var reviewCycles = await _initiativeService.GetReviewCycles(initiativeId);
            if (reviewCycles.Any())
            {
                return Ok(reviewCycles);
            }

            if (!reviewCycles.Any())
            {
                return NoContent();
            }
            return BadRequest("Failed to get review cycle list");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpGet("reviewCriteria/{reviewCycleId}")]
        public async Task<IActionResult> GetEvaluationCriterions(string reviewCycleId)
        {
            var evaluationCriterions = await _initiativeService.GetEvaluationCriterions(reviewCycleId);
            if (evaluationCriterions.Any())
            {
                return Ok(evaluationCriterions);
            }

            if (!evaluationCriterions.Any())
            {
                return NoContent();
            }
            return BadRequest("Failed to get evaluation criterions list");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpPost("review")]
        public async Task<IActionResult> CreateReviewCycle(ReviewCycleDto reviewCycleDto)
        {
            var result = await _initiativeService.CreateReviewCycle(reviewCycleDto);

            if (result == true)
            {
                return Ok();
            }
            return BadRequest("Starting cycle failed");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpPost("reviewCriteria")]
        public async Task<IActionResult> CreateEvaluationCriterion([FromBody] EvaluationCriteriaDto evaluationCriteriaDto)
        {
            var result = await _initiativeService.CreateEvaluationCriterion(evaluationCriteriaDto);

            if (result == true)
            {
                return Ok();
            }
            return BadRequest("Starting cycle failed");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpDelete("reviewCriteria/{id}")]
        public async Task<IActionResult> RemoveEvaluationCriteria(string id)
        {
            var result = await _initiativeService.RemoveEvaluationCriteria(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest("Failed to delete the evaluation criteria");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpDelete("review/{id}")]
        public async Task<IActionResult> RemoveReviewCycle(string id)
        {
            var result = await _initiativeService.RemoveReviewCycle(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest("Failed to delete the review cycle");
        }

        [Authorize(Roles = UserRoles.EvaluatorRole)]
        [HttpGet("report/{initiativeId}")]
        public async Task<IActionResult> GetInitiativeReport(string initiativeId)
        {
            var initiativeReport = await _initiativeService.GetInitiativeReport(initiativeId);
            if (initiativeReport != null)
            {
                return Ok(initiativeReport);
            }

            if (initiativeReport == null)
            {
                return NoContent();
            }
            return BadRequest("Failed to get initiative list");
        }

        [HttpGet("newsLetter")]
        public async Task<IActionResult> InvokeNewsLetter()
        {
            var result = await _initiativeService.GenerateNewsLetter();
            if (result == true)
            {
                return Ok();
            }

            return BadRequest("Failed to Generate news letter");
        }
    }
}