using Eria.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.WorkTypesList.Queries.GetWorkType
{
    public class WorkTypeDetailQuery : IRequest<WorkTypeVM>
    {
        public int WorkTypeId { get; set; }
    }
    public class GetWorkTypesQueryHandler : IRequestHandler<WorkTypeDetailQuery, WorkTypeVM>
    {
        private readonly IAppDbContext _context;

        public GetWorkTypesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<WorkTypeVM> Handle(WorkTypeDetailQuery request, CancellationToken cancellationToken)
        {
            var workType = await _context.WorkTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.WorkTypeId == request.WorkTypeId);
            return new WorkTypeVM
            {
                WorkTypeId = workType.WorkTypeId,
                Name = workType.Name
            };
        }
    }
}
