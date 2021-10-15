using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("node")]
    public class UoNodeModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int NodeId { get; set; }
        [Required]
        [Column("position")]
        public int Position { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("node_code")]
        public string NodeCode { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [NotMapped]
        public UoLevelModel Level { get; set; }
        [NotMapped]
        public List<UoLevelCategoryModel> LevelCategories { get { return LevelCategoryNodes.Select(join => join.LevelCategory).ToList(); } }
        #endregion

        #region Inverse navigation properties
        public List<UoNodeModel> ChildNodes { get; } = new List<UoNodeModel>();
        public bool ShouldSerializeChildNodes() { return ChildNodes.Count > 0; }
        #endregion

        #region Navigation properties
        [Column("framework")]
        public int? FrameworkId { get; set; }
        public static bool ShouldSerializeFrameworkId() { return false; }
        public UoFrameworkModel Framework { get; set; }

        [Column("parent_node")]
        public int? ParentNodeId { get; set; }
        public static bool ShouldSerializeParentNodeId() { return false; }
        public UoNodeModel ParentNode { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoLevelAssessmentsJoin> LevelAssessments { get; } = new List<UoLevelAssessmentsJoin>();
        [JsonIgnore]
        public List<UoLevelCategoryNodesJoin> LevelCategoryNodes { get; } = new List<UoLevelCategoryNodesJoin>();
        [JsonIgnore]
        public List<UoLevelCoursesJoin> LevelCourses { get; } = new List<UoLevelCoursesJoin>();
        [JsonIgnore]
        public List<UoLevelProgramsJoin> LevelPrograms { get; } = new List<UoLevelProgramsJoin>();
        [JsonIgnore]
        public List<UoNodeAssessmentsJoin> NodeAssessments { get; } = new List<UoNodeAssessmentsJoin>();
        [JsonIgnore]
        public List<UoNodeCoursesJoin> NodeCourses { get; } = new List<UoNodeCoursesJoin>();
        [JsonIgnore]
        public List<UoNodeProgramsJoin> NodePrograms { get; } = new List<UoNodeProgramsJoin>();
        #endregion

        public bool IsMapped()
        {
            PropertyInfo[] properties = new PropertyInfo[6]
            {
                GetType().GetProperty("LevelAssessments"),
                GetType().GetProperty("LevelCourses"),
                GetType().GetProperty("LevelPrograms"),
                GetType().GetProperty("NodeAssessments"),
                GetType().GetProperty("NodeCourses"),
                GetType().GetProperty("NodePrograms")
            };

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(this) is IList mapping)
                {
                    if (mapping.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
