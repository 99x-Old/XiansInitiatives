using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XiansInitiatives.Shared.Models
{
    public class ReviewCycle
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ScheduledAt { get; set; }

        public string OverallComment { get; set; }
        public string Description { get; set; }

        [Required]
        public Guid InitiativeId { get; set; }

        [ForeignKey("InitiativeId")]
        public virtual Initiative Initiative { get; set; }

        [Required]
        public string EvaluatorId { get; set; }

        [ForeignKey("EvaluatorId")]
        public virtual ApplicationUser Evaluator { get; set; }
    }
}