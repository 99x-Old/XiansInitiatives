using System.Collections.Generic;

namespace XiansInitiatives.Shared.Dtos
{
    public class ActionItemForReturnDto
    {
        public string Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public string Deadline { get; set; }
        public string Start { get; set; }
        public ICollection<ItemAssigneeDto> Assignees { get; set; }
        public ICollection<CommentItemDto> CommentItem { get; set; }
    }
}