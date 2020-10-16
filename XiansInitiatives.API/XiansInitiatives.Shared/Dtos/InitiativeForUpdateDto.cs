using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class InitiativeForUpdateDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string InitiativeYearId { get; set; }

        [Required]
        public string Department { get; set; }

        public string Description { get; set; }

        public string LeadId { get; set; }
        public string CoLeadId { get; set; }
        public string MentorId { get; set; }
        public string CreatedById { get; set; }
    }
}