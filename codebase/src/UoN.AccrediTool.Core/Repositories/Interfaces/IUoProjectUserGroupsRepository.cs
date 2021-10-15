using System.Threading.Tasks;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoProjectUserGroupsRepository
    {
        Task<int?> AddProjectUserGroups(UoProjectUserGroupsJoin projectUserGroups);
        Task<int?> DeleteProjectUserGroupsById(int id);
    }
}
