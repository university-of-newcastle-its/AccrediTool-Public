using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLearningOutcomeAssessmentsRepository : IUoLearningOutcomeAssessmentsRepository
    {
        private readonly DataContext _context;
        public UoLearningOutcomeAssessmentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddLearningOutcomeAssessments(UoLearningOutcomeAssessmentsJoin learningOutcomeAssessments)
        {

            if (learningOutcomeAssessments is UoLearningOutcomeAssessmentsJoin)
            {
                if (await _context.FindAsync<UoLearningOutcomeAssessmentsJoin>(learningOutcomeAssessments.Id).ConfigureAwait(false) is null)
                {
                    learningOutcomeAssessments.Id = 0;
                    _context.Add(learningOutcomeAssessments);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return learningOutcomeAssessments.Id;
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

        public async Task<int?> DeleteLearningOutcomeAssessmentsById(int id)
        {
            if (await _context.FindAsync<UoLearningOutcomeAssessmentsJoin>(id).ConfigureAwait(false) is UoLearningOutcomeAssessmentsJoin join)
            {
                _context.Remove(join);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
