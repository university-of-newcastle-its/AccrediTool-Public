using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoCourseRepository
    {
        List<UoCourseModel> GetAllCourses();
        Task<UoCourseModel> GetCourseById(int id);
        Task<int?> AddCourse(UoCourseModel course);
        Task<int?> SetCourseById(int id, UoCourseModel course);
        Task<int?> UpdateCourseById(int id, JObject patchObject);
        Task<int?> DeleteCourseById(int id);
    }
}
