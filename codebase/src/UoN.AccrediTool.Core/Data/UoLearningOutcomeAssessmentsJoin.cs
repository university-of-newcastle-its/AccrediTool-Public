using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("learning_outcome_assessments_join")]
    public class UoLearningOutcomeAssessmentsJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("assessment")]
        public int AssessmentId { get; set; }
        public UoAssessmentModel Assessment { get; set; }

        [Required]
        [Column("learning_outcome")]
        public int LearningOutcomeId { get; set; }
        public UoLearningOutcomeModel LearningOutcome { get; set; }
    }
}
