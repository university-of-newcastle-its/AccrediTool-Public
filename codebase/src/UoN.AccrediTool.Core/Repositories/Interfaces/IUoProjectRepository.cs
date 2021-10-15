using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoProjectRepository
    {
        List<UoProjectModel> GetAllProjects();
        Task<UoProjectModel> GetProjectById(int id);
        Task<int?> AddProject(UoProjectModel project);
        Task<int?> SetProjectById(int id, UoProjectModel project);
        Task<int?> UpdateProjectById(int id, JObject patchObject);
        Task<int?> DeleteProjectById(int id);
    }
}
