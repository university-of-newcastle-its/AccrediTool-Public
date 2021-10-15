using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoCourseListRepository
    {
        List<UoCourseListModel> GetAllCourseLists();
        Task<UoCourseListModel> GetCourseListById(int id);
        Task<int?> AddCourseList(UoCourseListModel courseList);
        Task<int?> SetCourseListById(int id, UoCourseListModel courseList);
        Task<int?> UpdateCourseListById(int id, JObject patchObject);
        Task<int?> DeleteCourseListById(int id);
    }
}
