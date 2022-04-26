using System.Threading.Tasks;
using System.Collections.Generic;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelCoursesRepository
    {
        List<UoLevelCoursesJoin> GetLevelCoursesByCourseId(int id);
        Task<int?> AddLevelCourses(UoLevelCoursesJoin levelCourses);
        Task<int?> DeleteLevelCoursesById(int id);
    }
}
