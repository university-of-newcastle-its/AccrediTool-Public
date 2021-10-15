using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoAssessmentTypeRepository : IUoAssessmentTypeRepository
    {
        private readonly DataContext _context;
        public UoAssessmentTypeRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoAssessmentTypeModel> GetAllAssessmentTypes()
        {
            List<UoAssessmentTypeModel> assessmentTypeItems = _context.AssessmentType.ToList();
            return assessmentTypeItems;
        }

        public async Task<UoAssessmentTypeModel> GetAssessmentTypeById(int id)
        {
            try
            {
                UoAssessmentTypeModel assessmentTypeItem = await _context.AssessmentType
                    .Include(assessmentType => assessmentType.Assessments)
                        .ThenInclude(assessment => assessment.CourseInstance)
                        .ThenInclude(courseInstance => courseInstance.Course)
                    .SingleAsync(assessmentType => assessmentType.AssessmentTypeId == id)
                    .ConfigureAwait(false);

                return assessmentTypeItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.AssessmentType.FindAsync(id).ConfigureAwait(false);
            }
        }
    }
}
