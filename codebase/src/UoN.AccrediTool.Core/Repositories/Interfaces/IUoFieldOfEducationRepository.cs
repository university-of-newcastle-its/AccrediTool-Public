using System.Collections.Generic;
using System.Threading.Tasks;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoFieldOfEducationRepository
    {
        List<UoFieldOfEducationModel> GetAllFieldsOfEducation();
        Task<UoFieldOfEducationModel> GetFieldOfEducationById(int id);
    }
}
