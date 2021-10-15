using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace UoN.AccrediTool.Core.Models
{
    [Table("framework")]
    public class UoFrameworkModel
    {
        #region Entity properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")] [JsonProperty("id")]
        public int FrameworkId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("template_name")]
        public string TemplateName { get; set; }
        /// <summary>
        /// Defines what a node is called in the context of the framework.
        /// Set to "Node" by default.
        /// </summary>
        [Column("custom_node_noun")]
        public string CustomNodeNoun { get; set; } = "Node";
        /// <summary>
        /// Defines what a group of nodes are called in the context of the framework.
        /// Set to "Sub-nodes" by default.
        /// </summary>
        [Column("custom_node_plural_noun")]
        public string CustomNodePluralNoun { get; set; } = "Sub-nodes";
        /// <summary>
        /// Defines what the framework is called in the context of itself.
        /// Set to "Framework" by default.
        /// Possible custom values could be "Graduate Attributes" or "Student Learning Outcomes".
        /// </summary>
        [Column("custom_framework_noun")]
        public string CustomFrameworkNoun { get; set; } = "Framework";
        #endregion

        #region Inverse navigation properties
        public List<UoNodeModel> Nodes { get; } = new List<UoNodeModel>();
        public bool ShouldSerializeNodes() { return Nodes.Count > 0; }

        public List<UoProjectModel> Projects { get; } = new List<UoProjectModel>();
        public bool ShouldSerializeProjects() { return Projects.Count > 0; }
        #endregion
    }
}
