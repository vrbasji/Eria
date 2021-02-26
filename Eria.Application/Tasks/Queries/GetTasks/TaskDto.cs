using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eria.Application.Tasks.Queries.GetTasks
{
    public class TaskDto
    {
        [Display(Name="Task id")]
        public int TaskId { get; set; }
        [Display(Name = "Task name")]
        public string Name { get; set; }
        [Display(Name = "Work type id")]
        public int WorkTypeId { get; set; }
        [Display(Name = "Work type name")]
        public string WorkType { get; set; }
        [Display(Name = "From")]
        public DateTime From { get; set; }
        [Display(Name = "To")]
        public DateTime? To { get; set; }
    }
}
