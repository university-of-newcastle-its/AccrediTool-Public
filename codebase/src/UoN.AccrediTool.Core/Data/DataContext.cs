using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
    public partial class DataContext : DbContext
    {
        public DbSet<UoAcademicOrgModel> AcademicOrg { get; set; }
        public DbSet<UoAssessmentModel> Assessment { get; set; }
        public DbSet<UoAssessmentTypeModel> AssessmentType { get; set; }
        public DbSet<UoCampusModel> Campus { get; set; }
        public DbSet<UoCourseInstanceModel> CourseInstance { get; set; }
        public DbSet<UoCourseListModel> CourseList { get; set; }
        public DbSet<UoCourseModel> Course { get; set; }
        public DbSet<UoDocumentModel> Document { get; set; }
        public DbSet<UoFieldOfEducationModel> FieldOfEducation { get; set; }
        public DbSet<UoFrameworkModel> Framework { get; set; }
        public DbSet<UoLearningOutcomeModel> LearningOutcome { get; set; }
        public DbSet<UoLevelCategoryModel> LevelCategory { get; set; }
        public DbSet<UoLevelModel> Level { get; set; }
        public DbSet<UoNodeModel> Node { get; set; }
        public DbSet<UoProgramCareerModel> ProgramCareer { get; set; }
        public DbSet<UoProgramModel> Program { get; set; }
        public DbSet<UoProjectModel> Project { get; set; }
        public DbSet<UoUserGroupModel> UserGroup { get; set; }
        public DbSet<UoUserModel> User { get; set; }
        public DbSet<UoLevelCategoryNodesJoin> LevelCategoryNodes { get; set; }
        public DbSet<UoLevelCoursesJoin> LevelCourseJoins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is object)
            {
                #region Many-to-many relationships
                #region UoCourseListCoursesJoin
                modelBuilder.Entity<UoCourseListCoursesJoin>()
                    .HasIndex(joinTable => new { joinTable.CourseId, joinTable.CourseListId })
                    .IsUnique();

                modelBuilder.Entity<UoCourseListCoursesJoin>()
                    .HasOne(joinTable => joinTable.Course)
                    .WithMany(course => course.CourseListCourses)
                    .HasForeignKey(joinTable => joinTable.CourseId);

                modelBuilder.Entity<UoCourseListCoursesJoin>()
                    .HasOne(joinTable => joinTable.CourseList)
                    .WithMany(courseList => courseList.CourseListCourses)
                    .HasForeignKey(joinTable => joinTable.CourseListId);
                #endregion
                #region UoLearningOutcomeAssessmentsJoin
                modelBuilder.Entity<UoLearningOutcomeAssessmentsJoin>()
                    .HasIndex(joinTable => new { joinTable.AssessmentId, joinTable.LearningOutcomeId })
                    .IsUnique();

                modelBuilder.Entity<UoLearningOutcomeAssessmentsJoin>()
                    .HasOne(joinTable => joinTable.Assessment)
                    .WithMany(assessment => assessment.LearningOutcomeAssessments)
                    .HasForeignKey(joinTable => joinTable.AssessmentId);

                modelBuilder.Entity<UoLearningOutcomeAssessmentsJoin>()
                    .HasOne(joinTable => joinTable.LearningOutcome)
                    .WithMany(learningOutcome => learningOutcome.LearningOutcomeAssessments)
                    .HasForeignKey(joinTable => joinTable.LearningOutcomeId);
                #endregion
                #region UoLevelAssessmentsJoin
                modelBuilder.Entity<UoLevelAssessmentsJoin>()
                    .HasIndex(joinTable => new { joinTable.AssessmentId, joinTable.LevelId, joinTable.NodeId })
                    .IsUnique();

                modelBuilder.Entity<UoLevelAssessmentsJoin>()
                    .HasOne(joinTable => joinTable.Assessment)
                    .WithMany(assessment => assessment.LevelAssessments)
                    .HasForeignKey(joinTable => joinTable.AssessmentId);

                modelBuilder.Entity<UoLevelAssessmentsJoin>()
                    .HasOne(joinTable => joinTable.Level)
                    .WithMany(level => level.LevelAssessments)
                    .HasForeignKey(joinTable => joinTable.LevelId);

                modelBuilder.Entity<UoLevelAssessmentsJoin>()
                    .HasOne(joinTable => joinTable.Node)
                    .WithMany(node => node.LevelAssessments)
                    .HasForeignKey(joinTable => joinTable.NodeId);
                #endregion
                #region UoLevelCategoryNodesJoin
                modelBuilder.Entity<UoLevelCategoryNodesJoin>()
                    .HasIndex(joinTable => new { joinTable.LevelCategoryId, joinTable.NodeId })
                    .IsUnique();

                modelBuilder.Entity<UoLevelCategoryNodesJoin>()
                    .HasOne(joinTable => joinTable.LevelCategory)
                    .WithMany(levelCategory => levelCategory.LevelCategoryNodes)
                    .HasForeignKey(joinTable => joinTable.LevelCategoryId);

                modelBuilder.Entity<UoLevelCategoryNodesJoin>()
                    .HasOne(joinTable => joinTable.Node)
                    .WithMany(node => node.LevelCategoryNodes)
                    .HasForeignKey(joinTable => joinTable.NodeId);
                #endregion
                #region UoLevelCoursesJoin
                modelBuilder.Entity<UoLevelCoursesJoin>()
                    .HasIndex(joinTable => new { joinTable.CourseId, joinTable.LevelId, joinTable.NodeId })
                    .IsUnique();

                modelBuilder.Entity<UoLevelCoursesJoin>()
                    .HasOne(joinTable => joinTable.Course)
                    .WithMany(course => course.LevelCourses)
                    .HasForeignKey(joinTable => joinTable.CourseId);

                modelBuilder.Entity<UoLevelCoursesJoin>()
                    .HasOne(joinTable => joinTable.Level)
                    .WithMany(level => level.LevelCourses)
                    .HasForeignKey(joinTable => joinTable.LevelId);

                modelBuilder.Entity<UoLevelCoursesJoin>()
                    .HasOne(joinTable => joinTable.Node)
                    .WithMany(node => node.LevelCourses)
                    .HasForeignKey(joinTable => joinTable.NodeId);
                #endregion
                #region UoLevelProgramsJoin
                modelBuilder.Entity<UoLevelProgramsJoin>()
                    .HasIndex(joinTable => new { joinTable.ProgramId, joinTable.LevelId, joinTable.NodeId })
                    .IsUnique();

                modelBuilder.Entity<UoLevelProgramsJoin>()
                    .HasOne(joinTable => joinTable.Program)
                    .WithMany(program => program.LevelPrograms)
                    .HasForeignKey(joinTable => joinTable.ProgramId);

                modelBuilder.Entity<UoLevelProgramsJoin>()
                    .HasOne(joinTable => joinTable.Level)
                    .WithMany(level => level.LevelPrograms)
                    .HasForeignKey(joinTable => joinTable.LevelId);

                modelBuilder.Entity<UoLevelProgramsJoin>()
                    .HasOne(joinTable => joinTable.Node)
                    .WithMany(node => node.LevelPrograms)
                    .HasForeignKey(joinTable => joinTable.NodeId);
                #endregion
                #region UoNodeAssessmentsJoin
                modelBuilder.Entity<UoNodeAssessmentsJoin>()
                    .HasIndex(joinTable => new { joinTable.AssessmentId, joinTable.NodeId })
                    .IsUnique();

                modelBuilder.Entity<UoNodeAssessmentsJoin>()
                    .HasOne(joinTable => joinTable.Assessment)
                    .WithMany(assessment => assessment.NodeAssessments)
                    .HasForeignKey(joinTable => joinTable.AssessmentId);

                modelBuilder.Entity<UoNodeAssessmentsJoin>()
                    .HasOne(joinTable => joinTable.Node)
                    .WithMany(node => node.NodeAssessments)
                    .HasForeignKey(joinTable => joinTable.NodeId);
                #endregion
                #region UoNodeCoursesJoin
                modelBuilder.Entity<UoNodeCoursesJoin>()
                    .HasIndex(joinTable => new { joinTable.CourseId, joinTable.NodeId })
                    .IsUnique();

                modelBuilder.Entity<UoNodeCoursesJoin>()
                    .HasOne(joinTable => joinTable.Course)
                    .WithMany(course => course.NodeCourses)
                    .HasForeignKey(joinTable => joinTable.CourseId);

                modelBuilder.Entity<UoNodeCoursesJoin>()
                    .HasOne(joinTable => joinTable.Node)
                    .WithMany(node => node.NodeCourses)
                    .HasForeignKey(joinTable => joinTable.NodeId);
                #endregion
                #region UoNodeProgramsJoin
                modelBuilder.Entity<UoNodeProgramsJoin>()
                    .HasIndex(joinTable => new { joinTable.ProgramId, joinTable.NodeId })
                    .IsUnique();

                modelBuilder.Entity<UoNodeProgramsJoin>()
                    .HasOne(joinTable => joinTable.Program)
                    .WithMany(program => program.NodePrograms)
                    .HasForeignKey(joinTable => joinTable.ProgramId);

                modelBuilder.Entity<UoNodeProgramsJoin>()
                    .HasOne(joinTable => joinTable.Node)
                    .WithMany(node => node.NodePrograms)
                    .HasForeignKey(joinTable => joinTable.NodeId);
                #endregion
                #region UoProjectProgramsJoin
                modelBuilder.Entity<UoProjectProgramsJoin>()
                    .HasIndex(joinTable => new { joinTable.ProgramId, joinTable.ProjectId })
                    .IsUnique();

                modelBuilder.Entity<UoProjectProgramsJoin>()
                    .HasOne(joinTable => joinTable.Program)
                    .WithMany(program => program.ProjectPrograms)
                    .HasForeignKey(joinTable => joinTable.ProgramId);

                modelBuilder.Entity<UoProjectProgramsJoin>()
                    .HasOne(joinTable => joinTable.Project)
                    .WithMany(project => project.ProjectPrograms)
                    .HasForeignKey(joinTable => joinTable.ProjectId);
                #endregion
                #region UoProjectUserGroupsJoin
                modelBuilder.Entity<UoProjectUserGroupsJoin>()
                    .HasIndex(joinTable => new { joinTable.UserGroupId, joinTable.ProjectId })
                    .IsUnique();

                modelBuilder.Entity<UoProjectUserGroupsJoin>()
                    .HasOne(joinTable => joinTable.UserGroup)
                    .WithMany(userGroup => userGroup.ProjectUserGroups)
                    .HasForeignKey(joinTable => joinTable.UserGroupId);

                modelBuilder.Entity<UoProjectUserGroupsJoin>()
                    .HasOne(joinTable => joinTable.Project)
                    .WithMany(project => project.ProjectUserGroups)
                    .HasForeignKey(joinTable => joinTable.ProjectId);
                #endregion
                #region UoUserGroupUsersJoin
                modelBuilder.Entity<UoUserGroupUsersJoin>()
                    .HasIndex(joinTable => new { joinTable.UserId, joinTable.UserGroupId })
                    .IsUnique();

                modelBuilder.Entity<UoUserGroupUsersJoin>()
                    .HasOne(joinTable => joinTable.User)
                    .WithMany(user => user.UserGroupUsers)
                    .HasForeignKey(joinTable => joinTable.UserId);

                modelBuilder.Entity<UoUserGroupUsersJoin>()
                    .HasOne(joinTable => joinTable.UserGroup)
                    .WithMany(userGroup => userGroup.UserGroupUsers)
                    .HasForeignKey(joinTable => joinTable.UserGroupId);
                #endregion
                #endregion

                modelBuilder.Entity<UoDocumentModel>()
                    .Property(document => document.Type)
                    .HasConversion<string>();

                modelBuilder.Entity<UoDocumentModel>()
                    .Property(document => document.Grade)
                    .HasConversion<string>();

                modelBuilder.Entity<UoFieldOfEducationModel>()
                    .Property(fieldOfEducation => fieldOfEducation.Type)
                    .HasConversion<string>();

                modelBuilder.Entity<UoUserModel>()
                    .HasIndex(user => user.Login)
                    .IsUnique();

                SeedAssessmentTypes(modelBuilder);
                SeedCampuses(modelBuilder);
                SeedFieldsOfEducation(modelBuilder);
                SeedProgramCareers(modelBuilder);
            }
            else
            {
                throw new System.Exception("ModelBuilder in DataContext::OnModelCreating() is null");
            }
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
