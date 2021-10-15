using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelRepository
    {
        List<UoLevelModel> GetAllLevels();
        Task<UoLevelModel> GetLevelById(int id);
        Task<int?> AddLevel(UoLevelModel level);
        Task<int?> SetLevelById(int id, UoLevelModel level);
        Task<int?> UpdateLevelById(int id, JObject patchObject);
        Task<int?> DeleteLevelById(int id);
    }
}
