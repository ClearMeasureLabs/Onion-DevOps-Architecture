using System.Collections.Generic;
using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Features.BrowseExpenseReports;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace ClearMeasure.OnionDevOpsArchitecture.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private Bus _bus;

        public ValuesController(Bus bus)
        {
            _bus = bus;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ExpenseReport>> Get()
        {
            var command = new ListExpenseReportsCommand();
            ExpenseReport[] reports = _bus.Send(command);
            return reports;
        }
    }
}