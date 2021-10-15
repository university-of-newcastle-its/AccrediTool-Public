using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelCoursesRepository
    {
        Task<int?> AddLevelCourses(UoLevelCoursesJoin levelCourses);
        Task<int?> DeleteLevelCoursesById(int id);
    }
}
