using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoCourseInstanceRepository : IUoCourseInstanceRepository
    {
        private readonly DataContext _context;
        public UoCourseInstanceRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoCourseInstanceModel> GetAllCourseInstances()
        {
            List<UoCourseInstanceModel> courseInstanceItems = _context.CourseInstance.ToList();
            return courseInstanceItems;
        }

        public async Task<UoCourseInstanceModel> GetCourseInstanceById(int id)
        {
            try
            {
                UoCourseInstanceModel courseInstanceItem = await _context.CourseInstance
                    .Include(courseInstance => courseInstance.Assessments)
                        .ThenInclude(assessments => assessments.LearningOutcomeAssessments)
                        .ThenInclude(join => join.LearningOutcome)
                    .Include(courseInstance => courseInstance.Assessments)
                        .ThenInclude(assessments => assessments.Documents)
                        .ThenInclude(documents => documents.Feedback)
                    .Include(courseInstance => courseInstance.Assessments)
                        .ThenInclude(assessments => assessments.AssessmentType)
                    .Include(courseInstance => courseInstance.Assessments)
                        .ThenInclude(assessments => assessments.LevelAssessments)
                        .ThenInclude(join => join.Level)
                        .ThenInclude(level => level.LevelCategory)
                        .ThenInclude(levelCategory => levelCategory.LevelCategoryNodes)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(courseInstance => courseInstance.Assessments)
                        .ThenInclude(assessments => assessments.NodeAssessments)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(courseInstance => courseInstance.LearningOutcomes)
                    .Include(courseInstance => courseInstance.Documents)
                    .Include(courseInstance => courseInstance.Campus)
                    .Include(courseInstance => courseInstance.Course)
                    .SingleAsync(courseInstance => courseInstance.CourseInstanceId == id)
                    .ConfigureAwait(false);

                return courseInstanceItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.CourseInstance.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddCourseInstance(UoCourseInstanceModel courseInstance)
        {
            if (courseInstance is UoCourseInstanceModel)
            {
                courseInstance.CourseInstanceId = 0;
                _context.CourseInstance.Add(courseInstance);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return courseInstance.CourseInstanceId;
            }
            return null;
        }

        public async Task<int?> SetCourseInstanceById(int id, UoCourseInstanceModel newCourseInstance)
        {
            if (await _context.CourseInstance.FindAsync(id).ConfigureAwait(false) is UoCourseInstanceModel oldCourseInstance)
            {
                if (newCourseInstance is UoCourseInstanceModel)
                {
                    newCourseInstance.CourseInstanceId = oldCourseInstance.CourseInstanceId;
                    _context.Entry(oldCourseInstance).CurrentValues.SetValues(newCourseInstance);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateCourseInstanceById(int id, JObject patchObject)
        {
            if (await _context.CourseInstance.FindAsync(id).ConfigureAwait(false) is UoCourseInstanceModel courseInstance)
            {
                if (patchObject != null)
                {
                    foreach (JProperty property in patchObject.Properties())
                    {
                        switch (property.Name)
                        {
                            case "termCode":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int termCode = property.Value.ToObject<int>();
                                    if (termCode > 0)
                                    {
                                        courseInstance.TermCode = termCode;
                                    }
                                }
                                break;
                            case "year":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int year = property.Value.ToObject<int>();
                                    if (year > 2000)
                                    {
                                        courseInstance.Year = year;
                                    }
                                }
                                break;
                            case "campusId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int campusId = property.Value.ToObject<int>();
                                    if (campusId > 0)
                                    {
                                        courseInstance.CampusId = campusId;
                                    }
                                }
                                break;
                            case "courseId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int courseId = property.Value.ToObject<int>();
                                    if (courseId > 0)
                                    {
                                        courseInstance.CourseId = courseId;
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

        public async Task<int?> DeleteCourseInstanceById(int id)
        {
            if (await _context.CourseInstance.FindAsync(id).ConfigureAwait(false) is UoCourseInstanceModel courseInstance)
            {
                _context.CourseInstance.Remove(courseInstance);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }

        public async void DeleteCourseInstanceByCourseId(int id)
        {
            List<UoCourseInstanceModel> courseInstanceModels = _context.CourseInstance.Where(CourseInstance => CourseInstance.CourseId == id).ToList();

            
            foreach(UoCourseInstanceModel courseInstance in courseInstanceModels)
            {
                await DeleteCourseInstanceById(courseInstance.CourseInstanceId);
            }

    
        }
    }
}
