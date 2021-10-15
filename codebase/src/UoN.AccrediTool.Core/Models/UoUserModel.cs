using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("user")]
    public class UoUserModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int UserId { get; set; }
        [Required]
        [Column("login")]
        public string Login { get; set; }
        [NotMapped]
        public List<UoUserGroupModel> UserGroups { get { return UserGroupUsers.Select(join => join.UserGroup).ToList(); } }
        #endregion

        #region Many-to-many relationships
        public List<UoUserGroupUsersJoin> UserGroupUsers { get; } = new List<UoUserGroupUsersJoin>();
        #endregion
    }
}
