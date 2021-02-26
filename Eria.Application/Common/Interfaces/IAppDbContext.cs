using Eria.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Eria.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Task> Tasks { get; set; }
        DbSet<WorkType> WorkTypes { get; set; }

        System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
