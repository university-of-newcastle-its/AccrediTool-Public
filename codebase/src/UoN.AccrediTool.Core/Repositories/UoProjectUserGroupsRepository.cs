using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoProjectUserGroupsRepository : IUoProjectUserGroupsRepository
    {
        private readonly DataContext _context;
        public UoProjectUserGroupsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddProjectUserGroups(UoProjectUserGroupsJoin projectUserGroups)
        {

            if (projectUserGroups is UoProjectUserGroupsJoin)
            {
                if (await _context.FindAsync<UoProjectUserGroupsJoin>(projectUserGroups.Id).ConfigureAwait(false) is null)
                {
                    projectUserGroups.Id = 0;
                    _context.Add(projectUserGroups);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return projectUserGroups.Id;
                    }
                    catch (DbUpdateException)
                    {
                        return 0;
                    }
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> DeleteProjectUserGroupsById(int id)
        {
            if (await _context.FindAsync<UoProjectUserGroupsJoin>(id).ConfigureAwait(false) is UoProjectUserGroupsJoin projectUserGroups)
            {
                _context.Remove(projectUserGroups);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
