using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoNodeRepository : IUoNodeRepository
    {
        private readonly DataContext _context;
        public UoNodeRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoNodeModel> GetAllNodes()
        {
            List<UoNodeModel> nodeItems = _context.Node.ToList();
            return nodeItems;
        }

        public async Task<UoNodeModel> GetNodeById(int id)
        {
            try
            {
                UoNodeModel nodeItem = await _context.Node
                    .Include(node => node.ChildNodes)
                    .Include(node => node.Framework)
                    .Include(node => node.ParentNode)
                    .Include(node => node.LevelCategoryNodes)
                        .ThenInclude(join => join.LevelCategory)
                        .ThenInclude(levelCategory => levelCategory.Levels)
                    .Include(node => node.NodeAssessments)
                        .ThenInclude(join => join.Assessment)
                        .ThenInclude(assessment => assessment.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(node => node.NodeCourses)
                        .ThenInclude(join => join.Course)
                        .ThenInclude(course => course.FieldOfEducation)
                    .Include(node => node.NodePrograms)
                        .ThenInclude(join => join.Program)
                        .ThenInclude(program => program.Campus)
                    .Include(node => node.LevelAssessments)
                        .ThenInclude(join => join.Assessment)
                        .ThenInclude(assessment => assessment.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(node => node.LevelCourses)
                        .ThenInclude(join => join.Course)
                        .ThenInclude(course => course.FieldOfEducation)
                    .Include(node => node.LevelCourses)
                        .ThenInclude(join => join.Level)
                        .ThenInclude(level => level.LevelCategory)
                    .Include(node => node.LevelPrograms)
                        .ThenInclude(join => join.Program)
                        .ThenInclude(program => program.Campus)
                    .SingleAsync(node => node.NodeId == id)
                    .ConfigureAwait(false);

                UoNodeModel[] childNodes = new UoNodeModel[nodeItem.ChildNodes.Count];
                nodeItem.ChildNodes.CopyTo(childNodes, 0);
                nodeItem.ChildNodes.Clear();
                nodeItem.ChildNodes.AddRange(await UoCoreRepositories.GetChildNodes(_context, childNodes).ConfigureAwait(false));

                return nodeItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Node.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddNode(UoNodeModel node)
        {
            if (node is UoNodeModel)
            {
                node.NodeId = 0;
                _context.Node.Add(node);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return node.NodeId;
            }
            return null;
        }

        public async Task<int?> SetNodeById(int id, UoNodeModel newNode)
        {
            if (await _context.Node.FindAsync(id).ConfigureAwait(false) is UoNodeModel oldNode)
            {
                if (newNode is UoNodeModel)
                {
                    newNode.NodeId = oldNode.NodeId;
                    _context.Entry(oldNode).CurrentValues.SetValues(newNode);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateNodeById(int id, JObject patchObject)
        {
            if (await _context.Node.FindAsync(id).ConfigureAwait(false) is UoNodeModel node)
            {
                if (patchObject != null)
                {
                    foreach (JProperty property in patchObject.Properties())
                    {
                        switch (property.Name)
                        {
                            case "position":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int position = property.Value.ToObject<int>();
                                    if (position > 0)
                                    {
                                        node.Position = position;
                                    }
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    node.Name = property.Value.ToString();
                                }
                                break;
                            case "nodeCode":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    node.NodeCode = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    node.NodeCode = null;
                                }
                                break;
                            case "description":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    node.Description = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    node.Description = null;
                                }
                                break;
                            case "frameworkId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int frameworkId = property.Value.ToObject<int>();
                                    if (frameworkId > 0)
                                    {
                                        node.FrameworkId = frameworkId;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    node.FrameworkId = null;
                                }
                                break;
                            case "parentNodeId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int parentNodeId = property.Value.ToObject<int>();
                                    if (parentNodeId > 0)
                                    {
                                        node.ParentNodeId = parentNodeId;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    node.ParentNodeId = null;
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

        public async Task<int?> DeleteNodeById(int id)
        {
            if (await _context.Node.FindAsync(id).ConfigureAwait(false) is UoNodeModel node)
            {
                _context.Node.Remove(node);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
