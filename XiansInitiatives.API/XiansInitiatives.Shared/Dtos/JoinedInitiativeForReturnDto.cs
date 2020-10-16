using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class JoinedInitiativeForReturnDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Department { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string Lead { get; set; }
        public string CoLead { get; set; }
        public string Mentor { get; set; }
    }
}