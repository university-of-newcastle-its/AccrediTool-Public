using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("project")]
    public class UoProjectModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int ProjectId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        /*
        [NotMapped]
        public List<UoProgramModel> Programs { get { return ProjectPrograms.Select(join => join.Program).ToList(); } }
        */
        #endregion

        #region Navigation properties
        [Column("framework")]
        public int? FrameworkId { get; set; }
        public static bool ShouldSerializeFrameworkId() { return false; }
        public UoFrameworkModel Framework { get; set; }
        #endregion

        #region Many-to-many relationships
        public List<UoProjectProgramsJoin> ProjectPrograms { get; } = new List<UoProjectProgramsJoin>();
        [JsonIgnore]
        public List<UoProjectUserGroupsJoin> ProjectUserGroups { get; } = new List<UoProjectUserGroupsJoin>();
        #endregion
    }
}
