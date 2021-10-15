using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoDocumentRepository
    {
        List<UoDocumentModel> GetAllDocuments();
        Task<UoDocumentModel> GetDocumentById(int id);
        Task<int?> AddDocument(UoDocumentModel document);
        Task<int?> SetDocumentById(int id, UoDocumentModel document);
        Task<int?> UpdateDocumentById(int id, JObject patchObject);
        Task<int?> DeleteDocumentById(int id);
    }
}
