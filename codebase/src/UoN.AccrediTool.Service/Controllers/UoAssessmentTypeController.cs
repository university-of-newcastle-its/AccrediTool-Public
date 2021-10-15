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
    [Route("api/assessment-types")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoAssessmentTypeController : ControllerBase
    {
        readonly string controllerName = "AssessmentType";
        private readonly ILogger<UoAssessmentTypeController> _logger;
        private readonly DataContext _context;
        public IUoAssessmentTypeRepository Repository { get; set; }
        public UoAssessmentTypeController(DataContext context, ILogger<UoAssessmentTypeController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoAssessmentTypeRepository(_context);
        }

        #region GET: api/assessment-types
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoAssessmentTypeModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoAssessmentTypeModel> assessmentTypes = Repository.GetAllAssessmentTypes();
            if (assessmentTypes.Count > 0)
            {
                return assessmentTypes;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/assessment-types/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoAssessmentTypeModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetAssessmentTypeById(id).Result is UoAssessmentTypeModel assessmentType)
            {
                return assessmentType;
            }
            return NotFound();
        }
        #endregion
    }
}
