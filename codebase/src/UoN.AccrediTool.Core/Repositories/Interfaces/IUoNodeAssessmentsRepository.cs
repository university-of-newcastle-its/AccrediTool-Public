using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoNodeAssessmentsRepository
    {
        Task<int?> AddNodeAssessments(UoNodeAssessmentsJoin nodeAssessments);
        Task<int?> DeleteNodeAssessmentsById(int id);
    }
}
