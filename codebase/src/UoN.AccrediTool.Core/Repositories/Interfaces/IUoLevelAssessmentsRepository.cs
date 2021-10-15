using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelAssessmentsRepository
    {
        Task<int?> AddLevelAssessments(UoLevelAssessmentsJoin levelAssessments);
        Task<int?> DeleteLevelAssessmentsById(int id);
    }
}
