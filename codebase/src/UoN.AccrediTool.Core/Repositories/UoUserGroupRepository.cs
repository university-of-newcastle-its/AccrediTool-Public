using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoUserGroupRepository : IUoUserGroupRepository
    {
        private readonly DataContext _context;
        public UoUserGroupRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoUserGroupModel> GetAllUserGroups()
        {
            List<UoUserGroupModel> userGroupItems = _context.UserGroup.ToList();
            return userGroupItems;
        }

        public async Task<UoUserGroupModel> GetUserGroupById(int id)
        {
            try
            {
                UoUserGroupModel userGroupItem = await _context.UserGroup
                    .Include(userGroup => userGroup.ProjectUserGroups)
                    .SingleAsync(userGroup => userGroup.UserGroupId == id)
                    .ConfigureAwait(false);

                return userGroupItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.UserGroup.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddUserGroup(UoUserGroupModel userGroup)
        {
            if (userGroup is UoUserGroupModel)
            {
                userGroup.UserGroupId = 0;
                _context.UserGroup.Add(userGroup);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return userGroup.UserGroupId;
            }
            return null;
        }

        public async Task<int?> UpdateUserGroupById(int id, JObject patchObject)
        {
            if (await _context.UserGroup.FindAsync(id).ConfigureAwait(false) is UoUserGroupModel userGroup)
            {
                if (patchObject != null)
                {
                    foreach (JProperty property in patchObject.Properties())
                    {
                        switch (property.Name)
                        {
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    userGroup.Name = property.Value.ToString();
                                }
                                break;
                            default:
                                return 0;
                        }
                    }
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> DeleteUserGroupById(int id)
        {
            if (await _context.UserGroup.FindAsync(id).ConfigureAwait(false) is UoUserGroupModel userGroup)
            {
                _context.UserGroup.Remove(userGroup);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
