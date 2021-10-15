using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    public enum UoDocumentType
    {
        Documentation,
        WorkSample,
        AssessmentItem,
        Feedback,
        MarkingRubric,
        CourseOutline,
        CourseDetailReport
    }

    public enum UoDocumentGrade
    {
        FF, P, C, D, HD
    }

    [Table("document")]
    public class UoDocumentModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int DocumentId { get; set; }
        [Required]
        [Column("uri")] [JsonProperty("uri")]
        public System.Uri URI { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("file_extension")]
        public string FileExtension { get; set; }
        [Required]
        [Column("type")]
        public UoDocumentType Type { get; set; }
        [Column("grade")]
        public UoDocumentGrade? Grade { get; set; }

        [NotMapped]
        private readonly IDictionary<UoDocumentType, string> _TypeDict = new Dictionary<UoDocumentType, string>()
        {
            [UoDocumentType.Documentation] = "Documentation",
            [UoDocumentType.WorkSample] = "Work Sample",
            [UoDocumentType.AssessmentItem] = "Assessment Item",
            [UoDocumentType.Feedback] = "Feedback",
            [UoDocumentType.MarkingRubric] = "Marking Rubric",
            [UoDocumentType.CourseOutline] = "Course Outline",
            [UoDocumentType.CourseDetailReport] = "Course Detail Report"
        };
        #endregion

        #region Navigation properties
        [Column("assessment")]
        public int? AssessmentId { get; set; }
        public static bool ShouldSerializeAssessmentId() { return false; }
        public UoAssessmentModel Assessment { get; set; }

        [Column("course")]
        public int? CourseId { get; set; }
        public static bool ShouldSerializeCourseId() { return false; }
        public UoCourseModel Course { get; set; }

        [Column("course_instance")]
        public int? CourseInstanceId { get; set; }
        public static bool ShouldSerializeCourseInstanceId() { return false; }
        public UoCourseInstanceModel CourseInstance { get; set; }

        [Column("program")]
        public int? ProgramId { get; set; }
        public static bool ShouldSerializeProgramId() { return false; }
        public UoProgramModel Program { get; set; }

        [Column("feedback")]
        public int? FeedbackId { get; set; }
        public static bool ShouldSerializeFeedbackId() { return false; }
        public UoDocumentModel Feedback { get; set; }
        #endregion

        public string GetDocumentType()
        {
            if (_TypeDict.TryGetValue(Type, out string result))
            {
                return result;
            }
            return "Unknown";
        }
    }
}
