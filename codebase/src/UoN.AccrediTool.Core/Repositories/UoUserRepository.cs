using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoUserRepository : IUoUserRepository
    {
        private readonly DataContext _context;
        public UoUserRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoUserModel> GetAllUsers()
        {
            List<UoUserModel> userItems = _context.User.ToList();
            return userItems;
        }

        public async Task<UoUserModel> GetUserById(int id)
        {
            try
            {
                UoUserModel userItem = await _context.User
                    .Include(user => user.UserGroupUsers)
                        .ThenInclude(join => join.UserGroup)
                    .SingleAsync(user => user.UserId == id)
                    .ConfigureAwait(false);

                return userItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.User.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<UoUserModel> GetUserByLogin(string login)
        {
            try
            {
                UoUserModel userItem = await _context.User
                    .Include(user => user.UserGroupUsers)
                        .ThenInclude(join => join.UserGroup)
                    .SingleAsync(user => user.Login == login)
                    .ConfigureAwait(false);

                return userItem;
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
        }

        public async Task<int?> AddUser(UoUserModel user)
        {
            if (user is UoUserModel)
            {
                user.UserId = 0;
                _context.User.Add(user);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return user.UserId;
            }
            return null;
        }

        public async Task<int?> DeleteUserById(int id)
        {
            if (await _context.User.FindAsync(id).ConfigureAwait(false) is UoUserModel user)
            {
                _context.User.Remove(user);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
