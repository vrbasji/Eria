using Eria.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.Tasks.Commands.EditTask
{
    public class EditTaskCommand : IRequest<int>
    {
        [Display(Name = "Task id")]
        public int TaskId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "From")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss}")]
        public DateTime From { get; set; }
        [Display(Name = "To")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss}")]
        public DateTime To { get; set; }
        public int SelectedWorkTypeId { get; set; }
    }

    public class EditTaskCommandHandler : IRequestHandler<EditTaskCommand, int>
    {
        private readonly IAppDbContext _context;

        public EditTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(EditTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks.FindAsync(request.TaskId);

            if (entity == null)
            {
                throw new NullReferenceException(nameof(Domain.Entities.Task));
            }

            var workType = await _context.WorkTypes.FindAsync(request.SelectedWorkTypeId);

            entity.Name = request.Name;
            entity.From = request.From;
            entity.To = request.To;
            entity.Work = workType;

            await _context.SaveChangesAsync(cancellationToken);

            return entity.TaskId;
        }
    }
}
