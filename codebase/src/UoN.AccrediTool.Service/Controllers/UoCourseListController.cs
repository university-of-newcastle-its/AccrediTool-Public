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
    [Route("api/course-lists")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoCourseListController : ControllerBase
    {
        readonly string controllerName = "CourseList";
        private readonly ILogger<UoCourseListController> _logger;
        private readonly DataContext _context;
        public IUoCourseListRepository Repository { get; set; }
        public UoCourseListController(DataContext context, ILogger<UoCourseListController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoCourseListRepository(_context);
        }

        #region GET: api/course-lists
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoCourseListModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoCourseListModel> courseLists = Repository.GetAllCourseLists();
            if (courseLists.Count > 0)
            {
                return courseLists;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/course-lists/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoCourseListModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetCourseListById(id).Result is UoCourseListModel courseList)
            {
                return courseList;
            }
            return NotFound();
        }
        #endregion

        #region POST: api/course-lists
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UoCourseListModel courseList)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (await Repository.AddCourseList(courseList).ConfigureAwait(false) is int id)
            {
                string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/course-lists/" + id.ToString(Thread.CurrentThread.CurrentCulture);
                return Created(new System.Uri(uri), courseList);
            }
            return BadRequest();
        }
        #endregion

        #region PUT: api/course-lists/{id:int}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] UoCourseListModel courseList)
        {
            string method = "Put";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.SetCourseListById(id, courseList).ConfigureAwait(false) is int count)
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

        #region PATCH: api/course-lists/{id:int}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id, [FromBody] JObject patchObject)
        {
            string method = "Patch";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.UpdateCourseListById(id, patchObject).ConfigureAwait(false) is int count)
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

        #region DELETE: api/course-lists/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteCourseListById(id).ConfigureAwait(false) is int count)
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
