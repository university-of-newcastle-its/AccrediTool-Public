using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;


using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Repositories;
using UoN.AccrediTool.Service.Models;

//JSON.net
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UoN.AccrediTool.Service.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/projects")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class UoProjectController : ControllerBase
    {
        readonly string controllerName = "Project";
        private readonly ILogger<UoProjectController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        public IUoProjectRepository Repository { get; set; }
        public UoProjectController(DataContext context, ILogger<UoProjectController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            Repository = new UoProjectRepository(_context);
            _configuration = configuration;
        }

        #region GET: api/projects
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UoProjectModel>> Get()
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            List<UoProjectModel> projects = Repository.GetAllProjects();
            if (projects.Count > 0)
            {
                return projects;
            }
            return NoContent();
        }
        #endregion

        #region GET: api/projects/{id:int}
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UoProjectModel> Get(int id)
        {
            string method = "Get";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Repository.GetProjectById(id).Result is UoProjectModel project)
            {
                return project;
            }
            return NotFound();
        }
        #endregion

        #region GET: api/projects/{id:int}/export
        [HttpGet("{id}/export")]
        [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Zip)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExport(int id, bool download = false)
        {
            string method = "GetExport";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (Get(id).Value is UoProjectModel project)
            {
                var jsonResult = new JObject();
                string host = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString();
                string urlParams = "/view?projectId=" + project.ProjectId + "&isStatic=true";

                // TODO: Change this to a for loop when enabling multiple frameworks in one project
                if (project.Framework is UoFrameworkModel framework)
                {
                    if (framework.TemplateName != null)
                        urlParams = string.Format(CultureInfo.CurrentCulture, "/view?projectId={0}&template={1}&isStatic=true", project.ProjectId, framework.TemplateName);

                    jsonResult.Add("framework-" + framework.FrameworkId + ".aspx", "/api/frameworks/" + framework.FrameworkId + urlParams);
                    void addChildNodes(List<UoNodeModel> nodes)
                    {
                        foreach (UoNodeModel node in nodes)
                        {
                            jsonResult.Add("node-" + node.NodeId + ".aspx", "/api/nodes/" + node.NodeId + urlParams);
                            addChildNodes(node.ChildNodes);
                        }
                    }
                    addChildNodes(framework.Nodes);
                }
               
                var programRepository = new UoProgramRepository(_context);
                var courseRepository = new UoCourseRepository(_context);
                foreach (UoProjectProgramsJoin projectPrograms in project.ProjectPrograms)
                {
                    UoProgramModel program = await programRepository.GetProgramById(projectPrograms.ProgramId).ConfigureAwait(false);
                    jsonResult.Add("program-" + program.ProgramId + ".aspx", "/api/programs/" + program.ProgramId + urlParams);
                    jsonResult.Add("mapping-" + program.ProgramId + ".aspx", "/api/programs/" + program.ProgramId + "/mapping" + urlParams);
                    foreach (UoCourseListModel courseList in program.ProgramStructure)
                    {
                        foreach (UoCourseListCoursesJoin courseListCourses in courseList.CourseListCourses)
                        {
                            UoCourseModel course = await courseRepository.GetCourseById(courseListCourses.CourseId).ConfigureAwait(false);
                            jsonResult.Add("course-" + course.CourseId + ".aspx", "/api/courses/" + course.CourseId + urlParams);
                            foreach (UoCourseInstanceModel courseInstance in course.CourseInstances)
                            {
                                jsonResult.Add(
                                    "course-instance-" + courseInstance.CourseInstanceId + ".aspx",
                                    "/api/course-instances/" + courseInstance.CourseInstanceId + urlParams
                                );
                            }
                        }
                    }
                }

                if (download)
                {
                    var webClient = new WebClient();

                    UoLoginModel loginModel = new();

                    loginModel.Username = "service-user";
                    loginModel.Secret = _configuration["Security:UserMappings:" + loginModel.Username + ":Secret"];

                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    var response = webClient.UploadString(new Uri (host + "/api/security/login/"), JsonConvert.SerializeObject(loginModel).ToString());

                    webClient.Headers[HttpRequestHeader.ContentType] = null;
                    webClient.Headers["Authorization"] = "Bearer " + JsonConvert.DeserializeObject<JObject>(response).First.First.ToString();

                    if (Directory.Exists("./Export") == false)
                    {
                        Directory.CreateDirectory("./Export");
                    }

                    string folderPath = "./Export/" + System.Guid.NewGuid().ToString();
                    var directory = Directory.CreateDirectory(folderPath);
                    string payloadPath = folderPath + "/payload.zip";

                    webClient.DownloadFile(host + "/api/static/payload.zip", payloadPath);
                    ZipFile.ExtractToDirectory(payloadPath, folderPath + "/static", true);
                    System.IO.File.Delete(payloadPath);

                    foreach (var kvPair in jsonResult)
                    {
                        string fileName = kvPair.Key;
                        string endpoint = (string)kvPair.Value;
                        webClient.DownloadFile(host + endpoint, folderPath + "/" + fileName);
                    }

                    ZipFile.CreateFromDirectory(folderPath, folderPath + ".zip");
                    directory.Delete(true);
                    try
                    {
                        return File(System.IO.File.ReadAllBytes(folderPath + ".zip"), MediaTypeNames.Application.Zip, "project-" + id + ".zip");
                    }
                    finally
                    {
                        webClient.Dispose();
                        System.IO.File.Delete(folderPath + ".zip");
                    }
                }
                else
                {
                    return Ok(jsonResult);
                }
            }
            return NotFound();
        }
        #endregion

        #region POST: api/projects
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UoProjectModel project)
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            if (await Repository.AddProject(project).ConfigureAwait(false) is int id)
            {
                string uri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString() + "/api/projects/" + id.ToString(Thread.CurrentThread.CurrentCulture);
                return Created(new System.Uri(uri), project);
            }
            return BadRequest();
        }
        #endregion

        #region PUT: api/projects/{id:int}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] UoProjectModel project)
        {
            string method = "Put";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.SetProjectById(id, project).ConfigureAwait(false) is int count)
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

        #region PATCH: api/projects/{id:int}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(int id, [FromBody] JObject patchObject)
        {
            string method = "Patch";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.UpdateProjectById(id, patchObject).ConfigureAwait(false) is int count)
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

        #region DELETE: api/projects/{id:int}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            string method = "Delete";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, Id: {3}, User: {4}", traceId, controllerName, method, id, User.Identity.Name);

            if (await Repository.DeleteProjectById(id).ConfigureAwait(false) is int count)
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
