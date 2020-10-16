using System.Collections.Generic;

namespace XiansInitiatives.Shared.Dtos
{
    public class ReportForDashboardDto
    {
        public string InitativeId { get; set; }
        public ProgressForDashboardDto Progress { get; set; }
        public ContributionsForDashboardDto Contributors { get; set; }
        public ICollection<UserForProfileDto> TopContributors { get; set; }
    }
}