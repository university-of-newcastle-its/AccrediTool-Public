using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("user_group_users_join")]
    public class UoUserGroupUsersJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("user")]
        public int UserId { get; set; }
        public UoUserModel User { get; set; }

        [Required]
        [Column("user_group")]
        public int UserGroupId { get; set; }
        public UoUserGroupModel UserGroup { get; set; }        
    }
}
