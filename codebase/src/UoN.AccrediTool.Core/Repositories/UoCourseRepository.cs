using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoCourseRepository : IUoCourseRepository
    {
        private readonly DataContext _context;
        public UoCourseRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoCourseModel> GetAllCourses()
        {
            List<UoCourseModel> courseItems = _context.Course.ToList();
            return courseItems;
        }

        public async Task<UoCourseModel> GetCourseById(int id)
        {
            try
            {
                UoCourseModel courseItem = await _context.Course
                    .Include(course => course.CourseInstances)
                        .ThenInclude(courseInstance => courseInstance.Campus)
                    .Include(course => course.Documents)
                    .Include(course => course.FieldOfEducation)
                    .Include(course => course.AcademicOrg)
                    .Include(course => course.CourseListCourses)
                        .ThenInclude(join => join.CourseList)
                        .ThenInclude(courseList => courseList.Program)
                    .Include(course => course.LevelCourses)
                        .ThenInclude(join => join.Level)
                        .ThenInclude(level => level.LevelCategory)
                    .Include(course => course.LevelCourses)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(course => course.LevelCourses)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.ParentNode)
                    .Include(course => course.NodeCourses)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(course => course.NodeCourses)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.ParentNode)
                        .ThenInclude(parentNode => parentNode.Framework)
                    .SingleAsync(course => course.CourseId == id)
                    .ConfigureAwait(false);

                return courseItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Course.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddCourse(UoCourseModel course)
        {
            if (course is UoCourseModel)
            {
                course.CourseId = 0;
                _context.Course.Add(course);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return course.CourseId;
            }
            return null;
        }

        public async Task<int?> SetCourseById(int id, UoCourseModel newCourse)
        {
            if (await _context.Course.FindAsync(id).ConfigureAwait(false) is UoCourseModel oldCourse)
            {
                if (newCourse is UoCourseModel)
                {
                    newCourse.CourseId = oldCourse.CourseId;
                    _context.Entry(oldCourse).CurrentValues.SetValues(newCourse);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateCourseById(int id, JObject patchObject)
        {
            if (await _context.Course.FindAsync(id).ConfigureAwait(false) is UoCourseModel course)
            {
                if (patchObject != null)
                {
                    foreach (JProperty property in patchObject.Properties())
                    {
                        switch (property.Name)
                        {
                            case "subject":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    course.Subject = property.Value.ToString();
                                }
                                break;
                            case "catalogNumber":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    course.CatalogNumber = property.Value.ToString();
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    course.Name = property.Value.ToString();
                                }
                                break;
                            case "description":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    course.Description = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    course.Description = null;
                                }
                                break;
                            case "units":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int units = property.Value.ToObject<int>();
                                    course.Units = units < 0 ? 0 : units;
                                }
                                break;
                            case "position":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int position = property.Value.ToObject<int>();
                                    course.Position = position < 0 ? 0 : position;
                                }
                                break;
                            case "academicOrgId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int academicOrgId = property.Value.ToObject<int>();
                                    if (academicOrgId > 0)
                                    {
                                        course.AcademicOrgId = academicOrgId;
                                    }
                                }
                                break;
                            case "fieldOfEducationId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int fieldOfEducationId = property.Value.ToObject<int>();
                                    if (fieldOfEducationId > 0)
                                    {
                                        course.FieldOfEducationId = fieldOfEducationId;
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

        public async Task<int?> DeleteCourseById(int id)
        {
            if (await _context.Course.FindAsync(id).ConfigureAwait(false) is UoCourseModel course)
            {
                _context.Course.Remove(course);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
