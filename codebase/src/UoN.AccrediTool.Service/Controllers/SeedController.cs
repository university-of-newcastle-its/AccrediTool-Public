using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Service.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public class SeedController : ControllerBase
    {
        readonly string controllerName = "Seed";
        private readonly ILogger<SeedController> _logger;
        public SeedController(ILogger<SeedController> logger)
        {
            _logger = logger;
        }

        private static async Task<HttpResponseMessage> PostToService(HttpClient client, System.Uri uri, object payload)
        {
            string content = JsonConvert.SerializeObject(payload);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, byteContent).ConfigureAwait(false);
            byteContent.Dispose();
            return response;
        }

        #region POST: api/seed
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post()
        {
            string method = "Post";
            string traceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            _logger.LogInformation("{0} - Web API - Controller: {1}, Method: {2}, User: {3}", traceId, controllerName, method, User.Identity.Name);

            HttpClient client = new HttpClient();
            string baseUri = "http" + (HttpContext.Request.IsHttps ? "s" : "") + "://" + HttpContext.Request.Host.ToString();
            HttpResponseMessage seedResponse = await client.GetAsync(new System.Uri(baseUri + "/api/static/seed.json")).ConfigureAwait(false);
            if (seedResponse.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(await seedResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
                JArray frameworks = data.Value<JArray>("frameworks");
                UoFrameworkModel eaFramework = null;
                foreach (JObject framework in frameworks.Values<JObject>())
                {
                    HttpResponseMessage frameworkPost = await PostToService(client, new System.Uri(baseUri + "/api/frameworks"), framework).ConfigureAwait(false);
                    if (frameworkPost.IsSuccessStatusCode)
                    {
                        string name = framework.Value<string>("name");
                        if (name == "Stage 1 Competency Standard for Professional Engineer")
                        {
                            HttpResponseMessage frameworkResponse = await client.GetAsync(frameworkPost.Headers.Location).ConfigureAwait(false);
                            eaFramework = JsonConvert.DeserializeObject<UoFrameworkModel>(await frameworkResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
                        }
                    }
                }

                if (eaFramework is UoFrameworkModel _)
                {
                    JArray eaLevelCategories = data.Value<JArray>("eaLevelCategories");
                    foreach (JObject levelCategoryJson in eaLevelCategories)
                    {
                        HttpResponseMessage levelCategoryPost = await PostToService(client, new System.Uri(baseUri + "/api/level-categories"), levelCategoryJson).ConfigureAwait(false);
                        if (levelCategoryPost.IsSuccessStatusCode)
                        {
                            HttpResponseMessage levelCategoryResponse = await client.GetAsync(levelCategoryPost.Headers.Location).ConfigureAwait(false);
                            UoLevelCategoryModel levelCategory = JsonConvert.DeserializeObject<UoLevelCategoryModel>(await levelCategoryResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
                            foreach (UoNodeModel node in eaFramework.Nodes)
                            {
                                string mappingJson = "{\"levelCategoryId\": " + levelCategory.LevelCategoryId + ", \"nodeId\": " + node.NodeId + "}";
                                await PostToService(client, new System.Uri(baseUri + "/api/level-category-nodes"), JObject.Parse(mappingJson)).ConfigureAwait(false);
                                foreach (UoNodeModel childNode in node.ChildNodes)
                                {
                                    string childMappingJson = "{\"levelCategoryId\": " + levelCategory.LevelCategoryId + ", \"nodeId\": " + childNode.NodeId + "}";
                                    await PostToService(client, new System.Uri(baseUri + "/api/level-category-nodes"), JObject.Parse(childMappingJson)).ConfigureAwait(false);
                                }
                            }
                        }   
                    }
                }

                JArray academicOrgs = data.Value<JArray>("academicOrgs");
                foreach (JObject academicOrg in academicOrgs.Values<JObject>())
                {
                    await PostToService(client, new System.Uri(baseUri + "/api/academic-orgs"), academicOrg).ConfigureAwait(false);
                }

                JObject mapping = data.Value<JObject>("mapping");

                JObject program = data.Value<JObject>("program");
                JToken[] courseLists = new JToken[4];
                program.Value<JArray>("programStructure").CopyTo(courseLists, 0);
                program.Remove("programStructure");
                if (eaFramework is UoFrameworkModel _)
                {
                    JObject project = new JObject
                    {
                        { "name", "Bachelor of Aerospace Systems Engineering - EA 2020" },
                        { "description", "Engineers Australia accreditation of Bachelor of Aerospace Systems Engineering conducted in September, 2020." },
                        { "frameworkId", eaFramework.FrameworkId }
                    };
                    await PostToService(client, new System.Uri(baseUri + "/api/projects"), project).ConfigureAwait(false);
                }
                await PostToService(client, new System.Uri(baseUri + "/api/programs"), program).ConfigureAwait(false);
                await PostToService(client, new System.Uri(baseUri + "/api/project-programs"), JObject.Parse("{\"programId\": 1 ,\"projectId\": 1}")).ConfigureAwait(false);
                foreach (JObject courseList in courseLists)
                {
                    courseList.Add("programId", 1);
                    await PostToService(client, new System.Uri(baseUri + "/api/course-lists"), courseList).ConfigureAwait(false);
                }

                JArray courses = data.Value<JArray>("courses");
                foreach (JObject course in courses)
                {
                    JToken[] courseInstances = new JToken[course.Value<JArray>("courseInstances").Count];
                    course.Value<JArray>("courseInstances").CopyTo(courseInstances, 0);
                    course.Remove("courseInstances");

                    string courseListId = course.Value<JValue>("courseListId").ToString(Thread.CurrentThread.CurrentCulture);
                    course.Remove("courseListId");

                    HttpResponseMessage coursePost = await PostToService(client, new System.Uri(baseUri + "/api/courses"), course).ConfigureAwait(false);
                    string[] courseSegments = coursePost.Headers.Location.Segments;
                    int courseId = int.Parse((string)courseSegments.GetValue(courseSegments.Length - 1), Thread.CurrentThread.CurrentCulture);

                    string clMappingJson = "{\"courseListId\": " + courseListId + ", \"courseId\": " + courseId + "}";
                    await PostToService(client, new System.Uri(baseUri + "/api/course-list-courses"), JObject.Parse(clMappingJson)).ConfigureAwait(false);

                    string subject = course.GetValue("subject", System.StringComparison.CurrentCulture).ToString();
                    string catalogNumber = course.GetValue("catalogNumber", System.StringComparison.CurrentCulture).ToString();
                    foreach (JObject levelMap in mapping.Value<JArray>(subject + catalogNumber))
                    {
                        int nodeId = (int)levelMap.GetValue("node", System.StringComparison.CurrentCulture);
                        foreach (int levelId in levelMap.Value<JArray>("levels"))
                        {
                            string lcsMappingJson = "{\"courseId\": " + courseId + ", \"levelId\": " + levelId + ", \"nodeId\": " + nodeId + "}";
                            await PostToService(client, new System.Uri(baseUri + "/api/level-courses"), JObject.Parse(lcsMappingJson)).ConfigureAwait(false);
                        }
                    }

                    foreach (JObject courseInstance in courseInstances)
                    {
                        IDictionary<string, int> learningOutcomeCache = new Dictionary<string, int>();

                        JToken[] learningOutcomes = new JToken[courseInstance.Value<JArray>("learningOutcomes").Count];
                        courseInstance.Value<JArray>("learningOutcomes").CopyTo(learningOutcomes, 0);
                        courseInstance.Remove("learningOutcomes");

                        JToken[] assessments = new JToken[courseInstance.Value<JArray>("assessments").Count];
                        courseInstance.Value<JArray>("assessments").CopyTo(assessments, 0);
                        courseInstance.Remove("assessments");

                        courseInstance.Add("courseId", courseId);
                        HttpResponseMessage courseInstancePost = await PostToService(client, new System.Uri(baseUri + "/api/course-instances"), courseInstance).ConfigureAwait(false);
                        if (courseInstancePost.IsSuccessStatusCode)
                        {
                            string[] courseInstanceSegments = courseInstancePost.Headers.Location.Segments;
                            int courseInstanceId = int.Parse((string)courseInstanceSegments.GetValue(courseInstanceSegments.Length - 1), Thread.CurrentThread.CurrentCulture);
                            foreach (JObject learningOutcome in learningOutcomes)
                            {
                                string learningOutcomeName = learningOutcome.GetValue("name", System.StringComparison.CurrentCulture).ToString();
                                if (learningOutcomeCache.ContainsKey(learningOutcomeName) == false)
                                {
                                    learningOutcome.Add("courseInstanceId", courseInstanceId);
                                    HttpResponseMessage learningOutcomePost = await PostToService(client, new System.Uri(baseUri + "/api/learning-outcomes"), learningOutcome).ConfigureAwait(false);
                                    if (learningOutcomePost.IsSuccessStatusCode)
                                    {
                                        string[] learningOutcomeSegments = learningOutcomePost.Headers.Location.Segments;
                                        int learningOutcomeId = int.Parse((string)learningOutcomeSegments.GetValue(learningOutcomeSegments.Length - 1), Thread.CurrentThread.CurrentCulture);
                                        learningOutcomeCache.Add(learningOutcomeName, learningOutcomeId);
                                    }
                                }
                            }

                            foreach (JObject assessment in assessments)
                            {
                                JToken[] tempLearningOutcomes = new JToken[assessment.Value<JArray>("tempLearningOutcomes").Count];
                                assessment.Value<JArray>("tempLearningOutcomes").CopyTo(tempLearningOutcomes, 0);
                                assessment.Remove("tempLearningOutcomes");
                                assessment.Add("courseInstanceId", courseInstanceId);

                                HttpResponseMessage assessmentPost = await PostToService(client, new System.Uri(baseUri + "/api/assessments"), assessment).ConfigureAwait(false);
                                string[] assessmentSegments = assessmentPost.Headers.Location.Segments;
                                int assessmentId = int.Parse((string)assessmentSegments.GetValue(assessmentSegments.Length - 1), Thread.CurrentThread.CurrentCulture);

                                foreach (JValue tempLearningOutcome in tempLearningOutcomes)
                                {
                                    int learningOutcomeId = learningOutcomeCache[tempLearningOutcome.ToString(Thread.CurrentThread.CurrentCulture)];
                                    string loMappingJson = "{\"learningOutcomeId\": " + learningOutcomeId + ", \"assessmentId\": " + assessmentId + "}";
                                    await PostToService(client, new System.Uri(baseUri + "/api/learning-outcome-assessments"), JObject.Parse(loMappingJson)).ConfigureAwait(false);
                                }
                            }
                        }
                    }
                }
            }

            client.Dispose();
            return NoContent();
        }
        #endregion
    }
}
