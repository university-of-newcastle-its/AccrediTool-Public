using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Models;

// Global variables used in UoN.AccrediTool.Service.Controllers

namespace UoN.AccrediTool.Service.Controllers
{
    public static class UoServiceControllers
    {
        public static readonly List<string> validTemplates = new List<string>()
        {
            "EngineersAustralia"
        };

        public static async Task<int?> GetTopLevelFrameworkId(UoNodeModel startNode, HttpContext context)
        {
            HttpClient client = new HttpClient();
            UoNodeModel node = startNode;
            UoNodeModel parentNode = node?.ParentNode;
            int? frameworkId = null;
            while (frameworkId == null)
            {
                if (parentNode != null)
                {
                    if (parentNode.Framework is UoFrameworkModel framework)
                    {
                        frameworkId = framework.FrameworkId;
                    }
                    else if (context != null)
                    {
                        string uri = "http" + (context.Request.IsHttps ? "s" : "") + "://" + context.Request.Host.ToString() + "/api/nodes/" + parentNode.NodeId.ToString(Thread.CurrentThread.CurrentCulture);
                        HttpResponseMessage response = await client.GetAsync(new System.Uri(uri)).ConfigureAwait(false);
                        if (response.IsSuccessStatusCode)
                        {
                            node = JsonConvert.DeserializeObject<UoNodeModel>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                            parentNode = node.ParentNode;
                            continue;
                        }
                    }
                    parentNode = null;
                }
                else if (node.Framework is UoFrameworkModel framework)
                {
                    frameworkId = framework.FrameworkId;
                }
                else
                {
                    break;
                }
            }

            client.Dispose();
            return frameworkId;
        }

        public static async Task<List<UoLevelCategoryModel>> GetMappedLevelCategories(List<UoLevelCategoryModel> levelCategories, HttpContext context)
        {
            HttpClient client = new HttpClient();
            if (levelCategories != null && context != null)
            {
                foreach (var levelCategory in levelCategories)
                {
                    List<UoLevelModel> levels = new List<UoLevelModel>();
                    foreach (var level in levelCategory.Levels)
                    {
                        string uri = "http" + (context.Request.IsHttps ? "s" : "") + "://" + context.Request.Host.ToString() + "/api/levels/" + level.LevelId.ToString(Thread.CurrentThread.CurrentCulture);
                        HttpResponseMessage response = await client.GetAsync(new System.Uri(uri)).ConfigureAwait(false);
                        if (response.IsSuccessStatusCode)
                        {
                            levels.Add(JsonConvert.DeserializeObject<UoLevelModel>(await response.Content.ReadAsStringAsync().ConfigureAwait(false)));
                        }
                    }

                    levelCategory.Levels.Clear();
                    levelCategory.Levels.AddRange(levels);
                }
            }

            client.Dispose();
            return levelCategories;
        }
    }
}
