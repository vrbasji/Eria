using Eria.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eria.Application.Tasks.Queries.GetTasks
{
    public class TasksVM
    {
        public IList<TaskDto> Tasks { get; set; }
    }
}
