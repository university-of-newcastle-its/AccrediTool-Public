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
    [Route("api/academic-orgs")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoAcademicOrgController : ControllerBase
    {
        readonly string controllerName = "AcademicOrg";
        private readonly ILogger<UoAcademicOrgController> _logger;
        private readonly DataContext _context;
        public IUoAcademicOrgRepository Repository { get; set; }
        public UoAcademicOrgController(DataContext context, ILogger<UoAcademicOrgController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoAcademicOrgRepository(_context);
        }

        #region GET: api/academic-orgs
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoAcademicOrgModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoAcademicOrgModel> academicOrgs = Repository.GetAllAcademicOrgs();
            if (academicOrgs.Count > 0)
            {
                return academicOrgs;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/academic-orgs/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoAcademicOrgModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetAcademicOrgById(id).Result is UoAcademicOrgModel academicOrg)
            {
                return academicOrg;
            }
            return NotFound();
        }
        #endregion

        #region POST: api/academic-orgs
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UoAcademicOrgModel academicOrg)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (await Repository.AddAcademicOrg(academicOrg).ConfigureAwait(false) is int id)
            {
                string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/academic-orgs/" + id.ToString(Thread.CurrentThread.CurrentCulture);
                return Created(new System.Uri(uri), academicOrg);
            }
            return BadRequest();
        }
        #endregion
    }
}
