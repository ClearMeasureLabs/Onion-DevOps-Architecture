using System.Collections.Generic;
using System.Configuration;
using ClearMeasure.OnionDevOpsArchitecture.Core;
using ClearMeasure.OnionDevOpsArchitecture.Core.Features.BrowseExpenseReports;
using ClearMeasure.OnionDevOpsArchitecture.Core.Model;
using Microsoft.AspNetCore.Http;
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
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var command = new ListExpenseReportsCommand();
            ExpenseReport[] reports = _bus.Send(command);
            reports[0].Description = connectionString;
            return reports;
        }
    }
}