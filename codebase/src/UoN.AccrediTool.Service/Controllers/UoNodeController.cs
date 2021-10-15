using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    [Route("api/nodes")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoNodeController : Controller
    {
        readonly string controllerName = "Node";
        private readonly ILogger<UoNodeController> _logger;
        private readonly DataContext _context;
        public IUoNodeRepository Repository { get; set; }
        public UoNodeController(DataContext context, ILogger<UoNodeController> logger)
        {
            _context = context;
            _logger = logger;
            Repository = new UoNodeRepository(_context);
        }

        #region GET: api/nodes
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoNodeModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoNodeModel> nodes = Repository.GetAllNodes();
            if (nodes.Count > 0)
            {
                return nodes;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/nodes/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoNodeModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetNodeById(id).Result is UoNodeModel node)
            {
                return node;
            }
            return NotFound();
        }
        #endregion

        #region GET: api/nodes/{id:int}/view?{projectId:str},{template:str},{isStatic:bool}
        [Route("{id}/view")]
        [Produces(MediaTypeNames.Text.Html)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<IActionResult> GetView(int id, string projectId, string template = "Universal", bool isStatic = false)
        {
            string method = "GetView";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, Template: {4}, IsStatic: {5}, User: {6}", traceId, controllerName, method, id, template, isStatic, User.Identity.Name);

            if (Get(id).Value is UoNodeModel node)
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
                int? frameworkId = await UoServiceControllers.GetTopLevelFrameworkId(node, HttpContext).ConfigureAwait(false);
                if (frameworkId is int fwkId)
                {
                    var frameworkRepository = new UoFrameworkRepository(_context);
                    ViewData["Framework"] = await frameworkRepository.GetFrameworkById(fwkId).ConfigureAwait(false);
                }
                List<UoLevelCategoryModel> levelCategories = node.LevelCategoryNodes.Select(levelCategoryNode => levelCategoryNode.LevelCategory).ToList();
                ViewData["LevelCategories"] = await UoServiceControllers.GetMappedLevelCategories(levelCategories, HttpContext).ConfigureAwait(false);
                return View("~/Views/Node.cshtml", node);
            }
            return NotFound();
        }
        #endregion

        #region POST: api/nodes
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UoNodeModel node)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (await Repository.AddNode(node).ConfigureAwait(false) is int id)
            {
                string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/nodes/" + id.ToString(Thread.CurrentThread.CurrentCulture);
                return Created(new System.Uri(uri), node);
            }
            return BadRequest();
        }
        #endregion

        #region PUT: api/nodes/{id:int}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] UoNodeModel node)
        {
            string method = "Put";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.SetNodeById(id, node).ConfigureAwait(false) is int count)
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

        #region PATCH: api/nodes/{id:int}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id, [FromBody] JObject patchObject)
        {
            string method = "Patch";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.UpdateNodeById(id, patchObject).ConfigureAwait(false) is int count)
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

        #region DELETE: api/nodes/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteNodeById(id).ConfigureAwait(false) is int count)
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
