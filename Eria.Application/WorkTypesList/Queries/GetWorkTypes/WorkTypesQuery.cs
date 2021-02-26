using Eria.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eria.Application.WorkTypesList.Queries.GetWorkTypes
{
    public class WorkTypesQuery : IRequest<WorkTypesVM>
    {
    }
    public class GetWorkTypesQueryHandler : IRequestHandler<WorkTypesQuery, WorkTypesVM>
    {
        private readonly IAppDbContext _context;

        public GetWorkTypesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<WorkTypesVM> Handle(WorkTypesQuery request, CancellationToken cancellationToken)
        {
            return new WorkTypesVM
            {
                WorkTypes = await _context.WorkTypes
                    .AsNoTracking()
                    .Select(x => new WorkTypeDto
                    {
                        Name = x.Name,
                        WorkTypeId = x.WorkTypeId
                    })
                    .OrderBy(t => t.WorkTypeId)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
