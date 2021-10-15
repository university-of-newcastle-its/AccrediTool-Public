using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoFieldOfEducationRepository : IUoFieldOfEducationRepository
    {
        private readonly DataContext _context;
        public UoFieldOfEducationRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoFieldOfEducationModel> GetAllFieldsOfEducation()
        {
            List<UoFieldOfEducationModel> fieldOfEducationItems = _context.FieldOfEducation
                .Where(fieldOfEducation => fieldOfEducation.ParentFieldOfEducationId == null)
                .OrderBy(fieldOfEducation => fieldOfEducation.FieldOfEducationId)
                .ToList();
            return fieldOfEducationItems;
        }

        public async Task<UoFieldOfEducationModel> GetFieldOfEducationById(int id)
        {
            try
            {
                UoFieldOfEducationModel fieldOfEducationItem = await _context.FieldOfEducation
                    .Include(fieldOfEducation => fieldOfEducation.ChildFieldsOfEducation)
                    .Include(fieldOfEducation => fieldOfEducation.ParentFieldOfEducation)
                    .Include(fieldOfEducation => fieldOfEducation.Courses)
                    .Include(fieldOfEducation => fieldOfEducation.Programs)
                    
                    .SingleAsync(fieldOfEducation => fieldOfEducation.FieldOfEducationId == id)
                    .ConfigureAwait(false);

                return fieldOfEducationItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.FieldOfEducation.FindAsync(id).ConfigureAwait(false);
            }
        }
    }
}
