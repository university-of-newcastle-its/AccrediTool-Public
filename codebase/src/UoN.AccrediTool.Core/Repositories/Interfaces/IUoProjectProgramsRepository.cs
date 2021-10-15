using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoProjectProgramsRepository
    {
        Task<int?> AddProjectPrograms(UoProjectProgramsJoin projectPrograms);
        Task<int?> DeleteProjectProgramsById(int id);
    }
}
