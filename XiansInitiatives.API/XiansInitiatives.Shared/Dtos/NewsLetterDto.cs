using System.Collections.Generic;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Shared.Dtos
{
    public class NewsLetterDto
    {
        public string InitativeId { get; set; }
        public string Initative { get; set; }
        public ICollection<UserForProfileDto> TopContributors { get; set; }
        public ICollection<ReviewCycle> UpComingReviewCycles { get; set; }
    }
}