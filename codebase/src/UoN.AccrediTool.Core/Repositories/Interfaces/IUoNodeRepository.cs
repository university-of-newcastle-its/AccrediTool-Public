using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoNodeRepository
    {
        List<UoNodeModel> GetAllNodes();
        Task<UoNodeModel> GetNodeById(int id);
        Task<int?> AddNode(UoNodeModel node);
        Task<int?> SetNodeById(int id, UoNodeModel node);
        Task<int?> UpdateNodeById(int id, JObject patchObject);
        Task<int?> DeleteNodeById(int id);
    }
}
