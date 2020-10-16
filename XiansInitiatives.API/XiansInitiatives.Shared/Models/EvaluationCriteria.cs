using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace XiansInitiatives.Shared.Models
{
    public class EvaluationCriteria
    {
        [Required]
        public Guid Id { get; set; }

        public string Criteria { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Justification { get; set; }

        public double Weight { get; set; }
        public double Score { get; set; }

        [Required]
        public Guid ReviewCycleId { get; set; }

        [ForeignKey("InitiativeId")]
        public virtual Initiative ReviewCycle { get; set; }
    }
}