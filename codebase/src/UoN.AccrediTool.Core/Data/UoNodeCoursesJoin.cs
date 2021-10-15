using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("node_courses_join")]
    public class UoNodeCoursesJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("course")]
        public int CourseId { get; set; }
        public UoCourseModel Course { get; set; }

        [Required]
        [Column("node")]
        public int NodeId { get; set; }
        public UoNodeModel Node { get; set; }
    }
}
