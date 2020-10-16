using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class InitiativeYearForCreateDto
    {
        [Required]
        public int Year { get; set; }

        public string Description { get; set; }
    }
}