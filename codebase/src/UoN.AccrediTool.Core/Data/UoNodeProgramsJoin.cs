using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("node_programs_join")]
    public class UoNodeProgramsJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("program")]
        public int ProgramId { get; set; }
        public UoProgramModel Program { get; set; }

        [Required]
        [Column("node")]
        public int NodeId { get; set; }
        public UoNodeModel Node { get; set; }
    }
}
