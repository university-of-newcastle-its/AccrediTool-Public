using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoLearningOutcomeRepository : IUoLearningOutcomeRepository
    {
        private readonly DataContext _context;
        public UoLearningOutcomeRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoLearningOutcomeModel> GetAllLearningOutcomes()
        {
            List<UoLearningOutcomeModel> learningOutcomeItems = _context.LearningOutcome.ToList();
            return learningOutcomeItems;
        }

        public async Task<UoLearningOutcomeModel> GetLearningOutcomeById(int id)
        {
            try
            {
                UoLearningOutcomeModel learningOutcomeItem = await _context.LearningOutcome
                    .Include(learningOutcome => learningOutcome.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .Include(learningOutcome => learningOutcome.Program)
                    .Include(learningOutcome => learningOutcome.LearningOutcomeAssessments)
                        .ThenInclude(join => join.Assessment)
                    .SingleAsync(learningOutcome => learningOutcome.LearningOutcomeId == id)
                    .ConfigureAwait(false);

                return learningOutcomeItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.LearningOutcome.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddLearningOutcome(UoLearningOutcomeModel learningOutcome)
        {
            if (learningOutcome is UoLearningOutcomeModel)
            {
                learningOutcome.LearningOutcomeId = 0;
                _context.LearningOutcome.Add(learningOutcome);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return learningOutcome.LearningOutcomeId;
            }
            return null;
        }

        public async Task<int?> SetLearningOutcomeById(int id, UoLearningOutcomeModel newLearningOutcome)
        {
            if (await _context.LearningOutcome.FindAsync(id).ConfigureAwait(false) is UoLearningOutcomeModel oldLearningOutcome)
            {
                if (newLearningOutcome is UoLearningOutcomeModel)
                {
                    newLearningOutcome.LearningOutcomeId = oldLearningOutcome.LearningOutcomeId;
                    _context.Entry(oldLearningOutcome).CurrentValues.SetValues(newLearningOutcome);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateLearningOutcomeById(int id, JObject patchObject)
        {
            if (await _context.LearningOutcome.FindAsync(id).ConfigureAwait(false) is UoLearningOutcomeModel learningOutcome)
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
                                        learningOutcome.Position = position;
                                    }
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    learningOutcome.Name = property.Value.ToString();
                                }
                                break;
                            case "courseInstanceId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int courseInstanceId = property.Value.ToObject<int>();
                                    if (courseInstanceId > 0)
                                    {
                                        learningOutcome.CourseInstanceId = courseInstanceId;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    learningOutcome.CourseInstanceId = null;
                                }
                                break;
                            case "programId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int programId = property.Value.ToObject<int>();
                                    if (programId > 0)
                                    {
                                        learningOutcome.ProgramId = programId;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    learningOutcome.ProgramId = null;
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

        public async Task<int?> DeleteLearningOutcomeById(int id)
        {
            if (await _context.LearningOutcome.FindAsync(id).ConfigureAwait(false) is UoLearningOutcomeModel learningOutcome)
            {
                _context.LearningOutcome.Remove(learningOutcome);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
