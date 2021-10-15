using System.Collections.Generic;
using System.Threading.Tasks;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public interface IUoAcademicOrgRepository
    {
        List<UoAcademicOrgModel> GetAllAcademicOrgs();
        Task<UoAcademicOrgModel> GetAcademicOrgById(int id);
        Task<int?> AddAcademicOrg(UoAcademicOrgModel academicOrg);
    }
}
