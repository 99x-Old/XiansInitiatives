using System;
using System.ComponentModel.DataAnnotations;

namespace XiansInitiatives.Shared.Dtos
{
    public class CommentItemDto
    {
        [Required]
        public string ItemId { get; set; }

        [Required]
        public string CommenterId { get; set; }

        [Required]
        public string Comment { get; set; }

        public string CommenterName { get; set; }
        public string ProfileImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}