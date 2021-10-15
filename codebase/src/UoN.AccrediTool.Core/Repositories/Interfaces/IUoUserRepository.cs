using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoUserRepository
    {
        List<UoUserModel> GetAllUsers();
        Task<UoUserModel> GetUserById(int id);
        Task<UoUserModel> GetUserByLogin(string login);
        Task<int?> AddUser(UoUserModel user);
        Task<int?> DeleteUserById(int id);
    }
}
