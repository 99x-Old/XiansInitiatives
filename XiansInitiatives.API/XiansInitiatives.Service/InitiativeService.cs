using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Templates;
using XiansInitiatives.DataContracts.IRepository;
using XiansInitiatives.ServiceContract;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;
using XiansInitiatives.Templetes.ViewModels;

namespace XiansInitiatives.Service
{
    public class InitiativeService : IInitiativeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureBusService _azureBusService;
        private readonly IRazorViewToStringRenderer _renderer;

        public InitiativeService(IMapper mapper, IUnitOfWork unitOfWork, IAzureBusService azureBusService, IRazorViewToStringRenderer renderer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _azureBusService = azureBusService;
            _renderer = renderer;
        }

        public async Task<bool> CreateInitiative(InitiativeForCreateDto initiativeForCreateDto)
        {
            var newInitiative = _mapper.Map<Initiative>(initiativeForCreateDto);

            newInitiative.CreatedAt = DateTime.Now;

            var result = await _unitOfWork.Initiative.CreateInitiative(newInitiative);

            return result;
        }

        public async Task<bool> CreateInitiativeYear(InitiativeYearForCreateDto initiativeYearForCreateDto)
        {
            var newInitiativeYear = _mapper.Map<InitiativeYear>(initiativeYearForCreateDto);

            newInitiativeYear.CreatedAt = DateTime.Now;

            var result = await _unitOfWork.InitiativeYear.CreateInitiativeYear(newInitiativeYear);

            return result;
        }

        public async Task<bool> DeleteInitiative(Initiative initiative)
        {
            return await _unitOfWork.Initiative.DeleteInitiative(initiative);
        }

        public async Task<Initiative> GetInitiative(string id)
        {
            var initiative = await _unitOfWork.Initiative.GetInitiative(id);

            if (initiative != null)
            {
                return initiative;
            }
            return null;
        }

        public async Task<InitiativeForReturnDto> GetInitiativeProfile(string id)
        {
            var initiative = await _unitOfWork.Initiative.GetInitiative(id);
            var initiativeForReturn = _mapper.Map<InitiativeForReturnDto>(initiative);

            var lead = await _unitOfWork.User.GetUser(initiative.LeadId);
            var colead = await _unitOfWork.User.GetUser(initiative.CoLeadId);
            var mentor = await _unitOfWork.User.GetUser(initiative.MentorId);
            initiativeForReturn.Lead = _mapper.Map<UserForProfileDto>(lead);
            initiativeForReturn.CoLead = _mapper.Map<UserForProfileDto>(colead);
            initiativeForReturn.Mentor = _mapper.Map<UserForProfileDto>(mentor);
            initiativeForReturn.NumberOfMembers = _unitOfWork.InitiativeMember.GetNumberOfMembers(id);

            if (initiativeForReturn != null)
            {
                return initiativeForReturn;
            }
            return null;
        }

        public async Task<List<Initiative>> GetInitiatives(string yearId)
        {
            var initiatives = await _unitOfWork.Initiative.GetInitiatives(yearId);

            if (initiatives != null)
            {
                return initiatives;
            }

            return null;
        }

        public async Task<List<Initiative>> GetInitiativesByYear()
        {
            var currentYear = DateTime.Now.Year;
            var initiatives = await _unitOfWork.Initiative.GetInitiativesByYear(currentYear);

            if (initiatives != null)
            {
                return initiatives;
            }

            return null;
        }

        public async Task<InitiativeYear> GetInitiativeYearByYear(int year)
        {
            var initiativeYear = await _unitOfWork.InitiativeYear.GetInitiativeYearByYear(year);

            if (initiativeYear != null)
            {
                return initiativeYear;
            }

            return null;
        }

        public async Task<IEnumerable<InitiativeYear>> GetInitiativeYears()
        {
            var initiativeYears = await _unitOfWork.InitiativeYear.GetInitiativeYears();

            if (initiativeYears != null)
            {
                return initiativeYears;
            }

            return null;
        }

        public async Task<bool> UpdateInitiative(InitiativeForUpdateDto initiativeForUpdateDto)
        {
            return await _unitOfWork.Initiative.UpdateInitiative(initiativeForUpdateDto);
        }

