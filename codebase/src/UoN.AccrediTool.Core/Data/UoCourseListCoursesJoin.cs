using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("course_list_courses_join")]
    public class UoCourseListCoursesJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("course")]
        public int CourseId { get; set; }
        public UoCourseModel Course { get; set; }

        [Required]
        [Column("course_list")]
        public int CourseListId { get; set; }
        public UoCourseListModel CourseList { get; set; }
    }
}
