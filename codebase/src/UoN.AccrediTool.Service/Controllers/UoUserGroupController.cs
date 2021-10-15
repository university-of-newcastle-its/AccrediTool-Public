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
    // [Authorize(Roles = "Restricted-Access")]
    [ApiController]
    [Route("api/user-groups")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoUserGroupController : ControllerBase
    {
        readonly string controllerName = "UserGroup";
        private readonly ILogger<UoUserGroupController> _logger;
        private readonly DataContext _context;
        public IUoUserGroupRepository Repository { get; set; }
        public UoUserGroupController(DataContext context, ILogger<UoUserGroupController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoUserGroupRepository(_context);
        }

        #region GET: api/user-groups
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoUserGroupModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoUserGroupModel> userGroups = Repository.GetAllUserGroups();
            if (userGroups.Count > 0)
            {
                return userGroups;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/user-groups/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoUserGroupModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetUserGroupById(id).Result is UoUserGroupModel userGroup)
            {
                return userGroup;
            }
            return NotFound();
        }
        #endregion

        #region POST: api/user-groups
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UoUserGroupModel userGroup)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (await Repository.AddUserGroup(userGroup).ConfigureAwait(false) is int id)
            {
                string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/user-groups/" + id.ToString(Thread.CurrentThread.CurrentCulture);
                return Created(new System.Uri(uri), userGroup);
            }
            return BadRequest();
        }
        #endregion

        #region PATCH: api/user-groups/{id:int}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id, [FromBody] JObject patchObject)
        {
            string method = "Patch";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.UpdateUserGroupById(id, patchObject).ConfigureAwait(false) is int count)
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

        #region DELETE: api/user-groups/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteUserGroupById(id).ConfigureAwait(false) is int count)
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
