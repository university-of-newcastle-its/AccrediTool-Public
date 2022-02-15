using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLevelCategoryNodesRepository : IUoLevelCategoryNodesRepository
    {
        private readonly DataContext _context;
        public UoLevelCategoryNodesRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoLevelCategoryNodesJoin> GetAllLevelCategoryNodesJoins()
        {
            List<UoLevelCategoryNodesJoin> levelCategoryNodes = _context.LevelCategoryNodes.ToList();
            return levelCategoryNodes;

        }

        public async Task<int?> AddLevelCategoryNodes(UoLevelCategoryNodesJoin levelCategoryNodes)
        {
            
            if (levelCategoryNodes is UoLevelCategoryNodesJoin)
            {
                if (await _context.FindAsync<UoLevelCategoryNodesJoin>(levelCategoryNodes.Id).ConfigureAwait(false) is null)
                {
                    levelCategoryNodes.Id = 0;
                    _context.Add(levelCategoryNodes);
                    try
                    {
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        return levelCategoryNodes.Id;
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

        public async Task<int?> DeleteLevelCategoryNodesById(int id)
        {
            if (await _context.FindAsync<UoLevelCategoryNodesJoin>(id).ConfigureAwait(false) is UoLevelCategoryNodesJoin levelCategoryNodes)
            {
                _context.Remove(levelCategoryNodes);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
