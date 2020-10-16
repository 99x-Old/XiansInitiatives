using System;
using System.Collections.Generic;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.Shared.Dtos
{
    public class ActionItemForCreateDto
    {
        public string Id { get; set; }
        public string InitiativeId { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public DateTime Deadline { get; set; }
        public ICollection<ItemAssignee> Assignees { get; set; }
    }
}