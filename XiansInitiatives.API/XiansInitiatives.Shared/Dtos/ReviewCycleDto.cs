using System;
using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class ReviewCycleDto
    {
        [Required]
        public string EvaluatorId { get; set; }

        [Required]
        public string InitiativeId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime ScheduledAt { get; set; }
    }
}