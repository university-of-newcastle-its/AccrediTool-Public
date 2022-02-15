using System.Threading.Tasks;
using System.Collections.Generic;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelCategoryNodesRepository
    {

        public List<UoLevelCategoryNodesJoin> GetAllLevelCategoryNodesJoins();
        Task<int?> AddLevelCategoryNodes(UoLevelCategoryNodesJoin levelCategoryNodes);
        Task<int?> DeleteLevelCategoryNodesById(int id);
    }
}
