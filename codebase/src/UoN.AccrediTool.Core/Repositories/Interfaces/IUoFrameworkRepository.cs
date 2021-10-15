using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoFrameworkRepository
    {
        List<UoFrameworkModel> GetAllFrameworks();
        Task<UoFrameworkModel> GetFrameworkById(int id);
        Task<int?> AddFramework(UoFrameworkModel framework);
        Task<int?> SetFrameworkById(int id, UoFrameworkModel framework);
        Task<int?> UpdateFrameworkById(int id, JObject patchObject);
        Task<int?> DeleteFrameworkById(int id);
    }
}
