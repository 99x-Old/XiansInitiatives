using System;
using System.Collections.Generic;

namespace XiansInitiatives.Shared.Dtos
{
    public class ActionItemForUpdate
    {
        public string Id { get; set; }
        public int Progress { get; set; }
        public DateTime Deadline { get; set; }

        public ICollection<ItemAssigneeDto> Assignees { get; set; }
    }
}