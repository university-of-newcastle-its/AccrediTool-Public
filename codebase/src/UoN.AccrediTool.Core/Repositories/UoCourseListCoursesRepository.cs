using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoCourseListCoursesRepository : IUoCourseListCoursesRepository
    {
        private readonly DataContext _context;
        public UoCourseListCoursesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddCourseListCourses(UoCourseListCoursesJoin courseListCourses)
        {

            if (courseListCourses is UoCourseListCoursesJoin)
            {
                if (await _context.FindAsync<UoCourseListCoursesJoin>(courseListCourses.Id).ConfigureAwait(false) is null)
                {
                    courseListCourses.Id = 0;
                    _context.Add(courseListCourses);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return courseListCourses.Id;
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

        public async Task<int?> DeleteCourseListCoursesById(int id)
        {
            if (await _context.FindAsync<UoCourseListCoursesJoin>(id).ConfigureAwait(false) is UoCourseListCoursesJoin courseListCourses)
            {
                _context.Remove(courseListCourses);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
