using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XiansInitiatives.Shared.Models
{
    public class Initiative
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string LeadId { get; set; }
        public string CoLeadId { get; set; }
        public string MentorId { get; set; }
        public string CreatedById { get; set; }

        public ApplicationUser Lead { get; set; }

        public ApplicationUser CoLead { get; set; }

        public ApplicationUser Mentor { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public Guid InitiativeYearId { get; set; }

        [ForeignKey("InitiativeYearId")]
        public virtual InitiativeYear InitiativeYear { get; set; }
    }
}