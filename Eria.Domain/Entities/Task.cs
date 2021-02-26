using System;

namespace Eria.Domain.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public WorkType Work { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
