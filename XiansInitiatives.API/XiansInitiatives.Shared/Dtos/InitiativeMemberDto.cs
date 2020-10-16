using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class InitiativeMemberDto
    {
        [Required]
        public string InitiativeId { get; set; }

        [Required]
        public string MemberId { get; set; }

        public int Rating { get; set; }
    }
}