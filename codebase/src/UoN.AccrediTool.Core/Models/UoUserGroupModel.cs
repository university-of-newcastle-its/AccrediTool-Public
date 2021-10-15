using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("user_group")]
    public class UoUserGroupModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int UserGroupId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoProjectUserGroupsJoin> ProjectUserGroups { get; } = new List<UoProjectUserGroupsJoin>();
        [JsonIgnore]
        public List<UoUserGroupUsersJoin> UserGroupUsers { get; } = new List<UoUserGroupUsersJoin>();
        #endregion
    }
}
