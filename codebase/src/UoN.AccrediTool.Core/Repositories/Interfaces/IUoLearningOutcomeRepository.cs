using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoLearningOutcomeRepository
    {
        List<UoLearningOutcomeModel> GetAllLearningOutcomes();
        Task<UoLearningOutcomeModel> GetLearningOutcomeById(int id);
        Task<int?> AddLearningOutcome(UoLearningOutcomeModel learningOutcome);
        Task<int?> SetLearningOutcomeById(int id, UoLearningOutcomeModel learningOutcome);
        Task<int?> UpdateLearningOutcomeById(int id, JObject patchObject);
        Task<int?> DeleteLearningOutcomeById(int id);
    }
}
