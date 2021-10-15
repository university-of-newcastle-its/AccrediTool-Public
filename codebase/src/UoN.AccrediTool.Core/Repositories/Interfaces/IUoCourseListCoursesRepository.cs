using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoCourseListCoursesRepository
    {
        Task<int?> AddCourseListCourses(UoCourseListCoursesJoin courseListCourses);
        Task<int?> DeleteCourseListCoursesById(int id);
    }
}
