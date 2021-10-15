using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoNodeProgramsRepository : IUoNodeProgramsRepository
    {
        private readonly DataContext _context;
        public UoNodeProgramsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddNodePrograms(UoNodeProgramsJoin nodePrograms)
        {

            if (nodePrograms is UoNodeProgramsJoin)
            {
                if (await _context.FindAsync<UoNodeProgramsJoin>(nodePrograms.Id).ConfigureAwait(false) is null)
                {
                    nodePrograms.Id = 0;
                    _context.Add(nodePrograms);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return nodePrograms.Id;
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

        public async Task<int?> DeleteNodeProgramsById(int id)
        {
            if (await _context.FindAsync<UoNodeProgramsJoin>(id).ConfigureAwait(false) is UoNodeProgramsJoin nodePrograms)
            {
                _context.Remove(nodePrograms);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
