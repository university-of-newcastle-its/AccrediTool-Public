using System.Collections.Generic;
using System.Threading.Tasks;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoCampusRepository
    {
        List<UoCampusModel> GetAllCampuses();
        Task<UoCampusModel> GetCampusById(int id);
    }
}
