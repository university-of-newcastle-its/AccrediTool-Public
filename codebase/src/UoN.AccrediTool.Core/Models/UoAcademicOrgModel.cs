using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    [Table("academic_org")]
    public class UoAcademicOrgModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int AcademicOrgId { get; set; }
        [Required]
        [Column("acad_code")]
        public string AcadCode { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        #endregion

        #region Inverse navigation properties
        public List<UoCourseModel> Courses { get; } = new List<UoCourseModel>();
        public bool ShouldSerializeCourses() { return Courses.Count > 0; }
        #endregion
    }
}
