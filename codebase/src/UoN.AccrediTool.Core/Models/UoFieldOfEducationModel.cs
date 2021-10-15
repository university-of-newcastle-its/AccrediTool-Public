using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    public enum UoFieldOfEducationType
    {
        Broad,
        Narrow,
        Detailed
    }

    [Table("field_of_education")]
    public class UoFieldOfEducationModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int FieldOfEducationId { get; set; }
        [Required]
        [Column("field_code")]
        public string FieldCode { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("type")]
        public UoFieldOfEducationType Type { get; set; }
        [Column("description")]
        public string Description { get; set; }
        #endregion

        #region Inverse navigation properties
        public List<UoCourseModel> Courses { get; } = new List<UoCourseModel>();
        public bool ShouldSerializeCourses() { return Courses.Count > 0; }

        public List<UoProgramModel> Programs { get; } = new List<UoProgramModel>();
        public bool ShouldSerializePrograms() { return Programs.Count > 0; }

        public List<UoFieldOfEducationModel> ChildFieldsOfEducation { get; } = new List<UoFieldOfEducationModel>();
        public bool ShouldSerializeChildFieldsOfEducation() { return ChildFieldsOfEducation.Count > 0; }
        #endregion

        #region Navigation properties
        [Column("parent_field_of_education")]
        public int? ParentFieldOfEducationId { get; set; }
        public static bool ShouldSerializeParentFieldOfEducationId() { return false; }
        public UoFieldOfEducationModel ParentFieldOfEducation { get; set; }
        #endregion
    }
}
