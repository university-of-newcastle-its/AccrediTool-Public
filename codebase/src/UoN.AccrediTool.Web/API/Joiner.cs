/**------------------------------------------------------------------------
 * ?                                ABOUT
 * @repo           : AccrediTool
 * @description    : This class contains static fucntions that are used to
 *                 : create join tables within the API
 *------------------------------------------------------------------------**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Web.API
{
    public static class Joiner
    {
        /**========================================================================
         **                           CourseToCourseList
         *?  Creates a join table between a given course and courselist
         *@param  course UoCourseModel
         *@param  list UoCourseListModel
         *@param  configuration IConfiguration
         *========================================================================**/
        public static void CourseToCourseList(UoCourseModel course, UoCourseListModel list, IConfiguration configuration)
        {
            if(list != null && course != null)
            {
                string json = new JObject(new JProperty("courseListId", list.CourseListId),
                     new JProperty("courseId", course.CourseId)).ToString();

                API.PostJSON(json, "course-list-courses", configuration);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(new String("Error: "));
                Console.ResetColor();
                Console.WriteLine(new String("Course list or Course passed to joiner was null (CourseToCourseList)"));
            }


        }
        /*---------------------------- END OF FUNCTION ----------------------------*/
        /**========================================================================
         **                           CourseToCourseList
         *?  Creates a join table between a list of levels and courses
         *@param  LevelCoursesJoins List<UoLevelCoursesJoin>
         *@param  configuration IConfiguration
         *========================================================================**/
        public static void CourseToCompToLevels(List<UoLevelCoursesJoin> LevelCoursesJoins, IConfiguration configuration)
        {
            if(LevelCoursesJoins != null)
            {
                for (int i = 0; i < LevelCoursesJoins.Count; i++)
                {

                    string json = new JObject(new JProperty("courseId", LevelCoursesJoins[i].CourseId),
                                       new JProperty("levelId", LevelCoursesJoins[i].LevelId),
                                      new JProperty("nodeId", LevelCoursesJoins[i].NodeId)).ToString();

                    API.PostJSON(json, "level-courses", configuration);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(new String("Error: "));
                Console.ResetColor();
                Console.WriteLine(new String("LevelCoursesJoins passed to joiner was null (CourseToCompToLevels)"));
            }
        }
        /*---------------------------- END OF FUNCTION ----------------------------*/

    }
}
