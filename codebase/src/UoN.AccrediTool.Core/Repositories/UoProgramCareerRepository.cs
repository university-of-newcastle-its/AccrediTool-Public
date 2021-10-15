using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoProgramCareerRepository : IUoProgramCareerRepository
    {
        private readonly DataContext _context;
        public UoProgramCareerRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoProgramCareerModel> GetAllProgramCareers()
        {
            List<UoProgramCareerModel> programCareerItems = _context.ProgramCareer.ToList();
            return programCareerItems;
        }

        public async Task<UoProgramCareerModel> GetProgramCareerById(int id)
        {
            try
            {
                UoProgramCareerModel programCareerItem = await _context.ProgramCareer
                    .Include(programCareer => programCareer.Programs)
                    .SingleAsync(programCareer => programCareer.ProgramCareerId == id)
                    .ConfigureAwait(false);

                return programCareerItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.ProgramCareer.FindAsync(id).ConfigureAwait(false);
            }
        }
    }
}
