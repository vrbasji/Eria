using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Eria.Domain.Entities;
using Eria.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace Eria.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
