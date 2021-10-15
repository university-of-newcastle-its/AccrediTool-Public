using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("project_user_groups_join")]
    public class UoProjectUserGroupsJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("user_group")]
        public int UserGroupId { get; set; }
        public UoUserGroupModel UserGroup { get; set; }

        [Required]
        [Column("project")]
        public int ProjectId { get; set; }
        public UoProjectModel Project { get; set; }
    }
}
