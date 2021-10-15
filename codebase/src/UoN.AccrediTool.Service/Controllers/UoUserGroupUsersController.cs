using System.Diagnostics;
using System.Net.Mime;
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
    [Route("api/user-group-users")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoUserGroupUsersController : ControllerBase
    {
        readonly string controllerName = "UserGroupUsers";
        private readonly ILogger<UoUserGroupUsersController> _logger;
        private readonly DataContext _context;
        public IUoUserGroupUsersRepository Repository { get; set; }
        public UoUserGroupUsersController(DataContext context, ILogger<UoUserGroupUsersController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoUserGroupUsersRepository(_context);
        }

        #region POST: api/user-group-users
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] UoUserGroupUsersJoin userGroupUsers)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (userGroupUsers != null)
            {
                if (await Repository.AddUserGroupUsers(userGroupUsers).ConfigureAwait(false) is int id)
                {
                    if (id > 0)
                    {
                        string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/user-group-users/" + id;
                        return Created(new System.Uri(uri), userGroupUsers);
                    }
                    return Conflict();
                }
            }
            return BadRequest();
        }
        #endregion

        #region DELETE: api/user-group-users/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteUserGroupUsersById(id).ConfigureAwait(false) is int count)
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
