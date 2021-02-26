using Eria.Application.Tasks.Commands.CreateTask;
using Eria.Application.Tasks.Commands.DeleteTask;
using Eria.Application.Tasks.Commands.EditTask;
using Eria.Application.Tasks.Commands.StopTask;
using Eria.Application.Tasks.Queries.GetTask;
using Eria.Application.Tasks.Queries.GetTasks;
using Eria.Application.WorkTypesList.Commands.CreateWorkType;
using Eria.Application.WorkTypesList.Queries.GetWorkType;
using Eria.Application.WorkTypesList.Queries.GetWorkTypes;
using Eria.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Eria.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<WorkTypesVM>> Index(string sortOrder, string searchString)
        {
            var workTypes = await Mediator.Send(new WorkTypesQuery());
            ViewData["CurrentFilter"] = searchString;
            var createTaskCommand = new CreateTaskCommand()
            {
                From = DateTime.Now,
                To = DateTime.Now,
                WorkTypes = workTypes.WorkTypes.Select(x => new WorkTypeVM
                {
                    Name = x.Name,
                    WorkTypeId = x.WorkTypeId
                }).ToList()
            };
            TempData["WorkTypes"] = createTaskCommand;
            var result = await Mediator.Send(new TasksQuery() { SortOrder = sortOrder, Filter = searchString });
            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult> Index(CreateTaskCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
