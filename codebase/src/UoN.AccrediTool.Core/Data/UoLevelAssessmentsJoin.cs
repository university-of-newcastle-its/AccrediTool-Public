using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("level_assessments_join")]
    public class UoLevelAssessmentsJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("assessment")]
        public int AssessmentId { get; set; }
        public UoAssessmentModel Assessment { get; set; }

        [Required]
        [Column("level")]
        public int LevelId { get; set; }
        public UoLevelModel Level { get; set; }

        [Required]
        [Column("node")]
        public int NodeId { get; set; }
        public UoNodeModel Node { get; set; }
    }
}
