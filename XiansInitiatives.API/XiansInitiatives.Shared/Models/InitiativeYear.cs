using System;
using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Models
{
    public class InitiativeYear
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Year { get; set; }

        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ApplicationUser CreatedBy { get; set; }
    }
}