using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("level_category")]
    public class UoLevelCategoryModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int LevelCategoryId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Inverse navigation properties
        public List<UoLevelModel> Levels { get; } = new List<UoLevelModel>();
        public bool ShouldSerializeLevels() { return Levels.Count > 0; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoLevelCategoryNodesJoin> LevelCategoryNodes { get; } = new List<UoLevelCategoryNodesJoin>();
        #endregion
    }
}
