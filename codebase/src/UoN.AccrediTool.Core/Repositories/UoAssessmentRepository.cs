using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoAssessmentRepository : IUoAssessmentRepository
    {
        private readonly DataContext _context;
        public UoAssessmentRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoAssessmentModel> GetAllAssessments()
        {
            List<UoAssessmentModel> assessmentItems = _context.Assessment.ToList();
            return assessmentItems;
        }

        public async Task<UoAssessmentModel> GetAssessmentById(int id)
        {
            try
            {
                UoAssessmentModel assessmentItem = await _context.Assessment
                    .Include(assessment => assessment.Documents)
                    .Include(assessment => assessment.AssessmentType)
                    .Include(assessment => assessment.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(assessment => assessment.LearningOutcomeAssessments)
                        .ThenInclude(join => join.LearningOutcome)
                    .Include(assessment => assessment.LevelAssessments)
                        .ThenInclude(join => join.Level)
                    .Include(assessment => assessment.LevelAssessments)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(assessment => assessment.NodeAssessments)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .SingleAsync(assessment => assessment.AssessmentId == id)
                    .ConfigureAwait(false);

                return assessmentItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Assessment.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddAssessment(UoAssessmentModel assessment)
        {
            if (assessment is UoAssessmentModel)
            {
                assessment.AssessmentId = 0;
                _context.Assessment.Add(assessment);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return assessment.AssessmentId;
            }
            return null;
        }

        public async Task<int?> SetAssessmentById(int id, UoAssessmentModel newAssessment)
        {
            if (await _context.Assessment.FindAsync(id).ConfigureAwait(false) is UoAssessmentModel oldAssessment)
            {
                if (newAssessment is UoAssessmentModel)
                {
                    newAssessment.AssessmentId = oldAssessment.AssessmentId;
                    _context.Entry(oldAssessment).CurrentValues.SetValues(newAssessment);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateAssessmentById(int id, JObject patchObject)
        {
            if (await _context.Assessment.FindAsync(id).ConfigureAwait(false) is UoAssessmentModel assessment)
            {
                if (patchObject != null)
                {
                    foreach (JProperty property in patchObject.Properties())
                    {
                        switch (property.Name)
                        {
                            case "position":
                                int position = property.Value.ToObject<int>();
                                if (position > 0)
                                {
                                    assessment.Position = position;
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    assessment.Name = property.Value.ToString();
                                }
                                break;
                            case "purpose":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    assessment.Purpose = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    assessment.Purpose = null;
                                }
                                break;
                            case "description":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    assessment.Description = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    assessment.Description = null;
                                }
                                break;
                            case "weight":
                                if (property.Value.Type == JTokenType.Float)
                                {
                                    float weight = property.Value.ToObject<float>();
                                    if (weight >= 0)
                                    {
                                        assessment.Weight = weight;
                                    }
                                }
                                break;
                            case "assessmentTypeId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int assessmentTypeId = property.Value.ToObject<int>();
                                    if (assessmentTypeId > 0)
                                    {
                                        assessment.AssessmentTypeId = assessmentTypeId;
                                    }
                                }
                                break;
                            case "courseInstanceId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int courseInstanceId = property.Value.ToObject<int>();
                                    if (courseInstanceId > 0)
                                    {
                                        assessment.CourseInstanceId = courseInstanceId;
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

        public async Task<int?> DeleteAssessmentById(int id)
        {
            if (await _context.Assessment.FindAsync(id).ConfigureAwait(false) is UoAssessmentModel assessment)
            {
                _context.Assessment.Remove(assessment);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null; 
        }
    }
}
