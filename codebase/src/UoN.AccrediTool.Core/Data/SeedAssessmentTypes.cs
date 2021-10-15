using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    public partial class DataContext
    {
        private static void SeedAssessmentTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UoAssessmentTypeModel>().HasData
            (
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 102,
                    Name = "Formal Examination",
                    Description = "Examination held in formal examination period."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 103,
                    Name = "In Term Test",
                    Description = "Examination or test held outside the formal examination period."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 104,
                    Name = "Quiz",
                    Description = "Non-invigilated response to a number of questions which may include multiple choice questions and/or short answers."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 105,
                    Name = "Essay",
                    Description = "An extended written response to a set question, problem or issue."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 106,
                    Name = "Presentation",
                    Description = "The process of showing and/or explaining content to an audience."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 107,
                    Name = "Professional Task",
                    Description = "Student engages in a professional activity in context or for a real-world audience."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 108,
                    Name = "Practical Demonstration",
                    Description = "A demonstration of technical/professional skills."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 109,
                    Name = "Participation",
                    Description = "Student engagement with content, either face to face or online, according to explicit criteria."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 110,
                    Name = "Project",
                    Description = "A tangible product where theory is applied to produce a model, a design, a program, a composition or other creative work."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 111,
                    Name = "Report",
                    Description = "A structured analytical account of a project, investigation or process."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 112,
                    Name = "Journal",
                    Description = "An evaluation of student's own learning that includes narrative and critical/analytical thinking."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 113,
                    Name = "Literature Review",
                    Description = "A written paper based on systematic and explicit identification, evaluation and interpretation of existing bodies of work."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 114,
                    Name = "Log / Workbook",
                    Description = "A record of observations, activities or goals that have been met/not met, often in chronological order."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 115,
                    Name = "Thesis",
                    Description = "An extended piece of research designed to set up and defend an intellectual position taken by its author."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 116,
                    Name = "Proposal / Plan",
                    Description = "A plan or proposal for potential future work or for the delivery of content/skills to a group of people, often with justification."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 117,
                    Name = "Portfolio",
                    Description = "A student compilation of work with a coherent structure, collected over a period of time."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 118,
                    Name = "Performance",
                    Description = "A musical, dramatic or other event presented before an audience."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 119,
                    Name = "Case Study / Problem Based Learning",
                    Description = "A description and/or analysis of a real-world situation or problem, often applying relevant theories to address this."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 120,
                    Name = "Annotated Bibliography"
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 121,
                    Name = "Exhibition / Poster",
                    Description = "A 2D representational work for public display."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 122,
                    Name = "Viva Voce",
                    Description = "A verbal explanation/account of a topic, problem or task."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 123,
                    Name = "Interview",
                    Description = "A formal meeting conducted to elicit information from a person or group."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 124,
                    Name = "Role Play",
                    Description = "Action within a reproduction of a professional/disciplinary environment."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 125,
                    Name = "Written Assignment",
                    Description = "A written assessment not covered by a specific assessment types already listed."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 126,
                    Name = "Tutorial / Laboratory Exercises",
                    Description = "General exercises completed as part of a tutorial, lab or other practical session."
                },
                new UoAssessmentTypeModel()
                {
                    AssessmentTypeId = 127,
                    Name = "Online Learning Activity"
                }
            );
        }
    }
}
