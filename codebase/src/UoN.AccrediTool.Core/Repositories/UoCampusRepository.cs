using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoCampusRepository : IUoCampusRepository
    {
        private readonly DataContext _context;
        public UoCampusRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoCampusModel> GetAllCampuses()
        {
            List<UoCampusModel> campusItems = _context.Campus.ToList();
            return campusItems;
        }

        public async Task<UoCampusModel> GetCampusById(int id)
        {
            try
            {
                UoCampusModel campusItem = await _context.Campus
                    .Include(campus => campus.CourseInstances)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(campus => campus.Programs)
                    .SingleAsync(campus => campus.CampusId == id)
                    .ConfigureAwait(false);

                return campusItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Campus.FindAsync(id).ConfigureAwait(false);
            }
        }
    }
}
