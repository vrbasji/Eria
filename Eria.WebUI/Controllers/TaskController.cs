using Eria.Application.Tasks.Commands.DeleteTask;
using Eria.Application.Tasks.Commands.EditTask;
using Eria.Application.Tasks.Commands.StopTask;
using Eria.Application.Tasks.Queries.GetTask;
using Eria.Application.WorkTypesList.Queries.GetWorkTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Eria.WebUI.Controllers
{
    public class TaskController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<TaskVM>> EditTask(int id)
        {
            var result = await Mediator.Send(new TaskQuery() { TaskId = id });
            var workTypes = await Mediator.Send(new WorkTypesQuery());
            TempData["WorkTypes"] = workTypes.WorkTypes;
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<TaskVM>> EditTask(TaskVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(new EditTaskCommand()
                {
                    From = model.From,
                    Name = model.Name,
                    To = model.To.Value,
                    SelectedWorkTypeId = model.WorkTypeId,
                    TaskId = model.TaskId
                });
                return RedirectToAction("Index", "Home");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTask(int id)
        {
            await Mediator.Send(new DeleteTaskCommand() { TaskId = id });
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> StopTask(int id)
        {
            await Mediator.Send(new StopTaskCommand() { TaskId = id, StopTime = DateTime.Now });
            return RedirectToAction("Index", "Home");
        }
    }
}
