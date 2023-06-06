using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GoalMonitoringApp.Core.Models
{
    public class Goals
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? FinishedDate { get; set; } // Nullable DateTime to track finished date
        public DateTime TargetDate { get; set; }
    }
}
