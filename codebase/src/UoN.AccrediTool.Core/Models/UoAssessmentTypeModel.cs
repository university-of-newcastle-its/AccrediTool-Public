using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    [Table("assessment_type")]
    public class UoAssessmentTypeModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")] [JsonProperty("id")]
        public int AssessmentTypeId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        #endregion

        #region Inverse navigation properties
        public List<UoAssessmentModel> Assessments { get; } = new List<UoAssessmentModel>();
        public bool ShouldSerializeAssessments() { return Assessments.Count > 0; }
        #endregion
    }
}
