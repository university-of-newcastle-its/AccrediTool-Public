using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("learning_outcome")]
    public class UoLearningOutcomeModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int LearningOutcomeId { get; set; }
        [Required]
        [Column("position")]
        public int Position { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Navigation properties
        [Column("course_instance")]
        public int? CourseInstanceId { get; set; }
        public static bool ShouldSerializeCourseInstanceId() { return false; }
        public UoCourseInstanceModel CourseInstance { get; set; }

        [Column("program")]
        public int? ProgramId { get; set; }
        public static bool ShouldSerializeProgramId() { return false; }
        public UoProgramModel Program { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoLearningOutcomeAssessmentsJoin> LearningOutcomeAssessments { get; } = new List<UoLearningOutcomeAssessmentsJoin>();
        #endregion
    }
}
