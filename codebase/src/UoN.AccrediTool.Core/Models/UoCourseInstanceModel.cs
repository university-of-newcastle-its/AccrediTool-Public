using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    [Table("course_instance")]
    public class UoCourseInstanceModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int CourseInstanceId { get; set; }
        [Required]
        [Column("term_code")]
        public int TermCode { get; set; }
        [Required]
        [Column("year")]
        public int Year { get; set; }
        #endregion

        #region Inverse navigation properties
        public List<UoAssessmentModel> Assessments { get; } = new List<UoAssessmentModel>();
        public bool ShouldSerializeAssessments() { return Assessments.Count > 0; }
        public List<UoLearningOutcomeModel> LearningOutcomes { get; } = new List<UoLearningOutcomeModel>();
        public bool ShouldSerializeLearningOutcomes() { return LearningOutcomes.Count > 0; }
        public List<UoDocumentModel> Documents { get; } = new List<UoDocumentModel>();
        public bool ShouldSerializeDocuments() { return Documents.Count > 0; }
        #endregion

        #region Navigation properties
        [Required]
        [Column("campus")]
        public int CampusId { get; set; }
        public static bool ShouldSerializeCampusId() { return false; }
        public UoCampusModel Campus { get; set; }

        [Required]
        [Column("course")]
        public int CourseId { get; set; }
        public static bool ShouldSerializeCourseId() { return false; }
        public UoCourseModel Course { get; set; }
        #endregion

        public string GetTerm()
        {
            return UoCoreModels.GetTerm(TermCode);
        }
    }
}
