using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoUserGroupRepository
    {
        List<UoUserGroupModel> GetAllUserGroups();
        Task<UoUserGroupModel> GetUserGroupById(int id);
        Task<int?> AddUserGroup(UoUserGroupModel userGroup);
        Task<int?> UpdateUserGroupById(int id, JObject patchObject);
        Task<int?> DeleteUserGroupById(int id);
    }
}
