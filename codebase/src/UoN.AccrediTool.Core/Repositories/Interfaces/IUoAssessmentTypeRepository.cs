using System.Collections.Generic;
using System.Threading.Tasks;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoAssessmentTypeRepository
    {
        List<UoAssessmentTypeModel> GetAllAssessmentTypes();
        Task<UoAssessmentTypeModel> GetAssessmentTypeById(int id);
    }
}
