using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoAcademicOrgRepository : IUoAcademicOrgRepository
    {
        private readonly DataContext _context;
        public UoAcademicOrgRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoAcademicOrgModel> GetAllAcademicOrgs()
        {
            List<UoAcademicOrgModel> academicOrgItems = _context.AcademicOrg.ToList();
            return academicOrgItems;
        }

        public async Task<UoAcademicOrgModel> GetAcademicOrgById(int id)
        {
            try
            {
                UoAcademicOrgModel academicOrgItem = await _context.AcademicOrg
                    .Include(academicOrg => academicOrg.Courses)
                    .SingleAsync(academicOrg => academicOrg.AcademicOrgId == id)
                    .ConfigureAwait(false);

                return academicOrgItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.AcademicOrg.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddAcademicOrg(UoAcademicOrgModel academicOrg)
        {
            if (academicOrg is UoAcademicOrgModel)
            {
                academicOrg.AcademicOrgId = 0;
                _context.AcademicOrg.Add(academicOrg);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return academicOrg.AcademicOrgId;
            }
            return null;
        }
    }
}
