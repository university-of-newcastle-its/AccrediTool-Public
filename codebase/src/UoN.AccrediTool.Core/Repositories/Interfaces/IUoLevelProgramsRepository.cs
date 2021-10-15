using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelProgramsRepository
    {
        Task<int?> AddLevelPrograms(UoLevelProgramsJoin levelPrograms);
        Task<int?> DeleteLevelProgramsById(int id);
    }
}
