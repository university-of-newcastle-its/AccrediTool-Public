using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Core.Models
{
    [Table("course")]
    public class UoCourseModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int CourseId { get; set; }
        [Required]
        [Column("subject")]
        public string Subject { get; set; }
        [Required]
        [Column("catalog_number")]
        public string CatalogNumber { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("units")]
        public int Units { get; set; } = 0;
        [Column("position")]
        public int Position { get; set; } = 0;
        [NotMapped]
        public List<UoCourseListModel> CourseLists { get { return CourseListCourses.Select(join => join.CourseList).ToList(); } }
        [NotMapped]
        public List<Dictionary<string, object>> NodeLevels
        { get
            {
                var result = new List<Dictionary<string, object>>();
                var nodeLevels = LevelCourses.Select(join => new { join.Node, join.Level }).ToList();
                foreach (var nodeLevel in nodeLevels)
                {
                    Dictionary<string, object> pair = new Dictionary<string, object> { };
                    pair.Add("node", nodeLevel.Node);
                    pair.Add("level", nodeLevel.Level);
                    result.Add(pair);
                }
                return result;
            }
        }
        [NotMapped]
        public List<UoNodeModel> Nodes { get { return NodeCourses.Select(join => join.Node).ToList(); } }

        [NotMapped]
        private readonly IDictionary<int, string> _LevelDict = new Dictionary<int, string>()
        {
            [1000] = "Introductory",
            [2000] = "Mid-program",
            [3000] = "Senior",
            [4000] = "Advanced",
            [5000] = "Advanced"
        };
        #endregion

        #region Inverse navigation properties
        public List<UoCourseInstanceModel> CourseInstances { get; } = new List<UoCourseInstanceModel>();
        public bool ShouldSerializeCourseInstances() { return CourseInstances.Count > 0; }

        public List<UoDocumentModel> Documents { get; } = new List<UoDocumentModel>();
        public bool ShouldSerializeDocuments() { return Documents.Count > 0; }
        #endregion

        #region Navigation properties
        [Required]
        [Column("academic_org")]
        public int AcademicOrgId { get; set; }
        public static bool ShouldSerializeAcademicOrgId() { return false; }
        public UoAcademicOrgModel AcademicOrg { get; set; }

        [Required]
        [Column("field_of_education")]
        public int FieldOfEducationId { get; set; }
        public static bool ShouldSerializeFieldOfEducationId() { return false; }
        public UoFieldOfEducationModel FieldOfEducation { get; set; }
        #endregion

        #region Many-to-many relationships
        [JsonIgnore]
        public List<UoCourseListCoursesJoin> CourseListCourses { get; } = new List<UoCourseListCoursesJoin>();
        [JsonIgnore]
        public List<UoLevelCoursesJoin> LevelCourses { get; } = new List<UoLevelCoursesJoin>();
        [JsonIgnore]
        public List<UoNodeCoursesJoin> NodeCourses { get; } = new List<UoNodeCoursesJoin>();
        #endregion

        public string GetCourseCode()
        {
            return Subject + CatalogNumber;
        }

        public int GetLevel()
        {
            int number = int.Parse(Regex.Match(CatalogNumber, @"\d+").Value, System.Globalization.NumberStyles.Integer, Thread.CurrentThread.CurrentCulture);
            return (number / 1000) * 1000;
        }

        public string GetLevelName()
        {
            int level = GetLevel();

            if (level < 1000)
            {
                return "Enabling";
            }
            else if (_LevelDict.TryGetValue(level, out string result))
            {
                return result;
            }
            return "Postgraduate";
        }
    }
}
