using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("level")]
    public class UoLevelModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int LevelId { get; set; }
        [Required]
        [Column("position")]
        public int Position { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        #endregion

        #region Navigation properties
        [Required]
        [Column("level_category")]
        public int LevelCategoryId { get; set; }
        public static bool ShouldSerializeLevelCategoryId() { return false; }
        public UoLevelCategoryModel LevelCategory { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoLevelAssessmentsJoin> LevelAssessments { get; } = new List<UoLevelAssessmentsJoin>();
        [JsonIgnore]
        public List<UoLevelCoursesJoin> LevelCourses { get; } = new List<UoLevelCoursesJoin>();
        [JsonIgnore]
        public List<UoLevelProgramsJoin> LevelPrograms { get; } = new List<UoLevelProgramsJoin>();
        #endregion
    }
}
