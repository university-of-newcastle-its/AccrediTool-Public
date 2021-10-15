using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoUserGroupUsersRepository : IUoUserGroupUsersRepository
    {
        private readonly DataContext _context;
        public UoUserGroupUsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int?> AddUserGroupUsers(UoUserGroupUsersJoin userGroupUsers)
        {

            if (userGroupUsers is UoUserGroupUsersJoin)
            {
                if (await _context.FindAsync<UoUserGroupUsersJoin>(userGroupUsers.Id).ConfigureAwait(false) is null)
                {
                    userGroupUsers.Id = 0;
                    _context.Add(userGroupUsers);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return userGroupUsers.Id;
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

        public async Task<int?> DeleteUserGroupUsersById(int id)
        {
            if (await _context.FindAsync<UoUserGroupUsersJoin>(id).ConfigureAwait(false) is UoUserGroupUsersJoin userGroupUsers)
            {
                _context.Remove(userGroupUsers);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
