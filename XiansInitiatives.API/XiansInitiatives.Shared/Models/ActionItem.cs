using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XiansInitiatives.Shared.Models
{
    public class ActionItem
    {
        [Required]
        public Guid Id { get; set; }

        public string Item { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual ApplicationUser CreatedBy { get; set; }

        [Required]
        public Guid InitiativeId { get; set; }

        [ForeignKey("InitiativeId")]
        public virtual Initiative Initiative { get; set; }
    }
}