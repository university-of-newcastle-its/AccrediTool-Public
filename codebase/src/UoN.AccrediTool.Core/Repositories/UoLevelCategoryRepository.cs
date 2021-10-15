using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLevelCategoryRepository : IUoLevelCategoryRepository
    {
        private readonly DataContext _context;
        public UoLevelCategoryRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoLevelCategoryModel> GetAllLevelCategories()
        {
            List<UoLevelCategoryModel> levelCategoryItems = _context.LevelCategory.ToList();
            return levelCategoryItems;
        }

        public async Task<UoLevelCategoryModel> GetLevelCategoryById(int id)
        {
            try
            {
                UoLevelCategoryModel levelCategoryItem = await _context.LevelCategory
                    .Include(levelCategory => levelCategory.Levels)
                    .Include(levelCategory => levelCategory.LevelCategoryNodes)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .SingleAsync(levelCategory => levelCategory.LevelCategoryId == id)
                    .ConfigureAwait(false);

                return levelCategoryItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.LevelCategory.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddLevelCategory(UoLevelCategoryModel levelCategory)
        {
            if (levelCategory is UoLevelCategoryModel)
            {
                levelCategory.LevelCategoryId = 0;
                _context.LevelCategory.Add(levelCategory);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return levelCategory.LevelCategoryId;
            }
            return null;
        }

        public async Task<int?> SetLevelCategoryById(int id, UoLevelCategoryModel newLevelCategory)
        {
            if (await _context.LevelCategory.FindAsync(id).ConfigureAwait(false) is UoLevelCategoryModel oldLevelCategory)
            {
                if (newLevelCategory is UoLevelCategoryModel)
                {
                    newLevelCategory.LevelCategoryId = oldLevelCategory.LevelCategoryId;
                    _context.Entry(oldLevelCategory).CurrentValues.SetValues(newLevelCategory);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateLevelCategoryById(int id, JObject patchObject)
        {
            if (await _context.LevelCategory.FindAsync(id).ConfigureAwait(false) is UoLevelCategoryModel levelCategory)
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
                                    levelCategory.Name = property.Value.ToString();
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

        public async Task<int?> DeleteLevelCategoryById(int id)
        {
            if (await _context.LevelCategory.FindAsync(id).ConfigureAwait(false) is UoLevelCategoryModel levelCategory)
            {
                _context.LevelCategory.Remove(levelCategory);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
