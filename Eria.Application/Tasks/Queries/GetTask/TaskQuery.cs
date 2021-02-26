using Eria.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.Tasks.Queries.GetTask
{
    public class TaskQuery : IRequest<TaskVM>
    {
        public int TaskId { get; set; }
    }
    public class GetTaskQueryHandler : IRequestHandler<TaskQuery, TaskVM>
    {
        private readonly IAppDbContext _context;

        public GetTaskQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskVM> Handle(TaskQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                    .AsNoTracking()
                    .Include("Work")
                    .FirstOrDefaultAsync(x => x.TaskId == request.TaskId);
            if (task != null)
            {
                return new TaskVM
                {
                    TaskId = task.TaskId,
                    Name = task.Name,
                    From = task.From,
                    To = task.To,
                    WorkType = task.Work.Name,
                    WorkTypeId = task.Work.WorkTypeId
                };
            }
            return null;
        }
    }
}
