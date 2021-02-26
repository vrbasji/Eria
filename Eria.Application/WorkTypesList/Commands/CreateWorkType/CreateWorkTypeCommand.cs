using Eria.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.WorkTypesList.Commands.CreateWorkType
{
    public class CreateWorkTypeCommand : IRequest<int>
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class CreateWorkTypeCommandHandler : IRequestHandler<CreateWorkTypeCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateWorkTypeCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateWorkTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Eria.Domain.Entities.WorkType
            {
                Name = request.Name
            };

            _context.WorkTypes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.WorkTypeId;
        }
    }
}
