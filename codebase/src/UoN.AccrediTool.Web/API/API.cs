
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using Microsoft.Extensions.Configuration;

//JSON.net
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Utility;

namespace UoN.AccrediTool.Web.API
{
    public static class API
    {

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

        public static string PostJSON(string json, string endpoint, IConfiguration configuration)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            RestClient client = GetRestClient(configuration);

            return client?.Post(new Uri("api/" + endpoint, UriKind.Relative), content);
        }

        public static string GetJSON(string endpoint, IConfiguration configuration)
        {

            RestClient client = GetRestClient(configuration);

            return client?.Get(new Uri("api/" + endpoint, UriKind.Relative ));

        }

        public static string Update(string json, string endpoint, IConfiguration configuration)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            RestClient client = GetRestClient(configuration);

            return client?.Put(new Uri("api/" + endpoint, UriKind.Relative ), content);

        }


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

    

        public static bool Delete(string endpoint, IConfiguration configuration)
        {
            RestClient client = GetRestClient(configuration);

            var response = client?.Delete(new Uri("api/" + endpoint, UriKind.Relative ));

            if(response != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
