using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoProjectRepository : IUoProjectRepository
    {
        private readonly DataContext _context;
        public UoProjectRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoProjectModel> GetAllProjects()
        {
            List<UoProjectModel> projectItems = _context.Project.ToList();
            return projectItems;
        }

        public async Task<UoProjectModel> GetProjectById(int id)
        {
            try
            {
                UoProjectModel projectItem = await _context.Project
                    .Include(project => project.Framework)
                        .ThenInclude(framework => framework.Nodes)
                    .Include(project => project.ProjectPrograms)
                    .Include(project => project.ProjectUserGroups)
                    .SingleAsync(project => project.ProjectId == id)
                    .ConfigureAwait(false);

                UoNodeModel[] nodes = new UoNodeModel[projectItem.Framework.Nodes.Count];
                projectItem.Framework.Nodes.CopyTo(nodes, 0);
                projectItem.Framework.Nodes.Clear();
                projectItem.Framework.Nodes.AddRange(await UoCoreRepositories.GetChildNodes(_context, nodes, false).ConfigureAwait(false));

                return projectItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Project.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddProject(UoProjectModel project)
        {
            if (project is UoProjectModel)
            {
                project.ProjectId = 0;
                _context.Project.Add(project);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return project.ProjectId;
            }
            return null;
        }

        public async Task<int?> SetProjectById(int id, UoProjectModel newProject)
        {
            if (await _context.Project.FindAsync(id).ConfigureAwait(false) is UoProjectModel oldProject)
            {
                if (newProject is UoProjectModel)
                {
                    newProject.ProjectId = oldProject.ProjectId;
                    _context.Entry(oldProject).CurrentValues.SetValues(newProject);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateProjectById(int id, JObject patchObject)
        {
            if (await _context.Project.FindAsync(id).ConfigureAwait(false) is UoProjectModel project)
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
                                    project.Name = property.Value.ToString();
                                }
                                break;
                            case "description":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    project.Description = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    project.Description = null;
                                }
                                break;
                            case "frameworkId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int frameworkId = property.Value.ToObject<int>();
                                    if (frameworkId > 0)
                                    {
                                        project.FrameworkId = frameworkId;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    project.FrameworkId = null;
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

        public async Task<int?> DeleteProjectById(int id)
        {
            if (await _context.Project.FindAsync(id).ConfigureAwait(false) is UoProjectModel project)
            {
                _context.Project.Remove(project);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
