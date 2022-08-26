/**------------------------------------------------------------------------
 * ?                                ABOUT
 * @repo           : AccrediTool
 * @description    : This class contains static fucntions that are used to
 *                 : interact with the AccrediTool API. API endpoints specs
 *                 : can be found at: https://dev.azure.com/uon-web-app/AccrediTool/_wiki/wikis/AccrediTool.wiki/202/API-Specification#
 *------------------------------------------------------------------------**/
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using Microsoft.Extensions.Configuration;

//JSON.net
using UoN.AccrediTool.Core.Utility;

namespace UoN.AccrediTool.Web.API
{
    public static class API
    {
        /**========================================================================
         **                           GetRestClient
         *?  This function gets a rest client that has authed with the API.
         *@param  configuration IConfiguration
         *@return RestClient
         *========================================================================**/
        private static RestClient GetRestClient(IConfiguration configuration)
        {
            Uri baseUri = new Uri(configuration["Services:UoN.AccrediTool.Service:BaseUri"], UriKind.Absolute);
            Uri loginPath = new Uri(configuration["Services:UoN.AccrediTool.Service:LoginPath"], UriKind.Relative);

            RestClient client = new RestClient(baseUri, loginPath,
                new
                {
                    Username = configuration["Services:UoN.AccrediTool.Service:LoginId"],
                    Secret = configuration["Services:UoN.AccrediTool.Service:LoginSecret"]
                });

            return client;

        }
        /*---------------------------- END OF FUNCTION ----------------------------*/
        /**========================================================================
         **                           PostJSON
         *?  This functions makes a post request to a given api endpoint.
         *   for example: API.API.PostJSON(json, "course-lists", _Configuration)
         *   makes a POST request to the course-list api with a given json.
         *@param  json string
         *@param  endpoint string
         *@param  configuration IConfiguration
         *@return string | JSON formated API response
         *========================================================================**/
        public static string PostJSON(string json, string endpoint, IConfiguration configuration)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            RestClient client = GetRestClient(configuration);

            return client?.Post(new Uri("api/" + endpoint, UriKind.Relative), content);
        }
        /*---------------------------- END OF FUNCTION ----------------------------*/
        /**========================================================================
         **                           GetJSON
         *?  This functions makes a GET request to a given api endpoint. The function
         *?  then returns a json string from the API
         *@param  endpoint string
         *@param  configuration IConfiguration
         *@return string | JSON formated API response
         *========================================================================**/
        public static string GetJSON(string endpoint, IConfiguration configuration)
        {

            RestClient client = GetRestClient(configuration);

            return client?.Get(new Uri("api/" + endpoint, UriKind.Relative ));

        }
        /*---------------------------- END OF FUNCTION ----------------------------*/
        /**========================================================================
         **                           Update
         *?  This functions makes a PUT request to a given api endpoint. The function
         *?  then returns a json string from the API
         *@param  json string
         *@param  endpoint string
         *@param  configuration IConfiguration
         *@return string | JSON formated API response
         *========================================================================**/
        public static string Update(string json, string endpoint, IConfiguration configuration)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            RestClient client = GetRestClient(configuration);

            return client?.Put(new Uri("api/" + endpoint, UriKind.Relative ), content);

        }
        /*---------------------------- END OF FUNCTION ----------------------------*/
        /**========================================================================
         **                           DownloadProject
         *?  This functions makes a downloads a given project from the API
         *@param  projectId int
         *@param  configuration IConfiguration
         *========================================================================**/
        public static void DownloadProject(int projectId, IConfiguration configuration)
        {
            RestClient client = GetRestClient(configuration);

            if(!System.IO.File.Exists("projects/" + projectId + ".zip"))
            {
                //downloads file from service
                client?.GetFile(new Uri("api/projects/" + projectId + "/export?download=true", UriKind.Relative), projectId);
 
            }
            List<string> filePaths = new();

            filePaths.AddRange(Directory.GetFiles("projects"));

            // delete projects that are older then a week
            for(int i = 0; i < filePaths.Count; i++)
            {
                TimeSpan fileAge = File.GetLastWriteTime(filePaths[i]) - DateTime.Now;

                if (fileAge.Days > 7)
                {
                    File.Delete(filePaths[i]);
                }
            }

        }
        /*---------------------------- END OF FUNCTION ----------------------------*/
        /**========================================================================
         **                           Update
         *?  This functions makes a DELETE request to a given api endpoint.
         *@param  endpoint string
         *@param  configuration IConfiguration
         *@return bool 
         *========================================================================**/

        public static bool Delete(string endpoint, IConfiguration configuration)
        {
            RestClient client = GetRestClient(configuration);

            var response = client?.Delete(new Uri("api/" + endpoint, UriKind.Relative ));

            if (response != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*---------------------------- END OF FUNCTION ----------------------------*/
    }
}
