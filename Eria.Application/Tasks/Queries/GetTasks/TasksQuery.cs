using Eria.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.Tasks.Queries.GetTasks
{
    public class TasksQuery : IRequest<TasksVM>
    {
        public string SortOrder { get; set; }
        public string Filter { get; set; }
    }
    public class GetWorkTypesQueryHandler : IRequestHandler<TasksQuery, TasksVM>
    {
        private readonly IAppDbContext _context;

        public GetWorkTypesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TasksVM> Handle(TasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _context.Tasks
                    .AsNoTracking()
                    .Select(x => new TaskDto
                    {
                        TaskId = x.TaskId,
                        Name = x.Name,
                        From = x.From,
                        To = x.To,
                        WorkType = x.Work.Name,
                        WorkTypeId = x.Work.WorkTypeId
                    })
                    .OrderBy(t => t.From)
                    .ToListAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.Filter))
            {
                tasks = tasks.Where(s => s.From.ToString("MM/dd/yyyy HH:mm:ss").Contains(request.Filter) 
                || s.To.GetValueOrDefault().ToString("MM/dd/yyyy HH:mm:ss").Contains(request.Filter)
                                       || s.WorkType.Contains(request.Filter)).ToList();
            }

            switch (request.SortOrder)
            {
                case nameof(TaskDto.TaskId):
                    tasks = tasks.OrderBy(s => s.TaskId).ToList();
                    break;
                case nameof(TaskDto.Name):
                    tasks = tasks.OrderBy(s => s.Name).ToList();
                    break;
                case nameof(TaskDto.From):
                    tasks = tasks.OrderBy(s => s.From).ToList();
                    break;
                case nameof(TaskDto.To):
                    tasks = tasks.OrderBy(s => s.To).ToList();
                    break;
                case nameof(TaskDto.WorkType):
                    tasks = tasks.OrderBy(s => s.WorkType).ToList();
                    break;
                case nameof(TaskDto.WorkTypeId):
                    tasks = tasks.OrderBy(s => s.WorkTypeId).ToList();
                    break;
                default:
                    tasks = tasks.OrderBy(s => s.TaskId).ToList();
                    break;
            }
            return new TasksVM
            {
                Tasks = tasks
            };
        }
    }
}
