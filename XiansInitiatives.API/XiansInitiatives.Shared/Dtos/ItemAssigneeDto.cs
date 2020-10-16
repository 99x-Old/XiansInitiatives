using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class ItemAssigneeDto
    {
        [Required]
        public string AssigneeId { get; set; }

        public string ItemId { get; set; }

        public string FullName { get; set; }
    }
}