using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoNodeProgramsRepository
    {
        Task<int?> AddNodePrograms(UoNodeProgramsJoin nodePrograms);
        Task<int?> DeleteNodeProgramsById(int id);
    }
}
