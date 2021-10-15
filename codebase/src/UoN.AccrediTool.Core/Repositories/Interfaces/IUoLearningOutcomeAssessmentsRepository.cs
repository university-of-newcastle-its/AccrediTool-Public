using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLearningOutcomeAssessmentsRepository
    {
        Task<int?> AddLearningOutcomeAssessments(UoLearningOutcomeAssessmentsJoin learningOutcomeAsssessments);
        Task<int?> DeleteLearningOutcomeAssessmentsById(int id);
    }
}
