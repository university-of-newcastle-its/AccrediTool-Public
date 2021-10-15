using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLevelRepository : IUoLevelRepository
    {
        private readonly DataContext _context;
        public UoLevelRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoLevelModel> GetAllLevels()
        {
            List<UoLevelModel> levelItems = _context.Level.ToList();
            return levelItems;
        }

        public async Task<UoLevelModel> GetLevelById(int id)
        {
            try
            {
                UoLevelModel levelItem = await _context.Level
                    .Include(level => level.LevelCategory)
                        .ThenInclude(levelCategory => levelCategory.LevelCategoryNodes)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(level => level.LevelAssessments)
                        .ThenInclude(join => join.Assessment)
                        .ThenInclude(assessment => assessment.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(level => level.LevelCourses)
                        .ThenInclude(join => join.Course)
                    .Include(level => level.LevelPrograms)
                    .SingleAsync(level => level.LevelId == id)
                    .ConfigureAwait(false);

                return levelItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Level.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddLevel(UoLevelModel level)
        {
            if (level is UoLevelModel)
            {
                level.LevelId = 0;
                _context.Level.Add(level);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return level.LevelId;
            }
            return null;
        }

        public async Task<int?> SetLevelById(int id, UoLevelModel newLevel)
        {
            if (await _context.Level.FindAsync(id).ConfigureAwait(false) is UoLevelModel oldLevel)
            {
                if (newLevel is UoLevelModel)
                {
                    newLevel.LevelId = oldLevel.LevelId;
                    _context.Entry(oldLevel).CurrentValues.SetValues(newLevel);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateLevelById(int id, JObject patchObject)
        {
            if (await _context.Level.FindAsync(id).ConfigureAwait(false) is UoLevelModel level)
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
                                        level.Position = position;
                                    }
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    level.Name = property.Value.ToString();
                                }
                                break;
                            case "description":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    level.Description = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    level.Description = null;
                                }
                                break;
                            case "levelCategoryId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int levelCategoryId = property.Value.ToObject<int>();
                                    if (levelCategoryId > 0)
                                    {
                                        level.LevelCategoryId = levelCategoryId;
                                    }
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

        public async Task<int?> DeleteLevelById(int id)
        {
            if (await _context.Level.FindAsync(id).ConfigureAwait(false) is UoLevelModel level)
            {
                _context.Level.Remove(level);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
