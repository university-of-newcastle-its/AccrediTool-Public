using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoNodeCoursesRepository : IUoNodeCoursesRepository
    {
        private readonly DataContext _context;
        public UoNodeCoursesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddNodeCourses(UoNodeCoursesJoin nodeCourses)
        {

            if (nodeCourses is UoNodeCoursesJoin)
            {
                if (await _context.FindAsync<UoNodeCoursesJoin>(nodeCourses.Id).ConfigureAwait(false) is null)
                {
                    nodeCourses.Id = 0;
                    _context.Add(nodeCourses);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return nodeCourses.Id;
                    }
                    catch (DbUpdateException)
                    {
                        return 0;
                    }
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> DeleteNodeCoursesById(int id)
        {
            if (await _context.FindAsync<UoNodeCoursesJoin>(id).ConfigureAwait(false) is UoNodeCoursesJoin nodeCourses)
            {
                _context.Remove(nodeCourses);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
