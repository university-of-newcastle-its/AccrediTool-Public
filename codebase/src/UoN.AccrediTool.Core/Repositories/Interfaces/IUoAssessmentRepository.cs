using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoAssessmentRepository
    {
        List<UoAssessmentModel> GetAllAssessments();
        Task<UoAssessmentModel> GetAssessmentById(int id);
        Task<int?> AddAssessment(UoAssessmentModel assessment);
        Task<int?> SetAssessmentById(int id, UoAssessmentModel assessment);
        Task<int?> UpdateAssessmentById(int id, JObject patchObject);
        Task<int?> DeleteAssessmentById(int id);
    }
}
