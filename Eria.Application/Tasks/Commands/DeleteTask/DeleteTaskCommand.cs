using Eria.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<int>
    {
        public int TaskId { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, int>
    {
        private readonly IAppDbContext _context;

        public DeleteTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.TaskId == request.TaskId);
            if (task != null)
            {
                _context.Tasks.Attach(task);
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return task.TaskId;
        }
    }
}
