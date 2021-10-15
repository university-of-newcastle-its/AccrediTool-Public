using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoCourseInstanceRepository
    {
        List<UoCourseInstanceModel> GetAllCourseInstances();
        Task<UoCourseInstanceModel> GetCourseInstanceById(int id);
        Task<int?> AddCourseInstance(UoCourseInstanceModel courseInstance);
        Task<int?> SetCourseInstanceById(int id, UoCourseInstanceModel courseInstance);
        Task<int?> UpdateCourseInstanceById(int id, JObject patchObject);
        Task<int?> DeleteCourseInstanceById(int id);
    }
}
