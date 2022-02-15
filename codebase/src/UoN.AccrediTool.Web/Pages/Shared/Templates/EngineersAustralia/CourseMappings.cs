using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UoN.AccrediTool.Core.Models;
using UoN.AccrediTool.Core.Data;

namespace UoN.AccrediTool.Web.Pages.Shared.Templates.EngineersAustralia
{
    public class CourseMappings
    {
        public int NodeId { get; set; }
        public bool Taught { get; set; }
        public bool Practiced { get; set; }
        public bool Assessed { get; set; }
        public int Level { get; set; }

        public List<UoLevelCoursesJoin> GetLevelCoursesJoins()
        {
            return null;
        }

        
    }
}
