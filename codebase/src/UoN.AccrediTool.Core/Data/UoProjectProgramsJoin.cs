using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [Table("project_programs_join")]
    public class UoProjectProgramsJoin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("program")]
        public int ProgramId { get; set; }
        public UoProgramModel Program { get; set; }

        [Required]
        [Column("project")]
        public int ProjectId { get; set; }
        public UoProjectModel Project { get; set; }
    }
}
