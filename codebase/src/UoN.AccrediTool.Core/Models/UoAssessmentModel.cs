using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("assessment")]
    public class UoAssessmentModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int AssessmentId { get; set; }
        [Required]
        [Column("position")]
        public int Position { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("purpose")]
        public string Purpose { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("weight")]
        public float Weight { get; set; }
        [NotMapped]
        public List<UoLearningOutcomeModel> LearningOutcomes { get { return LearningOutcomeAssessments.Select(join => join.LearningOutcome).ToList(); } }
        [NotMapped]
        public List<Dictionary<string, object>> NodeLevels
        { get
            {
                var result = new List<Dictionary<string, object>>();
                var nodeLevels = LevelAssessments.Select(join => new { join.Node, join.Level }).ToList();
                foreach (var nodeLevel in nodeLevels)
                {
                    Dictionary<string, object> pair = new Dictionary<string, object> { };
                    pair.Add("node", nodeLevel.Node);
                    pair.Add("level", nodeLevel.Level);
                    result.Add(pair);
                }
                return result;
            }
        }
        [NotMapped]
        public List<UoNodeModel> Nodes { get { return NodeAssessments.Select(join => join.Node).ToList(); } }
        #endregion

        #region Inverse navigation properties
        public List<UoDocumentModel> Documents { get; } = new List<UoDocumentModel>();
        public bool ShouldSerializeDocuments() { return Documents.Count > 0; }
        #endregion

        #region Navigation properties
        [Required]
        [Column("assessment_type")]
        public int AssessmentTypeId { get; set; }
        public static bool ShouldSerializeAssessmentTypeId() { return false; }
        public UoAssessmentTypeModel AssessmentType { get; set; }

        [Required]
        [Column("course_instance")]
        public int CourseInstanceId { get; set; }
        public static bool ShouldSerializeCourseInstanceId() { return false; }
        public UoCourseInstanceModel CourseInstance { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoLearningOutcomeAssessmentsJoin> LearningOutcomeAssessments { get; } = new List<UoLearningOutcomeAssessmentsJoin>();
        [JsonIgnore]
        public List<UoLevelAssessmentsJoin> LevelAssessments { get; } = new List<UoLevelAssessmentsJoin>();
        [JsonIgnore]
        public List<UoNodeAssessmentsJoin> NodeAssessments { get; } = new List<UoNodeAssessmentsJoin>();
        #endregion

        public string GetWeightOrFormative()
        {
            return Weight > 0 ? Weight.ToString(Thread.CurrentThread.CurrentCulture) + "%" : "Formative";
        }
    }
}
