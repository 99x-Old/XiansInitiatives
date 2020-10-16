using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XiansInitiatives.Shared.Models
{
    public class InitiativeMeeting
    {
        [Required]
        public Guid Id { get; set; }

        public string Purpose { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ScheduledAt { get; set; }

        public string MeetingNotes { get; set; }

        [Required]
        public string OrganizerId { get; set; }

        [ForeignKey("OrganizerId")]
        public virtual ApplicationUser Organizer { get; set; }

        [Required]
        public Guid InitiativeId { get; set; }

        [ForeignKey("InitiativeId")]
        public virtual Initiative Initiative { get; set; }
    }
}