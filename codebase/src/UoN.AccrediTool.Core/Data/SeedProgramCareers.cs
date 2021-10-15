using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    public partial class DataContext
    {
        private static void SeedProgramCareers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UoProgramCareerModel>().HasData
            (
                new UoProgramCareerModel()
                {
                    ProgramCareerId = 1,
                    CareerCode = "ENAB",
                    Name = "Enabling"
                },
                new UoProgramCareerModel()
                {
                    ProgramCareerId = 2,
                    CareerCode = "PGCW",
                    Name = "Postgraduate Coursework"
                },
                new UoProgramCareerModel()
                {
                    ProgramCareerId = 3,
                    CareerCode = "RSCH",
                    Name = "Research"
                },
                new UoProgramCareerModel()
                {
                    ProgramCareerId = 4,
                    CareerCode = "UGRD",
                    Name = "Undergraduate"
                }
            );
        }
    }
}
