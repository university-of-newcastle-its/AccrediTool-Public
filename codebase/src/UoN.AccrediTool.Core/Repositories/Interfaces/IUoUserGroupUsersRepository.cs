using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoUserGroupUsersRepository
    {
        Task<int?> AddUserGroupUsers(UoUserGroupUsersJoin userGroupUsers);
        Task<int?> DeleteUserGroupUsersById(int id);
    }
}
