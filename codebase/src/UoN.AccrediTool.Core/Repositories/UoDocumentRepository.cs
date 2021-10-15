using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoDocumentRepository : IUoDocumentRepository
    {
        private readonly DataContext _context;
        public UoDocumentRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoDocumentModel> GetAllDocuments()
        {
            List<UoDocumentModel> documentItems = _context.Document.ToList();
            return documentItems;
        }

        public async Task<UoDocumentModel> GetDocumentById(int id)
        {
            try
            {
                UoDocumentModel documentItem = await _context.Document
                    .Include(document => document.Assessment)
                        .Include(assessment => assessment.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(document => document.Course)
                    .Include(document => document.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(document => document.Program)
                    .Include(document => document.Feedback)
                    .SingleAsync(document => document.DocumentId == id)
                    .ConfigureAwait(false);

                return documentItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Document.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddDocument(UoDocumentModel document)
        {
            if (document is UoDocumentModel)
            {
                document.DocumentId = 0;
                _context.Document.Add(document);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return document.DocumentId;
            }
            return null;
        }

        public async Task<int?> SetDocumentById(int id, UoDocumentModel newDocument)
        {
            if (await _context.Document.FindAsync(id).ConfigureAwait(false) is UoDocumentModel oldDocument)
            {
                if (newDocument is UoDocumentModel)
                {
                    newDocument.DocumentId = oldDocument.DocumentId;
                    _context.Entry(oldDocument).CurrentValues.SetValues(newDocument);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateDocumentById(int id, JObject patchObject)
        {
            if (await _context.Document.FindAsync(id).ConfigureAwait(false) is UoDocumentModel document)
            {
                if (patchObject != null)
                {
                    foreach (JProperty property in patchObject.Properties())
                    {
                        switch (property.Name)
                        {
                            case "uri":
                                if (property.Value.Type == JTokenType.Uri)
                                {
                                    document.URI = property.Value.ToObject<System.Uri>();
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    document.Name = property.Value.ToString();
                                }
                                break;
                            case "fileExtension":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    document.FileExtension = property.Value.ToString();
                                }
                                break;
                            case "type":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    document.Type = property.Value.ToObject<UoDocumentType>();
                                }
                                break;
                            case "grade":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    document.Grade = property.Value.ToObject<UoDocumentGrade>();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    document.Grade = null;
                                }
                                break;
                            case "assessmentId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    document.AssessmentId = property.Value.ToObject<int>();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    document.AssessmentId = null;
                                }
                                break;
                            case "courseId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    document.CourseId = property.Value.ToObject<int>();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    document.CourseId = null;
                                }
                                break;
                            case "programId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    document.ProgramId = property.Value.ToObject<int>();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    document.ProgramId = null;
                                }
                                break;
                            case "feedbackId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    document.FeedbackId = property.Value.ToObject<int>();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    document.FeedbackId = null;
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

        public async Task<int?> DeleteDocumentById(int id)
        {
            if (await _context.Document.FindAsync(id).ConfigureAwait(false) is UoDocumentModel document)
            {
                _context.Document.Remove(document);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
