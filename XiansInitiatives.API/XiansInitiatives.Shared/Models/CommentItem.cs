using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XiansInitiatives.Shared.Models
{
    public class CommentItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual ActionItem ActionItem { get; set; }

        [Required]
        public string CommenterId { get; set; }

        [ForeignKey("CommenterId")]
        public virtual ApplicationUser Commenter { get; set; }

        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}