using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
   public interface IUoProgramRepository
    {
        List<UoProgramModel> GetAllPrograms();
        Task<UoProgramModel> GetProgramById(int id);
        Task<UoProgramModel> GetProgramMappingById(int id);
        Task<int?> AddProgram(UoProgramModel program);
        Task<int?> SetProgramById(int id, UoProgramModel program);
        Task<int?> UpdateProgramById(int id, JObject patchObject);
        Task<int?> DeleteProgramById(int id);
        Task<UoProjectModel> GetProjectById(int id);
    }
}