        public async Task<Initiative> GetLeadInitiative(string memberId)
        {
            return await _unitOfWork.Initiative.GetLeadInitiative(memberId);
        }

        public async Task<List<ReviewCycle>> GetReviewCycles(string initativeId)
        {
            return await _unitOfWork.ReviewCycle.GetReviewCycles(initativeId);
        }

        public async Task<bool> CreateReviewCycle(ReviewCycleDto reviewCycleDto)
        {
            var newCycle = _mapper.Map<ReviewCycle>(reviewCycleDto);

            newCycle.CreatedAt = DateTime.Now;

            var result = await _unitOfWork.ReviewCycle.CreateReviewCycle(newCycle);

            return result;
        }

        public async Task<bool> CreateEvaluationCriterion(EvaluationCriteriaDto evaluationCriteriaDto)
        {
            var newCriteria = _mapper.Map<EvaluationCriteria>(evaluationCriteriaDto);

            newCriteria.CreatedAt = DateTime.Now;

            var result = await _unitOfWork.EvaluationCriteria.CreateEvaluationCriterion(newCriteria);

            return result;
        }

        public async Task<bool> RemoveReviewCycle(string id)
        {
            return await _unitOfWork.ReviewCycle.RemoveReviewCycle(id);
        }

        public async Task<List<EvaluationCriteriaDto>> GetEvaluationCriterions(string reviewCycleId)
        {
            return await _unitOfWork.EvaluationCriteria.GetEvaluationCriterions(reviewCycleId);
        }

        public async Task<bool> RemoveEvaluationCriteria(string id)
        {
            return await _unitOfWork.EvaluationCriteria.RemoveEvaluationCriteria(id);
        }

        public async Task<ReportForDashboardDto> GetInitiativeReport(string initiativeId)
        {
            var report = new ReportForDashboardDto();
            report.InitativeId = initiativeId;
            var topContributors = await _unitOfWork.InitiativeMember.GetTopContributors(initiativeId);

            if (topContributors != null)
            {
                report.TopContributors = topContributors;
            }

            var progress = await _unitOfWork.ActionItem.GetProgressReport(initiativeId);

            if (progress != null)
            {
                report.Progress = progress;
            }
            var totalNumberofMembers = _unitOfWork.InitiativeMember.GetNumberOfMembers(initiativeId);
            var numberofKeyMembers = _unitOfWork.ActionItem.GetNumberOfKeyMembers(initiativeId);
            var contributionsForDashboardDto = new ContributionsForDashboardDto();
            contributionsForDashboardDto.KeyContributors = numberofKeyMembers;
            contributionsForDashboardDto.Contributors = totalNumberofMembers - numberofKeyMembers;
            report.Contributors = contributionsForDashboardDto;

            return report;
        }

        public async Task<bool> GenerateNewsLetter()
        {
            try
            {
                var initiativeYear = await _unitOfWork.InitiativeYear.GetInitiativeYearByYear(DateTime.Now.Year);

                var initiativeList = await _unitOfWork.Initiative.GetInitiatives(initiativeYear.Id.ToString());
                var reportList = new List<NewsLetterDto>();
                foreach (var initiative in initiativeList)
                {
                    var report = new NewsLetterDto();
                    var initiativeId = initiative.Id.ToString();
                    report.InitativeId = initiativeId;
                    report.Initative = initiative.Name;
                    var topContributors = await _unitOfWork.InitiativeMember.GetTopContributors(initiativeId);

                    if (topContributors != null)
                    {
                        report.TopContributors = topContributors;
                    }

                    var upComingReviewCycles = await _unitOfWork.ReviewCycle.GetUpComingReviewCycles(initiativeId);
                    if (upComingReviewCycles != null)
                    {
                        report.UpComingReviewCycles = upComingReviewCycles;
                    }
                    reportList.Add(report);
                }

                var model = new NewsLetterViewModel(reportList);

                const string view = "/Views/Emails/NewsLetter/NewsLetter";
                var htmlBody = await _renderer.RenderViewToStringAsync($"{view}Html.cshtml", model);

                await _azureBusService.SendNewsLetterAsync(htmlBody);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"error message: {e}");
                return false;
            }
        }
    }
}