using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoCourseListRepository : IUoCourseListRepository
    {
        private readonly DataContext _context;
        public UoCourseListRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoCourseListModel> GetAllCourseLists()
        {
            List<UoCourseListModel> courseListItems = _context.CourseList.ToList();
            return courseListItems;
        }

        public async Task<UoCourseListModel> GetCourseListById(int id)
        {
            try
            {
                UoCourseListModel courseListItem = await _context.CourseList
                    .Include(courseList => courseList.Program)
                    .Include(courseList => courseList.CourseListCourses)
                        .ThenInclude(join => join.Course)
                    .SingleAsync(courseList => courseList.CourseListId == id)
                    .ConfigureAwait(false);

                return courseListItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.CourseList.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddCourseList(UoCourseListModel courseList)
        {
            if (courseList is UoCourseListModel)
            {
                courseList.CourseListId = 0;
                _context.CourseList.Add(courseList);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return courseList.CourseListId;
            }
            return null;
        }

        public async Task<int?> SetCourseListById(int id, UoCourseListModel newCourseList)
        {
            if (await _context.CourseList.FindAsync(id).ConfigureAwait(false) is UoCourseListModel oldCourseList)
            {
                if (newCourseList is UoCourseListModel)
                {
                    newCourseList.CourseListId = oldCourseList.CourseListId;
                    _context.Entry(oldCourseList).CurrentValues.SetValues(newCourseList);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateCourseListById(int id, JObject patchObject)
        {
            if (await _context.CourseList.FindAsync(id).ConfigureAwait(false) is UoCourseListModel courseList)
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
                                        courseList.Position = position;
                                    }
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    courseList.Name = property.Value.ToString();
                                }
                                break;
                            case "major":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    courseList.Major = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    courseList.Major = null;
                                }
                                break;
                            case "programId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int programId = property.Value.ToObject<int>();
                                    if (programId > 0)
                                    {
                                        courseList.ProgramId = programId;
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

        public async Task<int?> DeleteCourseListById(int id)
        {
            if (await _context.CourseList.FindAsync(id).ConfigureAwait(false) is UoCourseListModel courseList)
            {
                _context.CourseList.Remove(courseList);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
