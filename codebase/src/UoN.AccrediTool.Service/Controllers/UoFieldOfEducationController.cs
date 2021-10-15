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
    [Route("api/fields-of-education")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoFieldOfEducationController : ControllerBase
    {
        readonly string controllerName = "FieldOfEducation";
        private readonly ILogger<UoFieldOfEducationController> _logger;
        private readonly DataContext _context;
        public IUoFieldOfEducationRepository Repository { get; set; }
        public UoFieldOfEducationController(DataContext context, ILogger<UoFieldOfEducationController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoFieldOfEducationRepository(_context);
        }

        #region GET: api/fields-of-education
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoFieldOfEducationModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoFieldOfEducationModel> fieldsOfEducation = Repository.GetAllFieldsOfEducation();
            if (fieldsOfEducation.Count > 0)
            {
                return fieldsOfEducation;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/fields-of-education/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoFieldOfEducationModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetFieldOfEducationById(id).Result is UoFieldOfEducationModel fieldOfEducation)
            {
                return fieldOfEducation;
            }
            return NotFound();
        }
        #endregion
    }
}
