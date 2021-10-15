using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLevelCoursesRepository : IUoLevelCoursesRepository
    {
        private readonly DataContext _context;
        public UoLevelCoursesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddLevelCourses(UoLevelCoursesJoin levelCourses)
        {

            if (levelCourses is UoLevelCoursesJoin)
            {
                if (await _context.FindAsync<UoLevelCoursesJoin>(levelCourses.Id).ConfigureAwait(false) is null)
                {
                    levelCourses.Id = 0;
                    _context.Add(levelCourses);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return levelCourses.Id;
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

        public async Task<int?> DeleteLevelCoursesById(int id)
        {
            if (await _context.FindAsync<UoLevelCoursesJoin>(id).ConfigureAwait(false) is UoLevelCoursesJoin levelCourses)
            {
                _context.Remove(levelCourses);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
