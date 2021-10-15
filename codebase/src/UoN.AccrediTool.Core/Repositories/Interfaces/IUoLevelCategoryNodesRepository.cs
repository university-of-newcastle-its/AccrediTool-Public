using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelCategoryNodesRepository
    {
        Task<int?> AddLevelCategoryNodes(UoLevelCategoryNodesJoin levelCategoryNodes);
        Task<int?> DeleteLevelCategoryNodesById(int id);
    }
}
