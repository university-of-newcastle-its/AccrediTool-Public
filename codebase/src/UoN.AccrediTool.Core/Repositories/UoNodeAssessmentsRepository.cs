using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoNodeAssessmentsRepository : IUoNodeAssessmentsRepository
    {
        private readonly DataContext _context;
        public UoNodeAssessmentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddNodeAssessments(UoNodeAssessmentsJoin nodeAssessments)
        {

            if (nodeAssessments is UoNodeAssessmentsJoin)
            {
                if (await _context.FindAsync<UoNodeAssessmentsJoin>(nodeAssessments.Id).ConfigureAwait(false) is null)
                {
                    nodeAssessments.Id = 0;
                    _context.Add(nodeAssessments);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return nodeAssessments.Id;
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

        public async Task<int?> DeleteNodeAssessmentsById(int id)
        {
            if (await _context.FindAsync<UoNodeAssessmentsJoin>(id).ConfigureAwait(false) is UoNodeAssessmentsJoin nodeAssessments)
            {
                _context.Remove(nodeAssessments);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
