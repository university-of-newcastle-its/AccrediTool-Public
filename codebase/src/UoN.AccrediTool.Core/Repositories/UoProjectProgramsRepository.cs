using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoProjectProgramsRepository : IUoProjectProgramsRepository
    {
        private readonly DataContext _context;
        public UoProjectProgramsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddProjectPrograms(UoProjectProgramsJoin projectPrograms)
        {

            if (projectPrograms is UoProjectProgramsJoin)
            {
                if (await _context.FindAsync<UoProjectProgramsJoin>(projectPrograms.Id).ConfigureAwait(false) is null)
                {
                    projectPrograms.Id = 0;
                    _context.Add(projectPrograms);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return projectPrograms.Id;
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

        public async Task<int?> DeleteProjectProgramsById(int id)
        {
            if (await _context.FindAsync<UoProjectProgramsJoin>(id).ConfigureAwait(false) is UoProjectProgramsJoin projectPrograms)
            {
                _context.Remove(projectPrograms);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
