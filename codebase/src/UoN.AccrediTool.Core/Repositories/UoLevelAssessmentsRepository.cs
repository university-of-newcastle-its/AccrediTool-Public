using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLevelAssessmentsRepository : IUoLevelAssessmentsRepository
    {
        private readonly DataContext _context;
        public UoLevelAssessmentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddLevelAssessments(UoLevelAssessmentsJoin levelAssessments)
        {

            if (levelAssessments is UoLevelAssessmentsJoin)
            {
                if (await _context.FindAsync<UoLevelAssessmentsJoin>(levelAssessments.Id).ConfigureAwait(false) is null)
                {
                    levelAssessments.Id = 0;
                    _context.Add(levelAssessments);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return levelAssessments.Id;
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

        public async Task<int?> DeleteLevelAssessmentsById(int id)
        {
            if (await _context.FindAsync<UoLevelAssessmentsJoin>(id).ConfigureAwait(false) is UoLevelAssessmentsJoin levelAssessments)
            {
                _context.Remove(levelAssessments);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
