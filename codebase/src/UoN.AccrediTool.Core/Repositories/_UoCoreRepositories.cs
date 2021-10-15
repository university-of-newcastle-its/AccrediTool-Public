using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public static class UoCoreRepositories
    {
        public static async Task<List<UoNodeModel>> GetChildNodes(DataContext context, UoNodeModel[] nodes, bool levelCategories = true)
        {
            List<UoNodeModel> result = new List<UoNodeModel>();
            if (nodes != null && context != null)
            {

                foreach (UoNodeModel nodeItem in nodes)
                {
                    var node = context.Node.Include(node => node.ChildNodes);
                    if (levelCategories)
                    {
                        node.Include(node => node.LevelCategoryNodes)
                            .ThenInclude(join => join.LevelCategory)
                            .ThenInclude(levelCategory => levelCategory.Levels);
                    }

                    UoNodeModel extractedNode = await node
                        .SingleAsync(node => node.NodeId == nodeItem.NodeId)
                        .ConfigureAwait(false);

                    UoNodeModel[] childNodes = new UoNodeModel[extractedNode.ChildNodes.Count];
                    extractedNode.ChildNodes.CopyTo(childNodes);
                    if (childNodes.Length > 0)
                    {
                        childNodes = childNodes.OrderBy(node => node.Position).ToArray();
                        extractedNode.ChildNodes.Clear();
                        extractedNode.ChildNodes.AddRange(await GetChildNodes(context, childNodes).ConfigureAwait(false));
                    }
                    result.Add(extractedNode);
                }
            }
            return result;
        }
    }
}
