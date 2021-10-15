using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    public partial class DataContext
    {
        private static void SeedCampuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UoCampusModel>().HasData
            (
                new UoCampusModel()
                {
                    CampusId = 1,
                    CampusCode = "BCAC",
                    Name = "BCA Singapore"
                },
                new UoCampusModel()
                {
                    CampusId = 2,
                    CampusCode = "BBIC",
                    Name = "Broken Bay Institute"
                },
                new UoCampusModel()
                {
                    CampusId = 3,
                    CampusCode = "CALLA",
                    Name = "Callaghan Campus"
                },
                new UoCampusModel()
                {
                    CampusId = 4,
                    CampusCode = "DEDUC",
                    Name = "Distance Education"
                },
                new UoCampusModel()
                {
                    CampusId = 5,
                    CampusCode = "GOSFC",
                    Name = "Gosford"
                },
                new UoCampusModel()
                {
                    CampusId = 6,
                    CampusCode = "GSCOM",
                    Name = "GradSchool"
                },
                new UoCampusModel()
                {
                    CampusId = 7,
                    CampusCode = "HKMAC",
                    Name = "HKMA - Hong Kong"
                },
                new UoCampusModel()
                {
                    CampusId = 8,
                    CampusCode = "HKUSP",
                    Name = "HKU Space - Hong Kong University"
                },
                new UoCampusModel()
                {
                    CampusId = 9,
                    CampusCode = "NCLE",
                    Name = "Newcastle City Precinct"
                },
                new UoCampusModel()
                {
                    CampusId = 10,
                    CampusCode = "BINUS",
                    Name = "Nurture - BINUS University"
                },
                new UoCampusModel()
                {
                    CampusId = 11,
                    CampusCode = "UTCCC",
                    Name = "Nurture - UTCC"
                },
                new UoCampusModel()
                {
                    CampusId = 12,
                    CampusCode = "ONLIN",
                    Name = "Online"
                },
                new UoCampusModel()
                {
                    CampusId = 13,
                    CampusCode = "CCSTC",
                    Name = "Ourimbah"
                },
                new UoCampusModel()
                {
                    CampusId = 14,
                    CampusCode = "PSBC",
                    Name = "PSB Singapore"
                },
                new UoCampusModel()
                {
                    CampusId = 15,
                    CampusCode = "PMAQU",
                    Name = "Port Macquarie"
                },
                new UoCampusModel()
                {
                    CampusId = 16,
                    CampusCode = "UNEC",
                    Name = "Rural Medicine UNE"
                },
                new UoCampusModel()
                {
                    CampusId = 17,
                    CampusCode = "SYDC",
                    Name = "Sydney"
                }
            );
        }
    }
}
