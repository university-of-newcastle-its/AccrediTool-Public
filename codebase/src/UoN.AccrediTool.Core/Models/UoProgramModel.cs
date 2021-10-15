using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("program")]
    public class UoProgramModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int ProgramId { get; set; }
        [Required]
        [Column("program_code")]
        public int ProgramCode { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("first_term_code")]
        public int? FirstTermCode { get; set; }
        [Column("cricos_code")] [JsonProperty("CRICOScode")]
        public string CRICOSCode { get; set; }
        [Column("min_units")]
        public int? MinUnits { get; set; }
        [Column("duration")]
        public float? Duration { get; set; }
        [Column("max_years")]
        public float? MaxYears { get; set; }
        [Column("cwr_type")] [JsonIgnore]
        public string CWRType { get; set; }  // Compulsory Work Requirement type (e.g. Placement, Industrial Experience)
        [Column("cwr_length")] [JsonIgnore]
        public int? CWRLength { get; set; }
        [Column("cwr_length_units")] [JsonIgnore]
        public string CWRLengthUnits { get; set; }  // CWRLengthUnits corresponds with CWRType (e.g. Hours, Weeks)
        [NotMapped]
        public List<Dictionary<string, object>> NodeLevels
        {
            get
            {
                var result = new List<Dictionary<string, object>>();
                var nodeLevels = LevelPrograms.Select(join => new { join.Node, join.Level }).ToList();
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
        public List<UoNodeModel> Nodes { get { return NodePrograms.Select(join => join.Node).ToList(); } }
        [NotMapped]
        public List<UoProjectModel> Projects { get { return ProjectPrograms.Select(join => join.Project).ToList(); } }
        #endregion

        #region Inverse navigation properties
        public List<UoCourseListModel> ProgramStructure { get; } = new List<UoCourseListModel>();
        public bool ShouldSerializeProgramStructure() { return ProgramStructure.Count > 0; }
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
        [Column("program_career")]
        public int ProgramCareerId { get; set; }
        public static bool ShouldSerializeProgramCareerId() { return false; }
        public UoProgramCareerModel ProgramCareer { get; set; }

        [Column("field_of_education")]
        public int? FieldOfEducationId { get; set; }
        public static bool ShouldSerializeFieldOfEducationId() { return false; }
        public UoFieldOfEducationModel FieldOfEducation { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoLevelProgramsJoin> LevelPrograms { get; } = new List<UoLevelProgramsJoin>();
        [JsonIgnore]
        public List<UoNodeProgramsJoin> NodePrograms { get; } = new List<UoNodeProgramsJoin>();
        [JsonIgnore]
        public List<UoProjectProgramsJoin> ProjectPrograms { get; } = new List<UoProjectProgramsJoin>();
        #endregion

        public string GetFirstTerm()
        {
            return UoCoreModels.GetTerm(FirstTermCode);
        }

        public int? GetFirstYear()
        {
            if (FirstTermCode is int termCode)
            {
                return 2000 + ((termCode / 100) - 40);
            }
            return null;
        }

        public int? GetFinalYear()
        {
            if (GetFirstYear() is int firstYear && Duration != null)
            {
                return firstYear + (int)Duration;
            }
            return null;
        }

        public bool IsPartOfProject(int projectId)
        {
            return Projects.Exists(project => project.ProjectId == projectId);
        }
    }
}
