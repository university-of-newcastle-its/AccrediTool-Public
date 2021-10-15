using System.Diagnostics;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Repositories;

namespace UoN.AccrediTool.Service.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/course-list-courses")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoCourseListCoursesController : ControllerBase
    {
        readonly string controllerName = "CourseListCourses";
        private readonly ILogger<UoCourseListCoursesController> _logger;
        private readonly DataContext _context;
        public IUoCourseListCoursesRepository Repository { get; set; }
        public UoCourseListCoursesController(DataContext context, ILogger<UoCourseListCoursesController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoCourseListCoursesRepository(_context);
        }

        #region POST: api/course-list-courses
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] UoCourseListCoursesJoin courseListCourses)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (courseListCourses != null)
            {
                if (await Repository.AddCourseListCourses(courseListCourses).ConfigureAwait(false) is int id)
                {
                    if (id > 0)
                    {
                        string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/course-list-courses/" + id;
                        return Created(new System.Uri(uri), courseListCourses);
                    }
                    return Conflict();
                }
            }
            return BadRequest();
        }
        #endregion

        #region DELETE: api/course-list-courses/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteCourseListCoursesById(id).ConfigureAwait(false) is int count)
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
