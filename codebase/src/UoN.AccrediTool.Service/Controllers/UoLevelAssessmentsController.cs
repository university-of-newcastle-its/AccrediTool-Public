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
    [Route("api/level-assessments")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoLevelAssessmentsController : ControllerBase
    {
        readonly string controllerName = "LevelAssessments";
        private readonly ILogger<UoLevelAssessmentsController> _logger;
        private readonly DataContext _context;
        public IUoLevelAssessmentsRepository Repository { get; set; }
        public UoLevelAssessmentsController(DataContext context, ILogger<UoLevelAssessmentsController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoLevelAssessmentsRepository(_context);
        }

        #region POST: api/level-assessments
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] UoLevelAssessmentsJoin levelAssessments)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (levelAssessments != null)
            {
                if (await Repository.AddLevelAssessments(levelAssessments).ConfigureAwait(false) is int id)
                {
                    if (id > 0)
                    {
                        string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/level-assessments/" + id;
                        return Created(new System.Uri(uri), levelAssessments);
                    }
                    return Conflict();
                }
            }
            return BadRequest();
        }
        #endregion

        #region DELETE: api/level-assessments/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteLevelAssessmentsById(id).ConfigureAwait(false) is int count)
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
