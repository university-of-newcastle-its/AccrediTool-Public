using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    [Table("campus")]
    public class UoCampusModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int CampusId { get; set; }
        [Required]
        [Column("campus_code")]
        public string CampusCode { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Inverse navigation properties
        public List<UoCourseInstanceModel> CourseInstances { get; } = new List<UoCourseInstanceModel>();
        public bool ShouldSerializeCourseInstances() { return CourseInstances.Count > 0; }

        public List<UoProgramModel> Programs { get; } = new List<UoProgramModel>();
        public bool ShouldSerializePrograms() { return Programs.Count > 0; }
        #endregion
    }
}
