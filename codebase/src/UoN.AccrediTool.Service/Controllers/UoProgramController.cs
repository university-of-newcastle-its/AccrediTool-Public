using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Text.RegularExpressions;
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
    [Route("api/programs")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoProgramController : Controller
    {
        readonly string controllerName = "Program";
        private readonly ILogger<UoProgramController> _logger;
        private readonly DataContext _context;
        public UoProgramRepository Repository { get; set; }
        public UoProgramController(DataContext context, ILogger<UoProgramController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoProgramRepository(_context);
        }

        #region GET: api/programs
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoProgramModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoProgramModel> programs = Repository.GetAllPrograms();
            if (programs.Count > 0)
            {
                return programs;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/programs/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoProgramModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetProgramById(id).Result is UoProgramModel program)
            {
                return program;
            }
            return NotFound();
        }
        #endregion

        #region GET: api/programs/{id:int}/view?{projectId:str},{template:str},{isStatic:bool}
        [Route("{id}/view")]
        [Produces(MediaTypeNames.Text.Html, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public IActionResult GetView(int id, string projectId, string template = "Universal", bool isStatic = false)
        {
            string method = "GetView";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, Template: {4}, User: {5}", traceId, controllerName, method, id, template, User.Identity.Name);

            if (Get(id).Value is UoProgramModel program)
            {
                if (int.TryParse(projectId, out int project))
                {
                    ViewData["ProjectId"] = project;
                }
                if (template != "Universal" && !UoServiceControllers.validTemplates.Contains(template))
                {
                    return BadRequest();
                }
                ViewData["Template"] = template;
                ViewData["IsStatic"] = isStatic;
                return View("~/Views/Program.cshtml", program);
            }
            return NotFound();
        }
        #endregion

        #region GET: api/programs/{id:int}/mapping
        [HttpGet("{id}/mapping")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoProgramModel> GetMapping(int id)
        {
            string method = "GetMapping";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetProgramMappingById(id).Result is UoProgramModel program)
            {
                return program;
            }
            return NotFound();
        }
        #endregion

        #region GET: api/programs/{id:int}/mapping/view?{projectId:str},{template:str},{isStatic:bool}
        [Route("{id}/mapping/view")]
        [Produces(MediaTypeNames.Text.Html, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public IActionResult GetMappingView(int id, string projectId, string template = "Universal", bool isStatic = false)
        {
            string method = "GetMappingView";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, Template: {4}, IsStatic: {5}, User: {6}", traceId, controllerName, method, id, template, isStatic, User.Identity.Name);

            if (GetMapping(id).Value is UoProgramModel program)
            {
                if (int.TryParse(projectId, out int project))
                {
                    ViewData["ProjectId"] = project;
                    if(Repository.GetProjectById(project).Result is UoProjectModel projectModel)
                    {
                        ViewData["Project"] = projectModel;
                    }
                    
                }
                if (template != "Universal" && !UoServiceControllers.validTemplates.Contains(template))
                {
                    return BadRequest();
                }
                ViewData["Template"] = template;
                ViewData["IsStatic"] = isStatic;
                return View("~/Views/ProgramMapping.cshtml", program);
            }
            return NotFound();
        }
        #endregion

        #region POST: api/programs
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UoProgramModel program)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (await Repository.AddProgram(program).ConfigureAwait(false) is int id)
            {
                string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/programs/" + id.ToString(Thread.CurrentThread.CurrentCulture);
                return Created(new System.Uri(uri), program);
            }
            return BadRequest();
        }
        #endregion

        #region PUT: api/programs/{id:int}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] UoProgramModel program)
        {
            string method = "Put";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.SetProgramById(id, program).ConfigureAwait(false) is int count)
            {
                if (count > 0)
                {
                    return NoContent();
                }
                return BadRequest();
            }
            return NotFound();
        }
        #endregion

        #region PATCH: api/programs/{id:int}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id, [FromBody] JObject patchObject)
        {
            string method = "Patch";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.UpdateProgramById(id, patchObject).ConfigureAwait(false) is int count)
            {
                if (count > 0)
                {
                    return NoContent();
                }
                return BadRequest();
            }
            return NotFound();
        }
        #endregion

        #region DELETE: api/programs/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteProgramById(id).ConfigureAwait(false) is int count)
            {
                if (count > 0)
                {
                    return NoContent();
                }
                return BadRequest();
            }
            return NotFound();
        }
        #endregion
    }
}
