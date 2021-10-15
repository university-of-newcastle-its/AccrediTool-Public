using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    [Table("program_career")]
    public class UoProgramCareerModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int ProgramCareerId { get; set; }
        [Required]
        [Column("career_code")]
        public string CareerCode { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Inverse navigation properties
        public List<UoProgramModel> Programs { get; } = new List<UoProgramModel>();
        public bool ShouldSerializePrograms() { return Programs.Count > 0; }
        #endregion
    }
}