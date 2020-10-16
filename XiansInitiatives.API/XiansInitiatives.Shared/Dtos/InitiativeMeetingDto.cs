using System;
using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class InitiativeMeetingDto
    {
        public string Id { get; set; }

        [Required]
        public string Purpose { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ScheduledAt { get; set; }

        public string MeetingNotes { get; set; }

        public string OrganizerId { get; set; }

        public string OrganizerName { get; set; }

        [Required]
        public string InitiativeId { get; set; }
    }
}