using Eria.Application.Common.Interfaces;
using Eria.Application.WorkTypesList.Queries.GetWorkType;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.Tasks.Commands.CreateTask
{

    public class CreateTaskCommand : IRequest<int>
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss}")]
        public DateTime From { get; set; }
        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss}")]
        public DateTime To { get; set; }
        public int SelectedWorkTypeId { get; set; }
        [Display(Name = "Work types")]
        public List<WorkTypeVM> WorkTypes { get; set; }
        [Display(Name = "Stopwatch")]
        public bool StopWatch { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var workType = _context.WorkTypes.FirstOrDefault(x => x.WorkTypeId == request.SelectedWorkTypeId);
            var entity = new Eria.Domain.Entities.Task
            {
                Name = request.Name,
                From = request.From,
                Work = workType
            };
            if (!request.StopWatch)
                entity.To = request.To;
            else
                entity.To = null;

            _context.Tasks.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.TaskId;
        }
    }
}
