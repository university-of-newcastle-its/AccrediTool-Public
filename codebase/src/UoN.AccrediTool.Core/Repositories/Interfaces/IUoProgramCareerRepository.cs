using System.Collections.Generic;
using System.Threading.Tasks;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoProgramCareerRepository
    {
        List<UoProgramCareerModel> GetAllProgramCareers();
        Task<UoProgramCareerModel> GetProgramCareerById(int id);
    }
}
