using System;
using System.Collections.Generic;
using System.Text;

namespace Eria.Application.Tasks.Queries.GetTask
{
    public class TaskVM
    {
        public int TaskId { get; set; }
        public int WorkTypeId { get; set; }
        public string WorkType { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
