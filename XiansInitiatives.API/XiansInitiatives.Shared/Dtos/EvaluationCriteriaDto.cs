using System;
using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class EvaluationCriteriaDto
    {
        [Required]
        public string ReviewCycleId { get; set; }

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Criteria { get; set; }
        public string Justification { get; set; }
        public double Weight { get; set; }
        public double Score { get; set; }
        public string ReviewerName { get; set; }
    }
}