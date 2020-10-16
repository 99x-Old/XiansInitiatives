using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XiansInitiatives.Shared.Models
{
    public class InitiativeMember
    {
        public DateTime CreatedAt { get; set; }

        public int Rating { get; set; }

        [Required]
        public Guid InitiativeId { get; set; }

        [ForeignKey("InitiativeId")]
        public virtual Initiative Initiative { get; set; }

        [Required]
        public string MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual ApplicationUser Member { get; set; }
    }
}