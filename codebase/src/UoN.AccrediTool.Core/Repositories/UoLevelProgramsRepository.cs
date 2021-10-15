using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLevelProgramsRepository : IUoLevelProgramsRepository
    {
        private readonly DataContext _context;
        public UoLevelProgramsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddLevelPrograms(UoLevelProgramsJoin levelPrograms)
        {

            if (levelPrograms is UoLevelProgramsJoin)
            {
                if (await _context.FindAsync<UoLevelProgramsJoin>(levelPrograms.Id).ConfigureAwait(false) is null)
                {
                    levelPrograms.Id = 0;
                    _context.Add(levelPrograms);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return levelPrograms.Id;
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

        public async Task<int?> DeleteLevelProgramsById(int id)
        {
            if (await _context.FindAsync<UoLevelProgramsJoin>(id).ConfigureAwait(false) is UoLevelProgramsJoin levelPrograms)
            {
                _context.Remove(levelPrograms);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
