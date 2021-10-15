using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Repositories;

namespace UoN.AccrediTool.Service.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/program-careers")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoProgramCareerController : ControllerBase
    {
        readonly string controllerName = "ProgramCareer";
        private readonly ILogger<UoProgramCareerController> _logger;
        private readonly DataContext _context;
        public IUoProgramCareerRepository Repository { get; set; }
        public UoProgramCareerController(DataContext context, ILogger<UoProgramCareerController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoProgramCareerRepository(_context);
        }

        #region GET: api/program-careers
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoProgramCareerModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoProgramCareerModel> programCareers = Repository.GetAllProgramCareers();
            if (programCareers.Count > 0)
            {
                return programCareers;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/program-careers/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoProgramCareerModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetProgramCareerById(id).Result is UoProgramCareerModel programCareer)
            {
                return programCareer;
            }
            return NotFound();
        }
        #endregion
    }
}
