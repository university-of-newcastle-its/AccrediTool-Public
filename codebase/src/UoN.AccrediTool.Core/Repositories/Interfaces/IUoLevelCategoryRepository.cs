using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLevelCategoryRepository
    {
        List<UoLevelCategoryModel> GetAllLevelCategories();
        Task<UoLevelCategoryModel> GetLevelCategoryById(int id);
        Task<int?> AddLevelCategory(UoLevelCategoryModel levelCategory);
        Task<int?> SetLevelCategoryById(int id, UoLevelCategoryModel levelCategory);
        Task<int?> UpdateLevelCategoryById(int id, JObject patchObject);
        Task<int?> DeleteLevelCategoryById(int id);
    }
}
