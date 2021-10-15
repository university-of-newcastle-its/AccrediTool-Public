using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoFrameworkRepository : IUoFrameworkRepository
    {
        private readonly DataContext _context;
        public UoFrameworkRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoFrameworkModel> GetAllFrameworks()
        {
            List<UoFrameworkModel> frameworkItems = _context.Framework.ToList();
            return frameworkItems;
        }

        public async Task<UoFrameworkModel> GetFrameworkById(int id)
        {
            try
            {
                UoFrameworkModel frameworkItem = await _context.Framework
                    .Include(framework => framework.Nodes)
                    .SingleAsync(framework => framework.FrameworkId == id)
                    .ConfigureAwait(false);

                UoNodeModel[] nodes = new UoNodeModel[frameworkItem.Nodes.Count];
                frameworkItem.Nodes.CopyTo(nodes, 0);
                frameworkItem.Nodes.Clear();
                frameworkItem.Nodes.AddRange(await UoCoreRepositories.GetChildNodes(_context, nodes).ConfigureAwait(false));

                return frameworkItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Framework.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddFramework(UoFrameworkModel framework)
        {
            if (framework is UoFrameworkModel)
            {
                framework.FrameworkId = 0;
                _context.Framework.Add(framework);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return framework.FrameworkId;
            }
            return null;
        }

        public async Task<int?> SetFrameworkById(int id, UoFrameworkModel newFramework)
        {
            if (await _context.Framework.FindAsync(id).ConfigureAwait(false) is UoFrameworkModel oldFramework)
            {
                if (newFramework is UoFrameworkModel)
                {
                    newFramework.FrameworkId = oldFramework.FrameworkId;
                    _context.Entry(oldFramework).CurrentValues.SetValues(newFramework);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateFrameworkById(int id, JObject patchObject)
        {
            if (await _context.Framework.FindAsync(id).ConfigureAwait(false) is UoFrameworkModel framework)
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
                                    framework.Name = property.Value.ToString();
                                }
                                break;
                            case "description":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    framework.Description = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    framework.Description = null;
                                }
                                break;
                            case "templateName":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    framework.TemplateName = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    framework.TemplateName = null;
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

        public async Task<int?> DeleteFrameworkById(int id)
        {
            if (await _context.Framework.FindAsync(id).ConfigureAwait(false) is UoFrameworkModel framework)
            {
                _context.Framework.Remove(framework);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
