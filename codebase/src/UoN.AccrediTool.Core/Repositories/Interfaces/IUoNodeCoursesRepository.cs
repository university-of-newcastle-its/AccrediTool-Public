using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoNodeCoursesRepository
    {
        Task<int?> AddNodeCourses(UoNodeCoursesJoin nodeCourses);
        Task<int?> DeleteNodeCoursesById(int id);
    }
}
