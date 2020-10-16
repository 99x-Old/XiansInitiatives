using System;

namespace XiansInitiatives.Shared.Dtos
{
    public class InitiativeForReturnDto
    {
        public string Id { get; set; }
        public string YearId { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string Description { get; set; }
        public int NumberOfMembers { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserForProfileDto Lead { get; set; }
        public UserForProfileDto CoLead { get; set; }
        public UserForProfileDto Mentor { get; set; }
        public UserForProfileDto CreatedBy { get; set; }
    }
}