using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("course_list")]
    public class UoCourseListModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int CourseListId { get; set; }
        [Required]
        [Column("position")]
        public int Position { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("major")]
        public string Major { get; set; }
        [NotMapped]
        public List<UoCourseModel> Courses { get { return CourseListCourses.Select(join => join.Course).OrderBy(course => course.Position).ToList(); } }
        #endregion

        #region Navigation properties
        [Required]
        [Column("program")]
        public int ProgramId { get; set; }
        public static bool ShouldSerializeProgramId() { return false; }
        public UoProgramModel Program { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoCourseListCoursesJoin> CourseListCourses { get; } = new List<UoCourseListCoursesJoin>();
        #endregion
    }
}
