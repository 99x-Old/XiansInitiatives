using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XiansInitiatives.Shared.Models
{
    public class ItemAssignee
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual ActionItem ActionItem { get; set; }

        [Required]
        public string AssigneeId { get; set; }

        [ForeignKey("AssigneeId")]
        public virtual ApplicationUser Assignee { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}