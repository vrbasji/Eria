using Eria.Application.WorkTypesList.Commands.CreateWorkType;
using Eria.Application.WorkTypesList.Queries.GetWorkType;
using Eria.Application.WorkTypesList.Queries.GetWorkTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eria.WebUI.Controllers
{
    public class WorkTypeController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<WorkTypesVM>> WorkTypesList()
        {
            var result = await Mediator.Send(new WorkTypesQuery());
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult<WorkTypeVM>> WorkTypeDetail(int id)
        {
            var result = await Mediator.Send(new WorkTypeDetailQuery() { WorkTypeId = id });
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> CreateWorkType()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<CreateWorkTypeCommand>> CreateWorkType(CreateWorkTypeCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("WorkTypesList");
        }

    }
}
