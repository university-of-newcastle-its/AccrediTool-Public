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
    [Route("api/course-instances")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoCourseInstanceController : Controller
    {
        readonly string controllerName = "CourseInstance";
        private readonly ILogger<UoCourseInstanceController> _logger;
        private readonly DataContext _context;
        public IUoCourseInstanceRepository Repository { get; set; }
        public UoCourseInstanceController(DataContext context, ILogger<UoCourseInstanceController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoCourseInstanceRepository(_context);
        }

        #region GET: api/course-instances
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoCourseInstanceModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoCourseInstanceModel> courseInstances = Repository.GetAllCourseInstances();
            if (courseInstances.Count > 0)
            {
                return courseInstances;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/course-instances/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoCourseInstanceModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetCourseInstanceById(id).Result is UoCourseInstanceModel courseInstance)
            {
                return courseInstance;
            }
            return NotFound();
        }
        #endregion

        #region GET: api/course-instances/{id:int}/view?{projectId:str},{template:str},{isStatic:bool}
        [Route("{id}/view")]
        [Produces(MediaTypeNames.Text.Html, MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public IActionResult GetView(int id, string projectId, string template = "Universal", bool isStatic = false)
        {
            string method = "GetView";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, Template: {4}, IsStatic: {5}, User: {6}", traceId, controllerName, method, id, template, isStatic, User.Identity.Name);

            if (Get(id).Value is UoCourseInstanceModel courseInstance)
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
                return View("~/Views/CourseInstance.cshtml", courseInstance);
            }
            return NotFound();
        }
        #endregion

        #region POST: api/course-instances
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UoCourseInstanceModel courseInstance)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (await Repository.AddCourseInstance(courseInstance).ConfigureAwait(false) is int id)
            {
                string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/course-instances/" + id.ToString(Thread.CurrentThread.CurrentCulture);
                return Created(new System.Uri(uri), courseInstance);
            }
            return BadRequest();
        }
        #endregion

        #region PUT: api/course-instances/{id:int}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] UoCourseInstanceModel courseInstance)
        {
            string method = "Put";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.SetCourseInstanceById(id, courseInstance).ConfigureAwait(false) is int count)
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

        #region PATCH: api/course-instances/{id:int}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id, [FromBody] JObject patchObject)
        {
            string method = "Patch";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.UpdateCourseInstanceById(id, patchObject).ConfigureAwait(false) is int count)
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

        #region DELETE: api/course-instances/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteCourseInstanceById(id).ConfigureAwait(false) is int count)
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
