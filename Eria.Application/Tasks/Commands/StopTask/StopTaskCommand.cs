using Eria.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.Tasks.Commands.StopTask
{
    public class StopTaskCommand : IRequest<int>
    {
        public int TaskId { get; set; }
        public DateTime StopTime { get; set; }
    }

    public class StopTaskCommandHandler : IRequestHandler<StopTaskCommand, int>
    {
        private readonly IAppDbContext _context;

        public StopTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(StopTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.TaskId == request.TaskId);
            if (task != null)
            {
                task.To = request.StopTime;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return task.TaskId;
        }
    }
}
