using Eria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eria.Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        public static async System.Threading.Tasks.Task SeedDefaultWorkTypesAsync(AppDbContext context)
        {
            if (!context.WorkTypes.Any())
            {
                context.WorkTypes.Add(new WorkType
                {
                    WorkTypeId = 1,
                    Name = "Kódování"
                });
                context.WorkTypes.Add(new WorkType
                {
                    WorkTypeId = 2,
                    Name = "Programování"
                });
                context.WorkTypes.Add(new WorkType
                {
                    WorkTypeId = 3,
                    Name = "Meetingy"
                });

                await context.SaveChangesAsync();
            }
        }

        public static async System.Threading.Tasks.Task SeedSampleDataAsync(AppDbContext context)
        {
            if (!context.Tasks.Any())
            {
                var workTypes = context.WorkTypes.ToList();
                var rnd = new Random(DateTime.Now.Millisecond);
                context.Tasks.Add(new Domain.Entities.Task
                {
                    TaskId = 1,
                    Name = "Tvorba domácího úkolu",
                    Work = workTypes.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                    From = DateTime.Now,
                    To = DateTime.Now.AddMinutes(rnd.Next(20,400))
                });
                context.Tasks.Add(new Domain.Entities.Task
                {
                    TaskId = 2,
                    Name = "Úklid",
                    Work = workTypes.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                    From = DateTime.Now,
                    To = DateTime.Now.AddMinutes(rnd.Next(20, 400))
                });
                context.Tasks.Add(new Domain.Entities.Task
                {
                    TaskId = 3,
                    Name = "Návštěva babičky",
                    Work = workTypes.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                    From = DateTime.Now,
                    To = DateTime.Now.AddMinutes(rnd.Next(20, 400))
                });
                context.Tasks.Add(new Domain.Entities.Task
                {
                    TaskId = 4,
                    Name = "Aktuální práce",
                    Work = workTypes.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                    From = DateTime.Now
                });
                context.Tasks.Add(new Domain.Entities.Task
                {
                    TaskId = 5,
                    Name = "Aktuální práce další",
                    Work = workTypes.OrderBy(x => Guid.NewGuid()).FirstOrDefault(),
                    From = DateTime.Now.AddMinutes(-22)
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
