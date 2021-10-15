using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UoN.AccrediTool.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "academic_org",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    acad_code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_academic_org", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "assessment_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "campus",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    campus_code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "field_of_education",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    field_code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    type = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    parent_field_of_education = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_of_education", x => x.id);
                    table.ForeignKey(
                        name: "FK_field_of_education_field_of_education_parent_field_of_educa~",
                        column: x => x.parent_field_of_education,
                        principalTable: "field_of_education",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "framework",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    template_name = table.Column<string>(nullable: true),
                    custom_node_noun = table.Column<string>(nullable: true),
                    custom_node_plural_noun = table.Column<string>(nullable: true),
                    custom_framework_noun = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_framework", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "level_category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "program_career",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    career_code = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_career", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_group",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject = table.Column<string>(nullable: false),
                    catalog_number = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    units = table.Column<int>(nullable: false),
                    position = table.Column<int>(nullable: false),
                    academic_org = table.Column<int>(nullable: false),
                    field_of_education = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_academic_org_academic_org",
                        column: x => x.academic_org,
                        principalTable: "academic_org",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_field_of_education_field_of_education",
                        column: x => x.field_of_education,
                        principalTable: "field_of_education",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "node",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    node_code = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    framework = table.Column<int>(nullable: true),
                    parent_node = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_node", x => x.id);
                    table.ForeignKey(
                        name: "FK_node_framework_framework",
                        column: x => x.framework,
                        principalTable: "framework",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_node_node_parent_node",
                        column: x => x.parent_node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    framework = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_framework_framework",
                        column: x => x.framework,
                        principalTable: "framework",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "level",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    level_category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level", x => x.id);
                    table.ForeignKey(
                        name: "FK_level_level_category_level_category",
                        column: x => x.level_category,
                        principalTable: "level_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "program",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program_code = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    first_term_code = table.Column<int>(nullable: true),
                    cricos_code = table.Column<string>(nullable: true),
                    min_units = table.Column<int>(nullable: true),
                    duration = table.Column<float>(nullable: true),
                    max_years = table.Column<float>(nullable: true),
                    cwr_type = table.Column<string>(nullable: true),
                    cwr_length = table.Column<int>(nullable: true),
                    cwr_length_units = table.Column<string>(nullable: true),
                    campus = table.Column<int>(nullable: false),
                    program_career = table.Column<int>(nullable: false),
                    field_of_education = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program", x => x.id);
                    table.ForeignKey(
                        name: "FK_program_campus_campus",
                        column: x => x.campus,
                        principalTable: "campus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_program_field_of_education_field_of_education",
                        column: x => x.field_of_education,
                        principalTable: "field_of_education",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_program_program_career_program_career",
                        column: x => x.program_career,
                        principalTable: "program_career",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_group_users_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user = table.Column<int>(nullable: false),
                    user_group = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group_users_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_group_users_join_user_group_user_group",
                        column: x => x.user_group,
                        principalTable: "user_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_group_users_join_user_user",
                        column: x => x.user,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_instance",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    term_code = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    campus = table.Column<int>(nullable: false),
                    course = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_instance", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_instance_campus_campus",
                        column: x => x.campus,
                        principalTable: "campus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_instance_course_course",
                        column: x => x.course,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "level_category_nodes_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    level_category = table.Column<int>(nullable: false),
                    node = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_category_nodes_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_level_category_nodes_join_level_category_level_category",
                        column: x => x.level_category,
                        principalTable: "level_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_level_category_nodes_join_node_node",
                        column: x => x.node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "node_courses_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course = table.Column<int>(nullable: false),
                    node = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_node_courses_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_node_courses_join_course_course",
                        column: x => x.course,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_node_courses_join_node_node",
                        column: x => x.node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_user_groups_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_group = table.Column<int>(nullable: false),
                    project = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_user_groups_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_project_user_groups_join_project_project",
                        column: x => x.project,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_user_groups_join_user_group_user_group",
                        column: x => x.user_group,
                        principalTable: "user_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "level_courses_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course = table.Column<int>(nullable: false),
                    level = table.Column<int>(nullable: false),
                    node = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_courses_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_level_courses_join_course_course",
                        column: x => x.course,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_level_courses_join_level_level",
                        column: x => x.level,
                        principalTable: "level",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_level_courses_join_node_node",
                        column: x => x.node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_list",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    major = table.Column<string>(nullable: true),
                    program = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_list", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_list_program_program",
                        column: x => x.program,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "level_programs_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program = table.Column<int>(nullable: false),
                    level = table.Column<int>(nullable: false),
                    node = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_programs_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_level_programs_join_level_level",
                        column: x => x.level,
                        principalTable: "level",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_level_programs_join_node_node",
                        column: x => x.node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_level_programs_join_program_program",
                        column: x => x.program,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "node_programs_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program = table.Column<int>(nullable: false),
                    node = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_node_programs_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_node_programs_join_node_node",
                        column: x => x.node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_node_programs_join_program_program",
                        column: x => x.program,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_programs_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    program = table.Column<int>(nullable: false),
                    project = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_programs_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_project_programs_join_program_program",
                        column: x => x.program,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_programs_join_project_project",
                        column: x => x.project,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assessment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    purpose = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    weight = table.Column<float>(nullable: false),
                    assessment_type = table.Column<int>(nullable: false),
                    course_instance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessment", x => x.id);
                    table.ForeignKey(
                        name: "FK_assessment_assessment_type_assessment_type",
                        column: x => x.assessment_type,
                        principalTable: "assessment_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assessment_course_instance_course_instance",
                        column: x => x.course_instance,
                        principalTable: "course_instance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learning_outcome",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    course_instance = table.Column<int>(nullable: true),
                    program = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learning_outcome", x => x.id);
                    table.ForeignKey(
                        name: "FK_learning_outcome_course_instance_course_instance",
                        column: x => x.course_instance,
                        principalTable: "course_instance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_learning_outcome_program_program",
                        column: x => x.program,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "course_list_courses_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course = table.Column<int>(nullable: false),
                    course_list = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_list_courses_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_course_list_courses_join_course_course",
                        column: x => x.course,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_list_courses_join_course_list_course_list",
                        column: x => x.course_list,
                        principalTable: "course_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uri = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    file_extension = table.Column<string>(nullable: false),
                    type = table.Column<string>(nullable: false),
                    grade = table.Column<string>(nullable: true),
                    assessment = table.Column<int>(nullable: true),
                    course = table.Column<int>(nullable: true),
                    course_instance = table.Column<int>(nullable: true),
                    program = table.Column<int>(nullable: true),
                    feedback = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.id);
                    table.ForeignKey(
                        name: "FK_document_assessment_assessment",
                        column: x => x.assessment,
                        principalTable: "assessment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_course_course",
                        column: x => x.course,
                        principalTable: "course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_course_instance_course_instance",
                        column: x => x.course_instance,
                        principalTable: "course_instance",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_document_feedback",
                        column: x => x.feedback,
                        principalTable: "document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_document_program_program",
                        column: x => x.program,
                        principalTable: "program",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "level_assessments_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    assessment = table.Column<int>(nullable: false),
                    level = table.Column<int>(nullable: false),
                    node = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_level_assessments_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_level_assessments_join_assessment_assessment",
                        column: x => x.assessment,
                        principalTable: "assessment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_level_assessments_join_level_level",
                        column: x => x.level,
                        principalTable: "level",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_level_assessments_join_node_node",
                        column: x => x.node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "node_assessments_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    assessment = table.Column<int>(nullable: false),
                    node = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_node_assessments_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_node_assessments_join_assessment_assessment",
                        column: x => x.assessment,
                        principalTable: "assessment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_node_assessments_join_node_node",
                        column: x => x.node,
                        principalTable: "node",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "learning_outcome_assessments_join",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    assessment = table.Column<int>(nullable: false),
                    learning_outcome = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learning_outcome_assessments_join", x => x.Id);
                    table.ForeignKey(
                        name: "FK_learning_outcome_assessments_join_assessment_assessment",
                        column: x => x.assessment,
                        principalTable: "assessment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_learning_outcome_assessments_join_learning_outcome_learning~",
                        column: x => x.learning_outcome,
                        principalTable: "learning_outcome",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "assessment_type",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 102, "Examination held in formal examination period.", "Formal Examination" },
                    { 127, null, "Online Learning Activity" },
                    { 126, "General exercises completed as part of a tutorial, lab or other practical session.", "Tutorial / Laboratory Exercises" },
                    { 125, "A written assessment not covered by a specific assessment types already listed.", "Written Assignment" },
                    { 124, "Action within a reproduction of a professional/disciplinary environment.", "Role Play" },
                    { 123, "A formal meeting conducted to elicit information from a person or group.", "Interview" },
                    { 122, "A verbal explanation/account of a topic, problem or task.", "Viva Voce" },
                    { 121, "A 2D representational work for public display.", "Exhibition / Poster" },
                    { 120, null, "Annotated Bibliography" },
                    { 119, "A description and/or analysis of a real-world situation or problem, often applying relevant theories to address this.", "Case Study / Problem Based Learning" },
                    { 117, "A student compilation of work with a coherent structure, collected over a period of time.", "Portfolio" },
                    { 116, "A plan or proposal for potential future work or for the delivery of content/skills to a group of people, often with justification.", "Proposal / Plan" },
                    { 115, "An extended piece of research designed to set up and defend an intellectual position taken by its author.", "Thesis" },
                    { 118, "A musical, dramatic or other event presented before an audience.", "Performance" },
                    { 113, "A written paper based on systematic and explicit identification, evaluation and interpretation of existing bodies of work.", "Literature Review" },
                    { 103, "Examination or test held outside the formal examination period.", "In Term Test" },
                    { 104, "Non-invigilated response to a number of questions which may include multiple choice questions and/or short answers.", "Quiz" },
                    { 114, "A record of observations, activities or goals that have been met/not met, often in chronological order.", "Log / Workbook" },
                    { 106, "The process of showing and/or explaining content to an audience.", "Presentation" },
                    { 107, "Student engages in a professional activity in context or for a real-world audience.", "Professional Task" },
                    { 105, "An extended written response to a set question, problem or issue.", "Essay" },
                    { 109, "Student engagement with content, either face to face or online, according to explicit criteria.", "Participation" },
                    { 110, "A tangible product where theory is applied to produce a model, a design, a program, a composition or other creative work.", "Project" },
                    { 111, "A structured analytical account of a project, investigation or process.", "Report" },
                    { 112, "An evaluation of student's own learning that includes narrative and critical/analytical thinking.", "Journal" },
                    { 108, "A demonstration of technical/professional skills.", "Practical Demonstration" }
                });

            migrationBuilder.InsertData(
                table: "campus",
                columns: new[] { "id", "campus_code", "name" },
                values: new object[,]
                {
                    { 11, "UTCCC", "Nurture - UTCC" },
                    { 17, "SYDC", "Sydney" },
                    { 16, "UNEC", "Rural Medicine UNE" },
                    { 15, "PMAQU", "Port Macquarie" },
                    { 14, "PSBC", "PSB Singapore" },
                    { 13, "CCSTC", "Ourimbah" },
                    { 12, "ONLIN", "Online" },
                    { 10, "BINUS", "Nurture - BINUS University" },
                    { 4, "DEDUC", "Distance Education" },
                    { 8, "HKUSP", "HKU Space - Hong Kong University" },
                    { 7, "HKMAC", "HKMA - Hong Kong" },
                    { 6, "GSCOM", "GradSchool" },
                    { 5, "GOSFC", "Gosford" },
                    { 3, "CALLA", "Callaghan Campus" },
                    { 2, "BBIC", "Broken Bay Institute" },
                    { 1, "BCAC", "BCA Singapore" },
                    { 9, "NCLE", "Newcastle City Precinct" }
                });

            migrationBuilder.InsertData(
                table: "field_of_education",
                columns: new[] { "id", "description", "field_code", "name", "parent_field_of_education", "type" },
                values: new object[,]
                {
                    { 422, "Mixed Field Programmes are programmes providing general and personal development education.", "12", "Mixed Field Programmes", null, "Broad" },
                    { 409, "Food, Hospitality and Personal Services is the study of preparing, displaying and serving food and beverages, providing hospitality services, caring for the hair and body for grooming and beautification, and other personal services.", "11", "Food, Hospitality and Personal Services", null, "Broad" },
                    { 383, "Creative Arts is the study of creating and performing works of art, music, dance and drama. It includes the study of clothing design and creation, and communicating through a variety of media.", "10", "Creative Arts", null, "Broad" },
                    { 311, "Society and Culture is the study of the physical, social and cultural organisation of human society and their influence on the individual and groups.", "09", "Society and Culture", null, "Broad" },
                    { 271, "Management and Commerce is the study of the theory and practice of planning, directing, organising, motivating and co-ordinating the human and material resources of private and public organisations and institutions. It includes the merchandising and provision of goods and services and personal development.", "08", "Management and Commerce", null, "Broad" },
                    { 254, "Education is the study of the process of learning. It includes the theories, methods and techniques of imparting knowledge and skills to others.", "07", "Education", null, "Broad" },
                    { 142, "Architecture and Building is the study of the art, science and techniques involved in designing, constructing, adapting and maintaining public, commercial, industrial and residential structures and landscapes. It includes the study of the art and science of designing and planning urban and regional environments.", "04", "Architecture and Building", null, "Broad" },
                    { 166, "Agriculture, Environmental and Related Studies is the study of the theory and practice of breeding, growing, gathering, reproducing and caring for plants and animals. It includes the study of the interaction between people and the environment and the application of scientific principles to the environment to protect it from deterioration.", "05", "Agriculture, Environmental and Related Studies", null, "Broad" },
                    { 61, "Engineering and Related Technologies is the study of the design, manufacture, installation, maintenance and functioning of machines, systems and structures; and the composition and processing of metals, ceramics, foodstuffs and other materials. It includes the measurement and mapping of the earth’s surface and its natural and constructed features.", "03", "Engineering and Related Technologies", null, "Broad" },
                    { 39, "Information Technology is the study of the processing, transmitting and storage of information by computers.", "02", "Information Technology", null, "Broad" },
                    { 1, "Natural and Physical Sciences is the study of all living organisms and inanimate natural objects, through experiment, observation and deduction.", "01", "Natural and Physical Sciences", null, "Broad" },
                    { 186, "Health is the study of maintaining and restoring the physical and mental wellbeing of humans and animals.", "06", "Health", null, "Broad" }
                });

            migrationBuilder.InsertData(
                table: "program_career",
                columns: new[] { "id", "career_code", "name" },
                values: new object[,]
                {
                    { 3, "RSCH", "Research" },
                    { 1, "ENAB", "Enabling" },
                    { 2, "PGCW", "Postgraduate Coursework" },
                    { 4, "UGRD", "Undergraduate" }
                });

            migrationBuilder.InsertData(
                table: "field_of_education",
                columns: new[] { "id", "description", "field_code", "name", "parent_field_of_education", "type" },
                values: new object[,]
                {
                    { 2, "Mathematical Sciences is the study of abstract deductive systems, numerical facts, data and their applications.", "0101", "Mathematical Sciences", 1, "Narrow" },
                    { 315, "Studies in Human Society is the study of the past and present behaviour of humans and their interaction, social organisation and geographical distribution.", "0903", "Studies in Human Society", 311, "Narrow" },
                    { 314, "Policy Studies is the study of analysing and developing policies to achieve societal and corporate goals.", "090103", "Policy Studies", 311, "Detailed" },
                    { 313, "Political Science is the study of political ideas, processes, institutions and behaviour, and how they influence public decisions within and among communities.", "090101", "Political Science", 311, "Detailed" },
                    { 312, "Political Science and Policy Studies is the study of political theories, processes, events and institutions, the operation and function of government, and the development and analysis of public and other policies.", "0901", "Political Science and Policy Studies", 311, "Narrow" },
                    { 307, "This narrow field includes all Management and Commerce not elsewhere classified.", "0899", "Other Management and Commerce", 271, "Narrow" },
                    { 302, "Banking, Finance and Related Fields is the study of planning, directing, organising and controlling financial activities and services. It includes the provision of insurance and investment services at the corporate and individual level.", "0811", "Banking, Finance and Related Fields", 271, "Narrow" },
                    { 324, "Human Welfare Studies and Services is the study of social intervention designed to help persons, individually and collectively, maximise their social and economic wellbeing.", "0905", "Human Welfare Studies and Services", 311, "Narrow" },
                    { 297, "Office Studies is the study of planning, organising and managing office systems, and operating office equipment. It includes the study of clerical skills.", "0809", "Office Studies", 271, "Narrow" },
                    { 288, "Sales and Marketing is the study of identifying and developing markets, and promoting and selling goods, services and properties.", "0805", "Sales and Marketing", 271, "Narrow" },
                    { 274, "Business Management is the study of planning and directing the functions and activities of persons, organisations and institutions.", "0803", "Business Management", 271, "Narrow" },
                    { 272, "Accounting is the study of the theory and practice of developing, maintaining, auditing and analysing financial records.", "0801", "Accounting", 271, "Narrow" },
                    { 269, "This narrow field includes all Education not elsewhere classified.", "0799", "Other Education", 254, "Narrow" },
                    { 266, "Curriculum and Education Studies is the study of developing and evaluating appropriate curricula and teaching strategies and practices.", "0703", "Curriculum and Education Studies", 254, "Narrow" },
                    { 255, "Teacher Education is the study of teaching and learning methods and their application to educating people.", "0701", "Teacher Education", 254, "Narrow" },
                    { 295, "Tourism is the study of the structure and operations of the tourism industry, tour guiding, and ticketing and reservation practices.", "0807", "Tourism", 271, "Narrow" },
                    { 334, "Behavioural Science is the study of the causes of behaviour as a result of individual differences, experience and environment, and the modification of that behaviour.", "0907", "Behavioural Science", 311, "Narrow" },
                    { 337, "Law is the study of the principles and regulations which are recognised in the form of legislation or customs and policies recognised by judicial decision.", "0909", "Law", 311, "Narrow" },
                    { 346, "Justice and Law Enforcement is the study of the principles and procedures for formally maintaining social order.", "0911", "Justice and Law Enforcement", 311, "Narrow" },
                    { 428, "Social Skills Programmes develop skills required for day to day interacting and functioning in society.", "1203", "Social Skills Programmes", 422, "Narrow" },
                    { 423, "General Education Programmes develop general knowledge, skills and competencies required for education and learning.", "1201", "General Education Programmes", 422, "Narrow" },
                    { 418, "Personal Services is the study of caring for the hair and body for grooming and beautification.", "1103", "Personal Services", 409, "Narrow" },
                    { 410, "Food and Hospitality is the study of preparing, presenting and serving food and beverages, and providing hospitality services.", "1101", "Food and Hospitality", 409, "Narrow" },
                    { 407, "This narrow field includes all Creative Arts not elsewhere classified.", "1099", "Other Creative Arts", 383, "Narrow" },
                    { 401, "Communication and Media Studies is the study of creating, producing, disseminating and evaluating messages.", "1007", "Communication and Media Studies", 383, "Narrow" },
                    { 396, "Graphic and Design Studies is the study of the techniques and skills necessary to design and produce finished work, and solve design and visual communication problems. It includes the study of clothing design and creation.", "1005", "Graphic and Design Studies", 383, "Narrow" },
                    { 389, "Visual Arts and Crafts is the study of non-literary visual forms of creative expression for artistic and aesthetic purposes.", "1003", "Visual Arts and Crafts", 383, "Narrow" },
                    { 384, "Performing Arts is the study of creating and performing works of music, dance and drama.", "1001", "Performing Arts", 383, "Narrow" },
                    { 378, "This narrow field includes all Society and Culture not elsewhere classified.", "0999", "Other Society and Culture", 311, "Narrow" },
                    { 374, "Sport and Recreation  is the study of sport, recreational and leisure activities, their role in society, and the development of recreational and leisure programmes.", "0921", "Sport and Recreation", 311, "Narrow" },
                    { 371, "Economics and Econometrics is the study of the production, consumption and transfer of wealth, and developing and analysing models which describe this behaviour.", "0919", "Economics and Econometrics", 311, "Narrow" },
                    { 368, "Philosophy and Religious Studies is the study of the nature and context of human experience, perception and interpretation of reality, and human spirituality and beliefs.", "0917", "Philosophy and Religious Studies", 311, "Narrow" },
                    { 354, "Language and Literature is the study of the structure and use of languages, and the literature of particular times and geographic areas.", "0915", "Language and Literature", 311, "Narrow" },
                    { 351, "Librarianship, Information Management and Curatorial Studies is the study of selecting, acquiring, organising, storing and facilitating the use of collections of information, and locating, identifying and assessing cultural heritage resources.", "0913", "Librarianship, Information Management and Curatorial Studies", 311, "Narrow" },
                    { 248, "This narrow field includes all Health not elsewhere classified.", "0699", "Other Health", 186, "Narrow" },
                    { 433, "Employment Skills Programmes develop job searching and employment related skills.", "1205", "Employment Skills Programmes", 422, "Narrow" },
                    { 243, "Complementary Therapies is the study of the prevention and treatment of health problems using natural and traditional remedies.", "0619", "Complementary Therapies", 186, "Narrow" },
                    { 232, "Radiography is the study of technologies which use ionising and non-ionising (e.g. ultrasound) radiation for producing diagnostic images and administering radiation therapy.", "0615", "Radiography", 186, "Narrow" },
                    { 109, "Geomatic Engineering is the study of measuring and graphically representing natural and constructed features of the environment.", "0311", "Geomatic Engineering", 61, "Narrow" },
                    { 100, "Civil Engineering is the study of planning, designing, testing and directing the construction of large scale buildings and structures, and transport, water supply, pollution control and sewerage systems. It includes economic, functional and environmental considerations in the design and construction.", "0309", "Civil Engineering", 61, "Narrow" },
                    { 89, "Mechanical and Industrial Engineering and Technology is the study of designing, planning, installing, operating, maintaining and repairing mechanical plant, machinery and tools.", "0307", "Mechanical and Industrial Engineering and Technology", 61, "Narrow" },
                    { 79, "Automotive Engineering and Technology is the study of planning, designing, developing, producing and maintaining motor vehicles including earth moving equipment, motor cycles and small engines.", "0305", "Automotive Engineering and Technology", 61, "Narrow" },
                    { 73, "Process and Resources Engineering is the study of planning, designing and developing systems, processes and plant for locating and extracting minerals, oil and gas from the earth, and for physically and chemically transforming raw materials to produce metals, alloys, petrochemicals, ceramics, polymers and other materials. It includes the industrial manufacture, processing, packaging and handling of foodstuffs, pharmaceuticals and biochemicals.", "0303", "Process and Resources Engineering", 61, "Narrow" },
                    { 62, "Manufacturing Engineering and Technology is the study of the planning, organisation and operation of manufacturing methods, processes, facilities and systems.", "0301", "Manufacturing Engineering and Technology", 61, "Narrow" },
                    { 113, "Electrical and Electronic Engineering and Technology is the study of planning, designing, developing, testing, installing and maintaining electrical, electronic and communications equipment, circuits and systems. It includes designing, installing and maintaining equipment for generating and distributing electrical power.", "0313", "Electrical and Electronic Engineering and Technology", 61, "Narrow" },
                    { 58, "This narrow field includes all Information Technology not elsewhere classified.", "0299", "Other Information Technology", 39, "Narrow" },
                    { 40, "Computer Science is the study of the design and development of computer systems.", "0201", "Computer Science", 39, "Narrow" },
                    { 32, "This narrow field includes all Natural and Physical Sciences not elsewhere classified.", "0199", "Other Natural and Physical Sciences", 1, "Narrow" },
                    { 22, "Biological Sciences is the study of the structure, function, reproduction, growth, evolution and behaviour of living organisms.", "0109", "Biological Sciences", 1, "Narrow" },
                    { 13, "Earth Sciences is the study of the nature, composition and structure of the Earth including its atmosphere and hydrosphere.", "0107", "Earth Sciences", 1, "Narrow" },
                    { 9, "Chemical Sciences is the study of the composition, structure, and the chemical transformations of matter.", "0105", "Chemical Sciences", 1, "Narrow" },
                    { 6, "Physics and Astronomy is the study of the laws governing the structure of the universe and the forms of matter and energy.", "0103", "Physics and Astronomy", 1, "Narrow" },
                    { 52, "Information Systems is the study of the flow of information, capturing data, and the design and specification of information systems and user interfaces.", "0203", "Information Systems", 39, "Narrow" },
                    { 124, "Aerospace Engineering and Technology is the study of planning, designing, developing, assembling and maintaining aircraft structures and systems. It includes operating and directing aircraft.", "0315", "Aerospace Engineering and Technology", 61, "Narrow" },
                    { 130, "Maritime Engineering and Technology is the study of designing, maintaining and operating marine craft and shipboard machinery and systems.", "0317", "Maritime Engineering and Technology", 61, "Narrow" },
                    { 135, "This narrow field includes all Engineering and Related Technologies not elsewhere classified.", "0399", "Other Engineering and Related Technologies", 61, "Narrow" },
                    { 224, "Public Health is the study of the principles and practices of protecting, promoting, maintaining and restoring the health of the community.", "0613", "Public Health", 186, "Narrow" },
                    { 220, "Veterinary Studies is the study of identifying, preventing and treating disease and injury in animals, and their general veterinary care.", "0611", "Veterinary Studies", 186, "Narrow" },
                    { 216, "Optical Science is the study of measuring and assessing vision, and prescribing, preparing and dispensing corrective lenses.", "0609", "Optical Science", 186, "Narrow" },
                    { 211, "Dental Studies is the study of diagnosing, treating and preventing diseases and abnormalities of the teeth and adjacent tissues. It includes the study of designing, making and repairing dental prostheses and orthodontic appliances, and assisting with dental procedures.", "0607", "Dental Studies", 186, "Narrow" },
                    { 209, "Pharmacy is the study of the preparation and dispensing of drugs.", "0605", "Pharmacy", 186, "Narrow" },
                    { 199, "Nursing is the study of the principles and practices of providing general and specialised preventative, curative, rehabilitative and palliative care to individuals and groups. It includes the study of the structure and function of the human body and mind, the restoration and maintenance of health, pain control, human behaviour and nursing ethics.", "0603", "Nursing", 186, "Narrow" },
                    { 187, "Medical Studies", "0601", "Medical Studies", 186, "Narrow" },
                    { 183, "This narrow field includes all Agriculture, Environmental and Related Studies not elsewhere classified.", "0599", "Other Agriculture, Environmental and Related Studies", 166, "Narrow" },
                    { 180, "Environmental Studies is the study of the relationships between living organisms and the natural, rural, industrial and urban environments. It includes the study of the impact humans have upon the natural environment.", "0509", "Environmental Studies", 166, "Narrow" },
                    { 177, "Fisheries Studies is the study of breeding, rearing, harvesting, handling, and managing fish and other aquatic resources.", "0507", "Fisheries Studies", 166, "Narrow" },
                    { 175, "Forestry Studies is the study of establishing, cultivating, harvesting and managing forests.", "0505", "Forestry Studies", 166, "Narrow" },
                    { 172, "Horticulture and Viticulture is the study of cultivating, propagating and producing intensively managed crops such as grapes and other fruits, vegetables, flowers, trees, shrubs and plants.", "0503", "Horticulture and Viticulture", 166, "Narrow" },
                    { 167, "Agriculture is the study of growing, maintaining and harvesting non-intensively managed crops and pastures, and breeding, grazing and managing animals. It includes the study of farming and producing unprocessed plant and animal products.", "0501", "Agriculture", 166, "Narrow" },
                    { 149, "Building is the study of the science, technology and techniques of assembling, erecting and maintaining public, commercial, industrial and residential structures and their fittings.", "0403", "Building", 142, "Narrow" },
                    { 143, "Architecture and Urban Environment is the study of the art and science of planning and designing public, commercial, industrial and residential buildings, and their interiors. It includes the study of planning and designing built environments.", "0401", "Architecture and Urban Environment", 142, "Narrow" },
                    { 234, "Rehabilitation Therapies is the study of the practices and principles that restore a person to an optimal quality of life. It includes treating individuals so that they can return to normal duties after being affected by accident or disease.", "0617", "Rehabilitation Therapies", 186, "Narrow" },
                    { 438, "This narrow field includes all Mixed Field Programmes not elsewhere classified.", "1299", "Other Mixed Field Programmes", 422, "Narrow" }
                });

            migrationBuilder.InsertData(
                table: "field_of_education",
                columns: new[] { "id", "description", "field_code", "name", "parent_field_of_education", "type" },
                values: new object[,]
                {
                    { 3, "Mathematics is the study of deductive systems, including algebra, arithmetic, geometry, analysis and applied mathematics.", "010101", "Mathematics", 2, "Detailed" },
                    { 292, "Advertising is the study of informing potential customers of the nature of products and services and their merits.", "080507", "Advertising", 288, "Detailed" },
                    { 291, "Marketing is the study of identifying market opportunities and developing and implementing strategies for pricing and promoting products and services.", "080505", "Marketing", 288, "Detailed" },
                    { 290, "Real Estate is the study of developing, purchasing, leasing and selling buildings, businesses and properties.", "080503", "Real Estate", 288, "Detailed" },
                    { 289, "Sales is the study of buying and selling goods and services.", "080501", "Sales", 288, "Detailed" },
                    { 287, "This detailed field includes all Business and Management not elsewhere classified.", "080399", "Business and Management, n.e.c.", 274, "Detailed" },
                    { 286, "Tourism Management is the study of planning and managing the activities of tourism focused enterprises.", "080323", "Tourism Management", 274, "Detailed" },
                    { 285, "Farm Management and Agribusiness is the study of managing farming and agricultural business enterprises.", "080321", "Farm Management and Agribusiness", 274, "Detailed" },
                    { 284, "Hospitality Management is the study of managing the operations of organisations which provide hospitality services. It includes conference and special events management.", "080319", "Hospitality Management", 274, "Detailed" },
                    { 293, "Public Relations is the study of creating and maintaining an understanding and a favourable view of an organisation and its products, services and role.", "080509", "Public Relations", 288, "Detailed" },
                    { 283, "Quality Management is the study of initiating and implementing quality assurance techniques and procedures to meet standards of best practice.", "080317", "Quality Management", 274, "Detailed" },
                    { 281, "Public and Health Care Administration is the study of planning and directing the functions and operations of organisations whose primary objective is the provision of services for the public good.", "080313", "Public and Health Care Administration", 274, "Detailed" },
                    { 280, "International Business is the study of international trade, import and export processes and regulations and customs procedures and regulations.", "080311", "International Business", 274, "Detailed" },
                    { 279, "Industrial Relations is the study of the relationship between employers and employees, and the application of such relations to workplace issues.", "080309", "Industrial Relations", 274, "Detailed" },
                    { 278, "Organisation Management is the study of organisational structure and change.", "080307", "Organisation Management", 274, "Detailed" },
                    { 277, "Personal Management Training is the study of self-improvement techniques.", "080305", "Personal Management Training", 274, "Detailed" },
                    { 276, "Human Resource Management is the study of staffing policy, practice and management within an organisation.", "080303", "Human Resource Management", 274, "Detailed" },
                    { 275, "Business Management is the study of planning and directing the activities of commercial enterprises. It includes the study of the nature, operation and role of business, and the resolution of management and administrative problems.", "080301", "Business Management", 274, "Detailed" },
                    { 273, "Accounting is the study of the theory and practice of developing, maintaining, auditing and analysing financial records.", "080101", "Accounting", 272, "Detailed" },
                    { 282, "Project Management is the study of planning and managing a total project process.", "080315", "Project Management", 274, "Detailed" },
                    { 270, "This narrow field includes all Education not elsewhere classified.", "079999", "Education, n.e.c.", 269, "Detailed" },
                    { 294, "This detailed field includes all Sales and Marketing not elsewhere classified.", "080599", "Sales and Marketing, n.e.c.", 288, "Detailed" },
                    { 298, "Secretarial and Clerical Studies is the study of shorthand, record keeping, correspondence and general office procedures.", "080901", "Secretarial and Clerical Studies", 297, "Detailed" },
                    { 323, "This detailed field includes all Studies in Human Society not elsewhere classified.", "090399", "Studies in Human Society, n.e.c.", 315, "Detailed" },
                    { 322, "Gender Specific Studies is the study of the ways that people and society develop various ideas and beliefs concerning the roles and functions of a specific gender and the way that this gender relates to society.", "090313", "Gender Specific Studies", 315, "Detailed" },
                    { 321, "Indigenous Studies is the study of indigenous culture and societies and their relationship to the broader society of a particular country.", "090311", "Indigenous Studies", 315, "Detailed" },
                    { 320, "Human Geography is the study of spatial variations between human populations and the interactions between people and their environment.", "090309", "Human Geography", 315, "Detailed" },
                    { 319, "Archaeology is the study of human history and prehistory through the excavation of sites and the analysis of artefacts and other physical remains.", "090307", "Archaeology", 315, "Detailed" },
                    { 318, "History is the study of past cultures, events and ideas using written documents and other evidence.", "090305", "History", 315, "Detailed" },
                    { 317, "Anthropology is the study of the diversity of human cultural practices and beliefs by participant observation and comparison.", "090303", "Anthropology", 315, "Detailed" },
                    { 316, "Sociology is the study of the origin, development, organisation and functioning of human society, including the study of the patterns of social relations.", "090301", "Sociology", 315, "Detailed" },
                    { 296, "Tourism is the study of the structure and operations of the tourism industry, tour guiding, and ticketing and reservation practices.", "080701", "Tourism", 295, "Detailed" },
                    { 310, "This detailed field includes all Management and Commerce not elsewhere classified.", "089999", "Management and Commerce, n.e.c.", 307, "Detailed" },
                    { 308, "Purchasing, Warehousing and Distribution is the study of purchasing, supplying, storing and despatching goods and other materials.", "089901", "Purchasing, Warehousing and Distribution", 307, "Detailed" },
                    { 306, "This detailed field includes all Banking, Finance and Related Fields not elsewhere classified.", "081199", "Banking, Finance and Related Fields, n.e.c.", 302, "Detailed" },
                    { 305, "Investment and Securities is the study of directing, planning and providing investment and securities services.", "081105", "Investment and Securities", 302, "Detailed" },
                    { 304, "Insurance and Actuarial Studies is the study of directing, planning and providing insurance services, and applying mathematical and statistical analysis to financial planning problems.", "081103", "Insurance and Actuarial Studies", 302, "Detailed" },
                    { 303, "Banking and Finance is the study of directing, planning and providing financial institution services in relation to savings and loans.", "081101", "Banking and Finance", 302, "Detailed" },
                    { 301, "This detailed field includes all Office Studies not elsewhere classified.", "080999", "Office Studies, n.e.c", 297, "Detailed" },
                    { 300, "Practical Computing Skills is the study of basic computer operation and using software packages.", "080905", "Practical Computing Skills", 297, "Detailed" },
                    { 299, "Keyboard Skills is the study of typing and data entry.", "080903", "Keyboard Skills", 297, "Detailed" },
                    { 309, "Valuation is the study of valuing land, buildings, businesses, properties, machinery, art and personal items.", "089903", "Valuation", 307, "Detailed" },
                    { 325, "Social Work is the study of promoting, restoring, maintaining and enhancing the functioning of individuals, families, social groups, organisations and communities by the utilisation of resources within individuals and the social environment in order to alleviate social problems.", "090501", "Social Work", 324, "Detailed" },
                    { 268, "Education Studies is the study of the theoretical background of traditional and current teaching practices.", "070303", "Education Studies", 266, "Detailed" },
                    { 265, "This detailed field includes all Teacher Education not elsewhere classified.", "070199", "Teacher Education, n.e.c.", 255, "Detailed" },
                    { 238, "Speech Pathology is the study of assessing and treating speech and language disorders.", "061707", "Speech Pathology", 234, "Detailed" },
                    { 237, "Chiropractic and Osteopathy is the study of assessing and relieving disorders of the body through manipulating and treating the musculoskeletal system.", "061705", "Chiropractic and Osteopathy", 234, "Detailed" },
                    { 236, "Occupational Therapy is the study of treating physical, cognitive and psychiatric conditions through activities in order to optimise functioning and independence in daily life.", "061703", "Occupational Therapy", 234, "Detailed" },
                    { 235, "Physiotherapy is the study of assessing people with temporary and longer term physical injuries and movement disorders, and restoring maximum movement and functional ability.", "061701", "Physiotherapy", 234, "Detailed" },
                    { 233, "Radiography is the study of technologies which use ionising and non-ionising (e.g. ultrasound) radiation for producing diagnostic images and administering radiation therapy.", "061501", "Radiography", 232, "Detailed" },
                    { 231, "This detailed field includes all Public Health not elsewhere classified.", "061399", "Public Health, n.e.c.", 224, "Detailed" },
                    { 230, "Epidemiology is the study of the incidence, distribution and possible control of infectious and chronic diseases as they effect groups of people.", "061311", "Epidemiology", 224, "Detailed" },
                    { 229, "Community Health is the study of health practices in the community which support and assist the management of disabilities and illness.", "061309", "Community Health", 224, "Detailed" },
                    { 239, "Audiology is the study of assessing and treating hearing disorders.", "061709", "Audiology", 234, "Detailed" },
                    { 228, "Health Promotion is the study of promoting a healthy lifestyle and influencing behaviour to improve health.", "061307", "Health Promotion", 224, "Detailed" },
                    { 226, "Environmental Health is the study of recognising, evaluating and controlling environmental factors affecting public health.", "061303", "Environmental Health", 224, "Detailed" },
                    { 225, "Occupational Health and Safety is the study of recognising, evaluating and controlling environmental factors associated with the interaction of individuals and the workplace.", "061301", "Occupational Health and Safety", 224, "Detailed" },
                    { 223, "This detailed field includes all Veterinary Studies not elsewhere classified.", "061199", "Veterinary Studies, n.e.c.", 220, "Detailed" },
                    { 222, "Veterinary Assisting is the study of caring for sick, injured and infirm animals undergoing treatment in veterinary clinics.", "061103", "Veterinary Assisting", 220, "Detailed" },
                    { 221, "Veterinary Science is the study of diagnosing and treating animal diseases, ailments and injuries, and preventing and containing the spread of animal diseases.", "061101", "Veterinary Science", 220, "Detailed" },
                    { 219, "This detailed field includes all Optical Science not elsewhere classified.", "060999", "Optical Science, n.e.c.", 216, "Detailed" },
                    { 218, "Optical Technology is the study of preparing and dispensing corrective lenses according to prescription.", "060903", "Optical Technology", 216, "Detailed" },
                    { 217, "Optometry is the study of measuring and assessing vision, and prescribing lenses for visual correction.", "060901", "Optometry", 216, "Detailed" },
                    { 227, "Indigenous Health is the study of the health of the indigenous population within the broader context of socio-economic development of Aboriginal and Torres Strait Islander communities.", "061305", "Indigenous Health", 224, "Detailed" },
                    { 267, "Curriculum Studies is the study of developing and evaluating appropriate curricula for different Key Learning Areas and subjects to teach to particular groups of children and adults.", "070301", "Curriculum Studies", 266, "Detailed" },
                    { 240, "Massage Therapy is the study of treating disorders through massage of the soft tissue.", "061711", "Massage Therapy", 234, "Detailed" },
                    { 242, "This detailed field includes all Rehabilitation Therapies not elsewhere classified.", "061799", "Rehabilitation Therapies, n.e.c.", 234, "Detailed" },
                    { 264, "Nursing Education Teacher Training is the study of the theories, methods and practice of teaching nurses in tertiary educational institutions and hospital-based settings.", "070117", "Nursing Education Teacher Training", 255, "Detailed" },
                    { 263, "English as a Second Language Teaching is the study of theories, methods and practice of teaching English to those whose first language is other than English, including teaching children in school settings and teaching adults and children in other settings.", "070115", "English as a Second Language Teaching", 255, "Detailed" },
                    { 262, "Teacher Education: Special Education is the study of the theories, methods and practice of teaching children with special learning needs, including children with physical and mental disabilities and impairments, and gifted children. Special Education may be conducted in special schools or by providing support for teachers in primary and secondary schools.", "070113", "Teacher Education: Special Education", 255, "Detailed" },
                    { 261, "Teacher Education: Higher Education is the study of the theories, methods and practice of teaching in higher educational institutions.", "070111", "Teacher Education: Higher Education", 255, "Detailed" },
                    { 260, "Teacher Education: Vocational Education and Training is the study of the theories, methods and practice of teaching trade and other vocational subjects.", "070109", "Teacher Education: Vocational Education and Training", 255, "Detailed" },
                    { 259, "Teacher-Librarianship is the study of the theories, methods and practice of teaching relating to the integration of library and information resources in schools and colleges.", "070107", "Teacher-Librarianship", 255, "Detailed" },
                    { 258, "Teacher Education: Secondary is the study of the theories, methods and practice of teaching secondary school students between the ages of 12 and 18 within formal school and college settings.", "070105", "Teacher Education: Secondary", 255, "Detailed" },
                    { 257, "Teacher Education: Primary is the study of the theories, methods and practice of teaching children between the ages of 5 and 12 within formal school settings.", "070103", "Teacher Education: Primary", 255, "Detailed" },
                    { 241, "Podiatry is the study of assessing and treating physical ailments of the human foot and lower limb.", "061713", "Podiatry", 234, "Detailed" },
                    { 256, "Teacher Education: Early Childhood is the study of the theories, methods and practice of teaching children from birth to 8 years of age within formal education settings.", "070101", "Teacher Education: Early Childhood", 255, "Detailed" },
                    { 252, "First Aid is the study of skills necessary to aid the ill and injured until medical aid arrives.", "069907", "First Aid", 248, "Detailed" },
                    { 251, "Paramedical Studies is the study of the emergency treatment of the sick and injured prior to and during transfer to a hospital or medical facility.", "069905", "Paramedical Studies", 248, "Detailed" },
                    { 250, "Human Movement is the study of the nature, cause and control of movement. It includes posture and balance, and the science of human performance.", "069903", "Human Movement", 248, "Detailed" },
                    { 249, "Nutrition and Dietetics is the study of the nutritional and dietary needs of humans.", "069901", "Nutrition and Dietetics", 248, "Detailed" },
                    { 247, "This detailed field includes all Complementary Therapies not elsewhere classified.", "061999", "Complementary Therapies, n.e.c.", 243, "Detailed" },
                    { 246, "Traditional Chinese Medicine is the study of treating diseases through traditional Chinese therapies.", "061905", "Traditional Chinese Medicine", 243, "Detailed" },
                    { 245, "Acupuncture is the study of treating diseases through influencing points on meridians using fine needles.", "061903", "Acupuncture", 243, "Detailed" },
                    { 244, "Naturopathy is the study of treating diseases using natural therapies.", "061901", "Naturopathy", 243, "Detailed" },
                    { 253, "This detailed field includes all Health not elsewhere classified.", "069999", "Health, n.e.c.", 248, "Detailed" },
                    { 215, "This detailed field includes all Dental Studies not elsewhere classified.", "060799", "Dental Studies, n.e.c.", 211, "Detailed" },
                    { 326, "Children’s Services is the study of the care and development of children.", "090503", "Children’s Services", 324, "Detailed" },
                    { 328, "Care for the Aged is the study of caring for elderly people in various social support services associated with families and groups in the community.", "090507", "Care for the Aged", 324, "Detailed" },
                    { 406, "This detailed field includes all Communication and Media Studies not elsewhere classified.", "100799", "Communication and Media Studies, n.e.c.", 401, "Detailed" },
                    { 405, "Verbal Communication is the study of developing effective verbal communication skills.", "100707", "Verbal Communication", 401, "Detailed" },
                    { 404, "Written Communication is the study of developing effective written communication skills.", "100705", "Written Communication", 401, "Detailed" },
                    { 403, "Journalism is the study of researching current affairs and events and other matters of interests and reporting them.", "100703", "Journalism", 401, "Detailed" },
                    { 402, "Audio Visual Studies is the study of producing films and videos, and television and radio programmes.", "100701", "Audio Visual Studies", 401, "Detailed" },
                    { 400, "This detailed field includes all Graphic and Design Studies not elsewhere classified.", "100599", "Graphic and Design Studies, n.e.c.", 396, "Detailed" },
                    { 399, "Fashion Design is the study of creatively combining line, form and fabric in designing and constructing fashion garments.", "100505", "Fashion Design", 396, "Detailed" },
                    { 398, "Textile Design is the study of designing and producing textiles.", "100503", "Textile Design", 396, "Detailed" },
                    { 408, "This detailed field includes all Creative Arts not elsewhere classified.", "109999", "Creative Arts, n.e.c.", 407, "Detailed" },
                    { 397, "Graphic Arts and Design Studies is the study of designing and producing visual representations of concepts and information.", "100501", "Graphic Arts and Design Studies", 396, "Detailed" },
                    { 394, "Floristry is the study of designing and displaying floral arrangements.", "100309", "Floristry", 389, "Detailed" },
                    { 393, "Jewellery Making is the study of designing, producing and repairing ornaments for personal adornment.", "100307", "Jewellery Making", 389, "Detailed" },
                    { 392, "Crafts is the study of fashioning individual objects for decorative, ornamental and functional purpose.", "100305", "Crafts", 389, "Detailed" },
                    { 391, "Photography is the study of composing, taking, developing, printing, enlarging and presenting photographs for creative and practical purposes.", "100303", "Photography", 389, "Detailed" },
                    { 390, "Fine Arts is the study of non-literary visual forms of creative expression for artistic and aesthetic purposes, and the methods of creating those forms.", "100301", "Fine Arts", 389, "Detailed" },
                    { 388, "Performing Arts, n.e.c.", "100199", "Performing Arts, n.e.c.", 384, "Detailed" },
                    { 387, "Dance is the study of the history, theory, creation and performance of works of dance.", "100105", "Dance", 384, "Detailed" },
                    { 386, "Drama and Theatre Studies is the study of the history, theory, creation and performance of dramatic works. It includes speech, movement, mime, characterisation, improvisation and stage craft.", "100103", "Drama and Theatre Studies", 384, "Detailed" },
                    { 395, "This detailed field includes all Visual Arts and Crafts not elsewhere classified.", "100399", "Visual Arts and Crafts, n.e.c.", 389, "Detailed" },
                    { 385, "Music is the study of the history, theory, creation and performance of music.", "100101", "Music", 384, "Detailed" },
                    { 411, "Hospitality is the study of providing reception, accommodation, entertainment, food, beverages and related services to patrons at hotels, motels, clubs, restaurants, caravan parks, etc.", "110101", "Hospitality", 410, "Detailed" },
                    { 413, "Butchery is the study of cutting and preparing meat. It includes small-scale sausage and smallgoods making.", "110105", "Butchery", 410, "Detailed" },
                    { 436, "Work Practices Programmes develop skills for functioning effectively in the workplace.", "120505", "Work Practices Programmes", 433, "Detailed" },
                    { 435, "Job Search Skills Programmes develop skills for finding a job.", "120503", "Job Search Skills Programmes", 433, "Detailed" },
                    { 434, "Career Development Programmes provide guidance and counselling for career development.", "120501", "Career Development Programmes", 433, "Detailed" },
                    { 432, "This detailed field includes all Social Skills Programmes not elsewhere classified.", "120399", "Social Skills Programmes, n.e.c.", 428, "Detailed" },
                    { 431, "Parental Education Programmes develop skills for parenting.", "120305", "Parental Education Programmes", 428, "Detailed" },
                    { 430, "Survival Skills Programmes develop skills for managing a household.", "120303", "Survival Skills Programmes", 428, "Detailed" },
                    { 429, "Social and Interpersonal Skills Programmes develop skills for interacting with family and people in the wider community.", "120301", "Social and Interpersonal Skills Programmes", 428, "Detailed" },
                    { 427, "This detailed field includes all General Education Programmes not elsewhere classified.", "120199", "General Education Programmes, n.e.c.", 423, "Detailed" },
                    { 412, "Food and Beverage Service is the study of serving and presenting food and beverages.", "110103", "Food and Beverage Service", 410, "Detailed" },
                    { 426, "Learning Skills Programmes develop skills for study, such as research and analysis skills.", "120105", "Learning Skills Programmes", 423, "Detailed" },
                    { 424, "General Primary and Secondary Education Programmes develop general knowledge and skills through school education programmes. It includes secondary education programmes run in TAFEs and other similar institutions.", "120101", "General Primary and Secondary Education Programmes", 423, "Detailed" },
                    { 421, "This detailed field includes all Personal Services not elsewhere classified.", "110399", "Personal Services, n.e.c.", 418, "Detailed" },
                    { 420, "Hairdressing is the study of cutting, colouring and styling scalp and facial hair.", "110303", "Hairdressing", 418, "Detailed" },
                    { 419, "Beauty Therapy is the study of maintaining and beautifying the face and body.", "110301", "Beauty Therapy", 418, "Detailed" },
                    { 417, "This detailed field includes all Food and Hospitality not elsewhere classified.", "110199", "Food and Hospitality, n.e.c.", 410, "Detailed" },
                    { 416, "Food Hygiene is the study of the principles and regulations of food safe handling and preparation.", "110111", "Food Hygiene", 410, "Detailed" },
                    { 415, "Cookery is the study of preparing food. It includes the development of recipes.", "110109", "Cookery", 410, "Detailed" },
                    { 414, "Baking and Pastrymaking is the study of making and presenting breads, pastries, buns and cakes.", "110107", "Baking and Pastrymaking", 410, "Detailed" },
                    { 425, "Literacy and Numeracy Programmes develop basic reading, writing and numeracy skills.", "120103", "Literacy and Numeracy Programmes", 423, "Detailed" },
                    { 327, "Youth Work is the study of the development and support of youth.", "090505", "Youth Work", 324, "Detailed" },
                    { 382, "This detailed field includes all Society and Culture not elsewhere classified.", "099999", "Society and Culture, n.e.c.", 378, "Detailed" },
                    { 380, "Criminology is the study of crime and issues related to offenders and victims.", "099903", "Criminology", 378, "Detailed" },
                    { 349, "Police Studies is the study of the maintenance of law and order, and crime detection and prevention.", "091105", "Police Studies", 346, "Detailed" },
                    { 348, "Legal Studies is the study of the administrative and legal skills required to assist in legal practice.", "091103", "Legal Studies", 346, "Detailed" },
                    { 347, "Justice Administration is the study of the theory and practice of the administrative processes in the justice system.", "091101", "Justice Administration", 346, "Detailed" },
                    { 345, "This detailed field includes all Law not elsewhere classified.", "090999", "Law, n.e.c.", 337, "Detailed" },
                    { 344, "Legal Practice is the study of the issues associated with practising law. It includes the methods and principles involved in advising and representing clients on matters relating to law.", "090913", "Legal Practice", 337, "Detailed" },
                    { 343, "Taxation Law is the study of principles and regulations related to government collection of revenues from corporate and individual tax payers.", "090911", "Taxation Law", 337, "Detailed" },
                    { 342, "International Law is the study of the system of rules governing the conduct of international relations, and the relationship between Australian laws and the laws of other countries.", "090909", "International Law", 337, "Detailed" },
                    { 341, "Family Law is the study of the principles and regulations in relation to the family.", "090907", "Family Law", 337, "Detailed" },
                    { 350, "This detailed field includes all Justice and Law Enforcement not elsewhere classified.", "091199", "Justice and Law Enforcement, n.e.c.", 346, "Detailed" },
                    { 340, "Criminal Law is the study of the principles and practices associated with the body of law which deals with criminal offences.", "090905", "Criminal Law", 337, "Detailed" },
                    { 338, "Business and Commercial Law is the study of the principles and regulations governing business and commercial practices.", "090901", "Business and Commercial Law", 337, "Detailed" },
                    { 336, "This detailed field includes all Behavioural Science not elsewhere classified.", "090799", "Behavioural Science, n.e.c.", 334, "Detailed" },
                    { 335, "Psychology is the study of the science of human nature and of mental states and processes. It includes the study of human and animal nature.", "090701", "Psychology", 334, "Detailed" },
                    { 333, "This detailed field includes all Human Welfare Studies and Services not elsewhere classified.", "090599", "Human Welfare Studies and Services, n.e.c.", 324, "Detailed" },
                    { 332, "Welfare Studies is the study of providing personal services to individuals and family groups who are socially disadvantaged or otherwise in need.", "090515", "Welfare Studies", 324, "Detailed" },
                    { 331, "Counselling is the study of the practice and skills required to provide guidance on personal, social and psychological problems.", "090513", "Counselling", 324, "Detailed" },
                    { 330, "Residential Client Care is the study of caring for people living in a variety of welfare and residential settings.", "090511", "Residential Client Care", 324, "Detailed" },
                    { 329, "Care for the Disabled is the study of caring for disabled people in various social support services associated with families and groups in the community.", "090509", "Care for the Disabled", 324, "Detailed" },
                    { 339, "Constitutional Law is the study of the principles and regulations in relation to the constitution and the respective powers of the federal and state governments.", "090903", "Constitutional Law", 337, "Detailed" },
                    { 381, "Security Services is the study of protecting properties, premises and people against intruders and damage.", "099905", "Security Services", 378, "Detailed" },
                    { 352, "Librarianship and Information Management is the study of selecting, acquiring, organising and storing collections of information, and facilitating the use of information.", "091301", "Librarianship and Information Management", 351, "Detailed" },
                    { 355, "English Language is the study of reading, writing and speaking the English language.", "091501", "English Language", 354, "Detailed" },
                    { 379, "Family and Consumer Studies is the study of the wellbeing of individuals and families and the way that they manage resources to achieve their goals.", "099901", "Family and Consumer Studies", 378, "Detailed" },
                    { 377, "This detailed field includes all Sport and Recreation not elsewhere classified.", "092199", "Sport and Recreation, n.e.c", 374, "Detailed" },
                    { 376, "Sports Coaching, Officiating and Instruction is the study of the techniques for coaching and instructing individuals and teams in various sporting activities. It includes studying the techniques of officiating at various sporting events.", "092103", "Sports Coaching, Officiating and Instruction", 374, "Detailed" },
                    { 375, "Sport and Recreation Activities is the study of the theory and practice of participating in various sporting and recreational activities.", "092101", "Sport and Recreation Activities", 374, "Detailed" },
                    { 373, "Econometrics is the study of analysing and describing economic data using mathematical and statistical methods.", "091903", "Econometrics", 371, "Detailed" },
                    { 372, "Economics is the study of the production, consumption and transfer of wealth.", "091901", "Economics", 371, "Detailed" },
                    { 367, "This detailed field includes all Language and Literature not elsewhere classified.", "091599", "Language and Literature, n.e.c.", 354, "Detailed" },
                    { 366, "Literature is the study of written works valued for form of writing or expression.", "091523", "Literature", 354, "Detailed" },
                    { 353, "Curatorial Studies is the study of locating, identifying and assessing cultural heritage resources.", "091303", "Curatorial Studies", 351, "Detailed" },
                    { 365, "Linguistics is the study of the structure and composition of language.", "091521", "Linguistics", 354, "Detailed" },
                    { 363, "Australian Indigenous Languages is the study of reading, writing and speaking the languages of the Australian indigenous people.", "091517", "Australian Indigenous Languages", 354, "Detailed" },
                    { 362, "Eastern Asian Languages is the study of reading, writing and speaking the languages of Eastern Asia.", "091515", "Eastern Asian Languages", 354, "Detailed" },
                    { 361, "Southeast Asian Languages is the study of reading, writing and speaking the languages of Southeast Asia.", "091513", "Southeast Asian Languages", 354, "Detailed" },
                    { 360, "Southern Asian Languages is the study of reading, writing and speaking the languages of Southern Asia.", "091511", "Southern Asian Languages", 354, "Detailed" },
                    { 359, "Southwest Asian and North African Languages is the study of reading, writing and speaking the languages of Southwest Asia and North Africa.", "091509", "Southwest Asian and North African Languages", 354, "Detailed" },
                    { 358, "Eastern European Languages is the study of the reading, writing and speaking the languages of Eastern Europe.", "091507", "Eastern European Languages", 354, "Detailed" },
                    { 357, "Southern European Languages is the study of reading, writing and speaking the languages of Southern Europe.", "091505", "Southern European Languages", 354, "Detailed" },
                    { 356, "Northern European Languages is the study of reading, writing and speaking the languages of Northern Europe.", "091503", "Northern European Languages", 354, "Detailed" },
                    { 364, "Translating and Interpreting is the study of translating and interpreting foreign languages into the mother tongue and vice versa.", "091519", "Translating and Interpreting", 354, "Detailed" },
                    { 214, "Dental Technology is the study of designing, making and repairing dental prostheses and orthodontic appliances.", "060705", "Dental Technology", 211, "Detailed" },
                    { 213, "Dental Assisting is the study of providing assistance to dentists in clinical settings.", "060703", "Dental Assisting", 211, "Detailed" },
                    { 212, "Dentistry is the study of diagnosing, treating and preventing diseases of the teeth and adjacent tissues. It includes correcting malocclusion and restoring and replacing missing dental and oral structures.", "060701", "Dentistry", 211, "Detailed" },
                    { 77, "Food Processing Technology is the study of the industrial processing, packaging and handling of food.", "030307", "Food Processing Technology", 73, "Detailed" },
                    { 76, "Materials Engineering is the study of assaying, producing and refining materials, including metals, alloys, ceramics and polymers, timber, pulp and paper.", "030305", "Materials Engineering", 73, "Detailed" },
                    { 75, "Mining Engineering is the study of planning, developing, assessing, directing and managing the extraction of minerals, oil and gas from the earth.", "030303", "Mining Engineering", 73, "Detailed" },
                    { 74, "Chemical Engineering is the study of planning, designing and developing products and processes where chemical and physical changes occur.", "030301", "Chemical Engineering", 73, "Detailed" },
                    { 72, "This detailed field includes all Manufacturing Engineering and Technology not elsewhere classified.", "030199", "Manufacturing Engineering and Technology, n.e.c.", 62, "Detailed" },
                    { 71, "Furniture Polishing is the study of preparing and polishing different timber furniture surfaces.", "030117", "Furniture Polishing", 62, "Detailed" },
                    { 70, "Furniture Upholstery and Renovation is the study of designing, making and repairing the soft furnishings of chairs, beds and other furniture.", "030115", "Furniture Upholstery and Renovation", 62, "Detailed" },
                    { 69, "Cabinet Making is the study of making and repairing furniture and interior fittings for buildings.", "030113", "Cabinet Making", 62, "Detailed" },
                    { 78, "This detailed field includes all Process and Resources Engineering not elsewhere classified.", "030399", "Process and Resources Engineering, n.e.c.", 73, "Detailed" },
                    { 68, "Wood Machining and Turning is the study of shaping wood using various machines.", "030111", "Wood Machining and Turning", 62, "Detailed" },
                    { 66, "Garment Making is the study of the commercial production of clothing and other apparel.", "030107", "Garment Making", 62, "Detailed" },
                    { 65, "Textile Making is the study of the commercial production of textiles, yarns and fabrics.", "030105", "Textile Making", 62, "Detailed" },
                    { 64, "Printing is the study of reproducing texts and pictorial works onto any media from original plates and masters, and producing finished publications. It includes electronic (desktop) publishing.", "030103", "Printing", 62, "Detailed" },
                    { 63, "Manufacturing Engineering is the study of designing, developing and organising safe and flexible manufacturing processes, systems and facilities.", "030101", "Manufacturing Engineering", 62, "Detailed" },
                    { 60, "This detailed field includes all Information Technology not elsewhere classified.", "029999", "Information Technology, n.e.c.", 58, "Detailed" },
                    { 59, "Security Science is the study of securing electronic information and preventing unauthorised access to data and programs.", "029901", "Security Science", 58, "Detailed" },
                    { 57, "This detailed field includes all Information Systems not elsewhere classified.", "020399", "Information Systems, n.e.c.", 52, "Detailed" },
                    { 56, "Decision Support Systems is the study of designing information systems based on statistical and mathematical models to support management decisions.", "020307", "Decision Support Systems", 52, "Detailed" },
                    { 67, "Footwear Making is the study of designing, making and repairing shoes, boots and other footwear.", "030109", "Footwear Making", 62, "Detailed" },
                    { 55, "Systems Analysis and Design is the study of analysing the information needs of the user and designing or modifying a system to meet these needs.", "020305", "Systems Analysis and Design", 52, "Detailed" },
                    { 80, "Automotive Engineering is the study of designing, developing and testing motor vehicles, earth moving equipment, small engines and their components.", "030501", "Automotive Engineering", 79, "Detailed" },
                    { 82, "Automotive Electrics and Electronics is the study of installing, maintaining and repairing electrical wiring and electronic components in motor vehicles, boats and earth moving equipment.", "030505", "Automotive Electrics and Electronics", 79, "Detailed" },
                    { 102, "Structural Engineering is the study of the statical properties of structures and the behaviour and durability of materials used for erecting structures.", "030903", "Structural Engineering", 100, "Detailed" },
                    { 101, "Construction Engineering is the study of designing and developing infrastructure such as buildings, roads, bridges, tunnels and quarries.", "030901", "Construction Engineering", 100, "Detailed" },
                    { 99, "This detailed field includes all Mechanical and Industrial Engineering and Technology not elsewhere classified.", "030799", "Mechanical and Industrial Engineering and Technology, n.e.c.", 89, "Detailed" },
                    { 98, "Plant and Machine Operations is the study of setting up, controlling and monitoring mobile and stationary plant, equipment and machinery either directly or by remote control.", "030717", "Plant and Machine Operations", 89, "Detailed" },
                    { 97, "Precision Metalworking is the study of designing, fabricating, assembling, maintaining and repairing precision instruments such as locks, timepieces and firearms.", "030715", "Precision Metalworking", 89, "Detailed" },
                    { 96, "Metal Casting and Patternmaking is the study of planning and fabricating mould patterns and cores for the production of metal castings.", "030713", "Metal Casting and Patternmaking", 89, "Detailed" },
                    { 95, "Boilermaking and Welding is the study of marking out, cutting, shaping and joining metals, and constructing and repairing steelwork structures and pressure vessels including boilers, piping systems and ships.", "030711", "Boilermaking and Welding", 89, "Detailed" },
                    { 94, "Sheetmetal Working is the study of cutting, shaping and joining sheetmetal and other materials, and using hand tools, power tools and machines to make products and components.", "030709", "Sheetmetal Working", 89, "Detailed" },
                    { 81, "Vehicle Mechanics is the study of maintaining, diagnosing faults in, repairing and servicing motor vehicles and their components and small engines in boats, motorcycles, lawnmowers, generators and related equipment.", "030503", "Vehicle Mechanics", 79, "Detailed" },
                    { 93, "Metal Fitting, Turning and Machining is the study of setting up machining tools, production machines and textile machines, operating machining tools and machines to shape metal stock and castings, and fitting and assembling the fabricated metal parts into products.", "030707", "Metal Fitting, Turning and Machining", 89, "Detailed" },
                    { 91, "Industrial Engineering is the study of planning, designing, organising and operating industrial facilities and processes for the economic, safe and effective use of physical and human resources.", "030703", "Industrial Engineering", 89, "Detailed" },
                    { 90, "Mechanical Engineering is the study of planning, designing and developing machines, mechanical plant and systems.", "030701", "Mechanical Engineering", 89, "Detailed" },
                    { 88, "This detailed field includes all Automotive Engineering and Technology not elsewhere classified.", "030599", "Automotive Engineering and Technology, n.e.c.", 79, "Detailed" },
                    { 87, "Automotive Vehicle Operations is the study of driving motor vehicles including motor cycles, trucks and buses.", "030515", "Automotive Vehicle Operations", 79, "Detailed" },
                    { 86, "Upholstery and Vehicle Trimming is the study of making, installing, repairing and replacing the interior trimmings and upholstery of motor vehicles, boats, aircraft and railway carriages.", "030513", "Upholstery and Vehicle Trimming", 79, "Detailed" },
                    { 85, "Panel Beating is the study of repairing damaged motor vehicle bodies and replacing panels.", "030511", "Panel Beating", 79, "Detailed" },
                    { 84, "Automotive Body Construction is the study of building bodies for motor vehicles, trailers, buses, and railway rolling stock.", "030509", "Automotive Body Construction", 79, "Detailed" },
                    { 83, "Automotive Vehicle Refinishing is the study of preparing vehicle surfaces, mixing and matching paint colours, spray painting and detailing motor vehicles and aircraft.", "030507", "Automotive Vehicle Refinishing", 79, "Detailed" },
                    { 92, "Toolmaking is the study of making and repairing tools, dies, jigs, fixtures and other precision parts and equipment.", "030705", "Toolmaking", 89, "Detailed" },
                    { 103, "Building Services Engineering is the study of designing and developing infrastructure aimed at enhancing human comfort through controlling heat, light and sound in built environments.", "030905", "Building Services Engineering", 100, "Detailed" },
                    { 54, "Database Management is the study of programs which create and maintain databases.", "020303", "Database Management", 52, "Detailed" },
                    { 51, "This detailed field includes all Computer Science not elsewhere classified.", "020199", "Computer Science, n.e.c.", 40, "Detailed" },
                    { 25, "Ecology and Evolution is the study of interactions between organisms and their environment, and the processes of biological change in organisms.", "010905", "Ecology and Evolution", 22, "Detailed" },
                    { 24, "Botany is the study of plants.", "010903", "Botany", 22, "Detailed" },
                    { 23, "Biochemistry and Cell Biology is the study of the chemistry of living organisms and the structure and function of cells.", "010901", "Biochemistry and Cell Biology", 22, "Detailed" },
                    { 21, "This detailed field includes all Earth Sciences not elsewhere classified.", "010799", "Earth Sciences, n.e.c.", 13, "Detailed" },
                    { 20, "Oceanography is the study of the origins, composition, structure and history of the oceans and the ocean floor.", "010713", "Oceanography", 13, "Detailed" },
                    { 19, "Hydrology is the study of the location and movement of inland water, both frozen and liquid, above and below ground.", "010711", "Hydrology", 13, "Detailed" },
                    { 18, "Soil Science is the study of the origins, composition, structure and classification of soils.", "010709", "Soil Science", 13, "Detailed" },
                    { 17, "Geochemistry is the study of the quantities and distribution of the chemical elements in minerals, ores, rocks, soils, water, and the atmosphere.", "010707", "Geochemistry", 13, "Detailed" },
                    { 26, "Marine Science is the study of marine and estuarine plants and wildlife. It includes freshwater science.", "010907", "Marine Science", 22, "Detailed" },
                    { 16, "Geophysics is the study of the physical properties of the Earth through collecting and interpreting gravitational, magnetic, seismic and electrical data.", "010705", "Geophysics", 13, "Detailed" },
                    { 14, "Atmospheric Sciences is the study of the composition and structure of the earth’s atmosphere and climate.", "010701", "Atmospheric Sciences", 13, "Detailed" },
                    { 12, "This detailed field includes all Chemical Sciences not elsewhere classified.", "010599", "Chemical Sciences, n.e.c.", 9, "Detailed" },
                    { 11, "Inorganic Chemistry is the study of the description, properties, reactions, and preparation of all the elements and their compounds, with the exception of carbon compounds.", "010503", "Inorganic Chemistry", 9, "Detailed" },
                    { 10, "Organic Chemistry is the study of the description, properties, reactions and preparations of carbon compounds.", "010501", "Organic Chemistry", 9, "Detailed" },
                    { 8, "Astronomy is the study of celestial bodies, their positions, motions, distances and physical conditions, and their origins and evolution.", "010303", "Astronomy", 6, "Detailed" },
                    { 7, "Physics is the study of the laws governing states and properties of matter and energy.", "010301", "Physics", 6, "Detailed" },
                    { 5, "This detailed field includes all Mathematical Sciences not elsewhere classified.", "010199", "Mathematical Sciences, n.e.c.", 2, "Detailed" },
                    { 4, "Statistics is the study of collecting, describing, arranging and analysing numerical data.", "010103", "Statistics", 2, "Detailed" },
                    { 15, "Geology is the study of origin, composition and structure of the earth.", "010703", "Geology", 13, "Detailed" },
                    { 53, "Conceptual Modelling is the study of representing the structure, type, and relationships of data elements within a system used to support system design.", "020301", "Conceptual Modelling", 52, "Detailed" },
                    { 27, "Genetics is the study of heredity and the units of biological inheritance.", "010909", "Genetics", 22, "Detailed" },
                    { 29, "Human Biology is the study of human and primate anatomy, physiology, evolution and biosocial interactions.", "010913", "Human Biology", 22, "Detailed" },
                    { 50, "Artificial Intelligence is the study of creating computer programs which demonstrate some of the attributes associated with human intelligence.", "020119", "Artificial Intelligence", 40, "Detailed" },
                    { 49, "Operating Systems is the study of designing and constructing systems to control processes and process scheduling.", "020117", "Operating Systems", 40, "Detailed" },
                    { 48, "Computer Graphics is the study of developing and programming graphical output devices to generate pictures.", "020115", "Computer Graphics", 40, "Detailed" },
                    { 47, "Networks and Communications is the study of linking computers for information exchange and distribution.", "020113", "Networks and Communications", 40, "Detailed" },
                    { 46, "Data Structures is the study of the system of relationships between items of data which permit efficient manipulation through reducing complexity.", "020111", "Data Structures", 40, "Detailed" },
                    { 45, "Algorithms is the study of the processes and rules that describe the logical sequence of operations to be performed by a program.", "020109", "Algorithms", 40, "Detailed" },
                    { 44, "Compiler Construction is the study of the theories and techniques for translating instructions between high and low level languages.", "020107", "Compiler Construction", 40, "Detailed" },
                    { 43, "Computational Theory is the study of problems that can be solved using efficient algorithms and the identification of problems that are unsolvable.", "020105", "Computational Theory", 40, "Detailed" },
                    { 28, "Microbiology is the study of microscopic forms of life such as bacteria, viruses and protozoa.", "010911", "Microbiology", 22, "Detailed" },
                    { 42, "Programming is the study of writing coded instructions for computers to perform particular tasks.", "020103", "Programming", 40, "Detailed" },
                    { 38, "This detailed field includes all Natural and Physical Sciences not elsewhere classified.", "019999", "Natural and Physical Sciences, n.e.c.", 32, "Detailed" },
                    { 37, "Laboratory Technology is the study of the procedures, techniques and equipment used in biological, chemical, medical and other laboratories.", "019909", "Laboratory Technology", 32, "Detailed" },
                    { 36, "Pharmacology is the study of the development, uses and effects of drugs.", "019907", "Pharmacology", 32, "Detailed" },
                    { 35, "Food Science and Biotechnology is the study of the physical and chemical properties of food, and the industrial use of living organisms to produce food, pharmaceuticals and other products.", "019905", "Food Science and Biotechnology", 32, "Detailed" },
                    { 34, "Forensic Science is the study of the application of scientific techniques to criminal investigations.", "019903", "Forensic Science", 32, "Detailed" },
                    { 33, "Medical Science is the study of the application of physics, biology and chemistry to medicine.", "019901", "Medical Science", 32, "Detailed" },
                    { 31, "This detailed field includes all Biological Sciences not elsewhere classified.", "010999", "Biological Sciences, n.e.c.", 22, "Detailed" },
                    { 30, "Zoology is the study of animals and insects.", "010915", "Zoology", 22, "Detailed" },
                    { 41, "Formal Language Theory is the study of automated formal languages and the algorithms used to recognise them.", "020101", "Formal Language Theory", 40, "Detailed" },
                    { 104, "Water and Sanitary Engineering is the study of designing and developing water storage and distribution systems, and sludge, effluent and waste water treatment systems.", "030907", "Water and Sanitary Engineering", 100, "Detailed" },
                    { 105, "Transport Engineering is the study of planning and developing efficient transport systems.", "030909", "Transport Engineering", 100, "Detailed" },
                    { 106, "Geotechnical Engineering is the study of analysing foundations, slopes and soil mechanics, and designing foundations.", "030911", "Geotechnical Engineering", 100, "Detailed" },
                    { 184, "Pest and Weed Control is the study of controlling noxious plants, animals, insects and fungi.", "050901", "Pest and Weed Control", 183, "Detailed" },
                    { 182, "This detailed field includes all Environmental Studies not elsewhere classified.", "050999", "Environmental Studies, n.e.c.", 180, "Detailed" },
                    { 181, "Land, Parks and Wildlife Management is the study of managing land, parkland, marine and coastal zone parks, and wildlife.", "050901", "Land, Parks and Wildlife Management", 180, "Detailed" },
                    { 179, "This detailed field includes all Fisheries Studies not elsewhere classified.", "050799", "Fisheries Studies, n.e.c.", 177, "Detailed" },
                    { 178, "Aquaculture is the study of breeding, rearing, harvesting, handling and processing fish and other aquatic resources.", "050701", "Aquaculture", 177, "Detailed" },
                    { 176, "Forestry Studies is the study of establishing, cultivating, harvesting and managing forests.", "050501", "Forestry Studies", 175, "Detailed" },
                    { 174, "Viticulture is the study of cultivating and managing grapevines.", "050303", "Viticulture", 172, "Detailed" },
                    { 173, "Horticulture is the study of cultivating, propagating and producing intensively managed crops such as fruits (except grapes), vegetables, flowers, foliage, trees, shrubs and plants.", "050301", "Horticulture", 172, "Detailed" },
                    { 185, "This detailed field includes all Agriculture, Environmental and Related Studies not elsewhere classified.", "050999", "Agriculture, Environmental and Related Studies, n.e.c.", 183, "Detailed" },
                    { 171, "This detailed field includes all Agriculture not elsewhere classified.", "050199", "Agriculture, n.e.c.", 167, "Detailed" },
                    { 169, "Wool Science is the study of producing, handling and classing wool and other fleece.", "050103", "Wool Science", 167, "Detailed" },
                    { 168, "Agricultural Science is the study of the non-intensive farming of animals and plants.", "050101", "Agricultural Science", 167, "Detailed" },
                    { 165, "This detailed field includes all Building not elsewhere classified.", "040399", "Building, n.e.c.", 149, "Detailed" },
                    { 164, "Scaffolding and Rigging is the study of erecting and dismantling modular scaffolding and rigging.", "040329", "Scaffolding and Rigging", 149, "Detailed" },
                    { 163, "Plumbing is the study of designing, installing, maintaining and repairing pipelines, fixtures, fittings and related equipment for water, steam, gas, sewage and other liquids in residential, public commercial and industrial establishments.", "040327", "Plumbing", 149, "Detailed" },
                    { 162, "Painting, Decorating and Sign Writing is the study of applying paint, varnish and paper finishes to decorate and protect interior and exterior surfaces. It includes the study of designing and painting signs.", "040325", "Painting, Decorating and Sign Writing", 149, "Detailed" },
                    { 161, "Glazing is the study of installing and maintaining glass panes, mirrors and windscreens.", "040323", "Glazing", 149, "Detailed" },
                    { 160, "Floor Coverings is the study of measuring and laying parquetry, carpet, cork, linoleum, vinyl and other resilient floor coverings.", "040321", "Floor Coverings", 149, "Detailed" },
                    { 170, "Animal Husbandry is the study of the intensive management of animals, their training, handling, care, breeding and rearing.", "050105", "Animal Husbandry", 167, "Detailed" },
                    { 159, "Furnishing Installation is the study of measuring and installing curtains, blinds, awnings and other soft furnishings.", "040319", "Furnishing Installation", 149, "Detailed" },
                    { 188, "General Medicine is the study of the clinical presentation and treatment of diseases.", "060101", "General Medicine", 187, "Detailed" },
                    { 190, "Psychiatry is the study of the medical specialisation concerned with diagnosing, preventing and treating diseases and disorders of the mind.", "060105", "Psychiatry", 187, "Detailed" },
                    { 210, "Pharmacy is the study of the preparation and dispensing of drugs.", "060501", "Pharmacy", 209, "Detailed" },
                    { 208, "This detailed field includes all Nursing not elsewhere classified.", "060399", "Nursing, n.e.c.", 199, "Detailed" },
                    { 207, "Mothercraft Nursing and Family and Child Health Nursing is the study of the nursing specialisation concerned with the theory and practice of caring for young children and their families.", "060315", "Mothercraft Nursing and Family and Child Health Nursing", 199, "Detailed" },
                    { 206, "Palliative Care Nursing is the study of the nursing specialisation concerned with caring for the terminally ill and their families.", "060313", "Palliative Care Nursing", 199, "Detailed" },
                    { 205, "Aged Care Nursing is the study of the nursing specialisation concerned with the theory and practice of caring for elderly patients.", "060311", "Aged Care Nursing", 199, "Detailed" },
                    { 204, "Critical Care Nursing is the study of the nursing specialisation concerned with the theory and practice of caring for patients requiring intensive therapy.", "060309", "Critical Care Nursing", 199, "Detailed" },
                    { 203, "Community Nursing is the study of the nursing specialisation concerned with caring for individuals and groups in the community.", "060307", "Community Nursing", 199, "Detailed" },
                    { 202, "Mental Health Nursing is the study of the nursing specialisation concerned with caring for persons with mental health problems.", "060305", "Mental Health Nursing", 199, "Detailed" },
                    { 189, "Surgery is the study of the medical specialisation concerned with principles and practices for treating diseases, injuries, defects and deformities by manual operation and manipulation, and by using instruments and appliances.", "060103", "Surgery", 187, "Detailed" },
                    { 201, "Midwifery is the study of the principles and practices of providing care for women during pregnancy, birthing and after childbirth and for caring for the new-born. It includes assisting parents and families during childbearing.", "060303", "Midwifery", 199, "Detailed" },
                    { 198, "This detailed field includes all Medical Studies not elsewhere classified.", "060199", "Medical Studies, n.e.c.", 187, "Detailed" },
                    { 197, "General Practice is the study of providing primary and continuing medical care to patients in a community setting. It is particularly concerned with providing family and community oriented health care.", "060119", "General Practice", 187, "Detailed" },
                    { 196, "Internal Medicine is the study of the medical specialisation concerned with the diagnosis and non-surgical treatment of diseases and disorders of specific bodily structures and functions.", "060117", "Internal Medicine", 187, "Detailed" },
                    { 195, "Radiology is the study of the medical specialisation concerned with using radioactive substances, X-rays and other ionising radiation to diagnose, treat and follow the course of disease and its response to treatment.", "060115", "Radiology", 187, "Detailed" },
                    { 194, "Pathology is the study of the medical specialisation concerned with the causes and effects of diseases, including the structural and functional changes of body organs, tissues and fluids, and the systematic methods of detecting these changes.", "060113", "Pathology", 187, "Detailed" },
                    { 193, "Anaesthesiology is the study of the medical specialisation concerned with administering drugs and other substances to achieve lack of sensation with or without loss of consciousness.", "060111", "Anaesthesiology", 187, "Detailed" },
                    { 192, "Paediatrics is the study of the medical specialisation concerned with normal physical growth and development from birth through to late adolescence. It includes preventing, diagnosing and treating diseases and uncommon disorders in children and adolescents.", "060109", "Paediatrics", 187, "Detailed" },
                    { 191, "Obstetrics and Gynaecology is the study of the medical specialisation concerned with the care of women during pregnancy, labour and after childbirth, and the prevention, diagnosis and treatment of diseases of the female reproductive system.", "060107", "Obstetrics and Gynaecology", 187, "Detailed" },
                    { 200, "General Nursing is the study of the principles and practices of providing preventative, curative and rehabilitative care to individuals and groups.", "060301", "General Nursing", 199, "Detailed" },
                    { 158, "Plastering is the study of fixing fibrous sheets and applying plaster, cement-based and similar materials to ceilings, interior and exterior walls of buildings and the study of making plasterboard and plaster mouldings.", "040317", "Plastering", 149, "Detailed" },
                    { 157, "Roof Fixing is the study of covering structures with roof tiles, shingles and other materials to form a waterproof surface.", "040315", "Roof Fixing", 149, "Detailed" },
                    { 156, "Ceiling, Wall and Floor Fixing is the study of fixing tiles and lining materials to ceilings, walls and floors. It includes fixing cladding materials to external surfaces.", "040313", "Ceiling, Wall and Floor Fixing", 149, "Detailed" },
                    { 127, "Aircraft Operation is the study of piloting and navigating aircraft.", "031505", "Aircraft Operation", 124, "Detailed" },
                    { 126, "Aircraft Maintenance Engineering is the study of assembling, maintaining and repairing aircraft structures, and avionic and mechanical systems.", "031503", "Aircraft Maintenance Engineering", 124, "Detailed" },
                    { 125, "Aerospace Engineering is the study of designing, developing and modifying aircraft.", "031501", "Aerospace Engineering", 124, "Detailed" },
                    { 123, "This detailed field includes all Electrical and Electronic Engineering and Technology not elsewhere classified.", "031399", "Electrical and Electronic Engineering and Technology, n.e.c.", 113, "Detailed" },
                    { 122, "Electronic Equipment Servicing is the study of maintaining, diagnosing faults in and repairing computers, radio and television receivers, audio, video, and other electronic business and domestic equipment.", "031317", "Electronic Equipment Servicing", 113, "Detailed" },
                    { 121, "Refrigeration and Air Conditioning Mechanics is the study of installing, maintaining, diagnosing faults in and repairing domestic, commercial and industrial refrigeration, air conditioning and heating equipment.", "031315", "Refrigeration and Air Conditioning Mechanics", 113, "Detailed" },
                    { 120, "Electrical Fitting, Electrical Mechanics is the study of installing, maintaining, diagnosing faults in and repairing electrical wiring and equipment in domestic, commercial and industrial establishments, ships and trains.", "031313", "Electrical Fitting, Electrical Mechanics", 113, "Detailed" },
                    { 119, "Powerline Installation and Maintenance is the study of installing, repairing, maintaining and monitoring overhead and underground electrical power distribution networks.", "031311", "Powerline Installation and Maintenance", 113, "Detailed" },
                    { 128, "Air Traffic Control is the study of monitoring and directing ground and air aircraft movements.", "031507", "Air Traffic Control", 124, "Detailed" },
                    { 118, "Communications Equipment Installation and Maintenance is the study of installing, maintaining, operating, diagnosing and repairing faults in telecommunications equipment, appliances, instruments and systems.", "031309", "Communications Equipment Installation and Maintenance", 113, "Detailed" },
                    { 116, "Computer Engineering is the study of designing and constructing digital data processing hardware.", "031305", "Computer Engineering", 113, "Detailed" },
                    { 115, "Electronic Engineering is the study of planning, designing, developing and maintaining electronic equipment, machinery and systems.", "031303", "Electronic Engineering", 113, "Detailed" },
                    { 114, "Electrical Engineering is the study of planning, designing, developing and maintaining electrical equipment, circuits and systems.", "031301", "Electrical Engineering", 113, "Detailed" },
                    { 112, "This detailed field includes all Geomatic Engineering not elsewhere classified.", "031199", "Geomatic Engineering, n.e.c.", 109, "Detailed" },
                    { 111, "Mapping Science is the study of graphically representing the constructed and natural features of the earth in the form of maps.", "031103", "Mapping Science", 109, "Detailed" },
                    { 110, "Surveying is the study of measuring and representing the shape, contours, locations and dimensions of the constructed and natural features of the earth, in the form of reports and plans.", "031101", "Surveying", 109, "Detailed" },
                    { 108, "This detailed field includes all Civil Engineering not elsewhere classified.", "030999", "Civil Engineering, n.e.c.", 100, "Detailed" },
                    { 107, "Ocean Engineering is the study of designing, constructing and maintaining coastal and ocean related projects and facilities, and designing floating, fixed and sub-sea offshore systems.", "030913", "Ocean Engineering", 100, "Detailed" },
                    { 117, "Communications Technologies is the study of communication transmission and signal systems.", "031307", "Communications Technologies", 113, "Detailed" },
                    { 129, "This detailed field includes all Aerospace Engineering and Technology not elsewhere classified.", "031599", "Aerospace Engineering and Technology, n.e.c.", 124, "Detailed" },
                    { 131, "Maritime Engineering is the study of maintaining and operating shipboard machinery and systems.", "031701", "Maritime Engineering", 130, "Detailed" },
                    { 132, "Marine Construction is the study of fabricating, fitting out and repairing marine vessels and their structural components.", "031703", "Marine Construction", 130, "Detailed" },
                    { 155, "Carpentry and Joinery is the study of fabricating, assembling, installing, renovating and repairing doors, frames, formwork and other fittings in buildings.", "040311", "Carpentry and Joinery", 149, "Detailed" },
                    { 154, "Bricklaying and Stonemasonry is the study of cutting, shaping, laying and joining bricks, stone and building blocks to construct and repair all types of masonry structures.", "040309", "Bricklaying and Stonemasonry", 149, "Detailed" },
                    { 153, "Building Construction Economics is the study of estimating quantities of materials and total costs involved in building and construction.", "040307", "Building Construction Economics", 149, "Detailed" },
                    { 152, "Building Surveying is the study of assessing the condition of buildings to ensure compliance with plans, specification and regulations and that proper techniques and materials have been used.", "040305", "Building Surveying", 149, "Detailed" },
                    { 151, "Building Construction Management is the study of planning and controlling building projects.", "040303", "Building Construction Management", 149, "Detailed" },
                    { 150, "Building Science and Technology is the study of the theories and techniques required to construct and maintain structures.", "040301", "Building Science and Technology", 149, "Detailed" },
                    { 148, "This detailed field includes all Architecture and Urban Environment not elsewhere classified.", "040199", "Architecture and Urban Environment, n.e.c.", 143, "Detailed" },
                    { 147, "Interior and Environmental Design is the study of planning and designing interior environments of homes, offices, factories and other buildings with concern for functionality, practicality and human needs and requirements.", "040107", "Interior and Environmental Design", 143, "Detailed" },
                    { 146, "Landscape Architecture is the study of planning, designing and installing exterior environments with concern for functionality, practicality and human needs and requirements. It includes the design of parks and gardens integrated with the built environment.", "040105", "Landscape Architecture", 143, "Detailed" },
                    { 145, "Urban Design and Regional Planning is the study of designing and planning towns and rural communities to meet the needs of the population.", "040103", "Urban Design and Regional Planning", 143, "Detailed" },
                    { 144, "Architecture is the study of the art, science and techniques of building design. It includes utilitarian ends, such as the soundness of the structure, the functional and economic efficiency of the building, and aesthetic considerations.", "040101", "Architecture", 143, "Detailed" },
                    { 141, "This detailed field includes all Engineering and Related Technologies not elsewhere classified.", "039999", "Engineering and Related Technologies, n.e.c.", 135, "Detailed" },
                    { 140, "Cleaning is the study of removing dirt and stains from, and maintaining and restoring clothing and fabrics, and domestic, industrial and commercial assets.", "039909", "Cleaning", 135, "Detailed" },
                    { 139, "Rail Operations is the study of driving, shunting and marshalling trains.", "039907", "Rail Operations", 135, "Detailed" },
                    { 138, "Fire Technology is the study of fire detection, suppression and prevention methods and equipment.", "039905", "Fire Technology", 135, "Detailed" },
                    { 137, "Biomedical Engineering is the study of designing and manufacturing medical devices and equipment to assist in overcoming physical disabilities.", "039903", "Biomedical Engineering", 135, "Detailed" },
                    { 136, "Environmental Engineering is the study of technology concerned with the mitigation of pollution, contamination and deterioration of the environment.", "039901", "Environmental Engineering", 135, "Detailed" },
                    { 134, "This detailed field includes all Maritime Engineering and Technology not elsewhere classified.", "031799", "Maritime Engineering and Technology, n.e.c.", 130, "Detailed" },
                    { 133, "Marine Craft Operation is the study of operating ships and other marine craft and their navigation and communication systems.", "031705", "Marine Craft Operation", 130, "Detailed" },
                    { 437, "This detailed field includes all Employment Skills Programmes not elsewhere classified.", "120599", "Employment Skills Programmes, n.e.c.", 433, "Detailed" },
                    { 439, "This detailed field includes all Mixed Field Programmes not elsewhere classified.", "129999", "Mixed Field Programmes, n.e.c.", 438, "Detailed" }
                });

            migrationBuilder.InsertData(
                table: "field_of_education",
                columns: new[] { "id", "description", "field_code", "name", "parent_field_of_education", "type" },
                values: new object[,]
                {
                    { 369, "Philosophy is the study of the fundamental nature of knowledge, reality and existence.", "091701", "Philosophy", 358, "Detailed" },
                    { 370, "Religious Studies is the study of a set of beliefs and practices, usually involving acknowledgement of a divine or higher being or power, by which people order the conduct of their lives both practically and in a moral sense.", "091703", "Religious Studies", 358, "Detailed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_assessment_assessment_type",
                table: "assessment",
                column: "assessment_type");

            migrationBuilder.CreateIndex(
                name: "IX_assessment_course_instance",
                table: "assessment",
                column: "course_instance");

            migrationBuilder.CreateIndex(
                name: "IX_course_academic_org",
                table: "course",
                column: "academic_org");

            migrationBuilder.CreateIndex(
                name: "IX_course_field_of_education",
                table: "course",
                column: "field_of_education");

            migrationBuilder.CreateIndex(
                name: "IX_course_instance_campus",
                table: "course_instance",
                column: "campus");

            migrationBuilder.CreateIndex(
                name: "IX_course_instance_course",
                table: "course_instance",
                column: "course");

            migrationBuilder.CreateIndex(
                name: "IX_course_list_program",
                table: "course_list",
                column: "program");

            migrationBuilder.CreateIndex(
                name: "IX_course_list_courses_join_course_list",
                table: "course_list_courses_join",
                column: "course_list");

            migrationBuilder.CreateIndex(
                name: "IX_course_list_courses_join_course_course_list",
                table: "course_list_courses_join",
                columns: new[] { "course", "course_list" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_document_assessment",
                table: "document",
                column: "assessment");

            migrationBuilder.CreateIndex(
                name: "IX_document_course",
                table: "document",
                column: "course");

            migrationBuilder.CreateIndex(
                name: "IX_document_course_instance",
                table: "document",
                column: "course_instance");

            migrationBuilder.CreateIndex(
                name: "IX_document_feedback",
                table: "document",
                column: "feedback");

            migrationBuilder.CreateIndex(
                name: "IX_document_program",
                table: "document",
                column: "program");

            migrationBuilder.CreateIndex(
                name: "IX_field_of_education_parent_field_of_education",
                table: "field_of_education",
                column: "parent_field_of_education");

            migrationBuilder.CreateIndex(
                name: "IX_learning_outcome_course_instance",
                table: "learning_outcome",
                column: "course_instance");

            migrationBuilder.CreateIndex(
                name: "IX_learning_outcome_program",
                table: "learning_outcome",
                column: "program");

            migrationBuilder.CreateIndex(
                name: "IX_learning_outcome_assessments_join_learning_outcome",
                table: "learning_outcome_assessments_join",
                column: "learning_outcome");

            migrationBuilder.CreateIndex(
                name: "IX_learning_outcome_assessments_join_assessment_learning_outco~",
                table: "learning_outcome_assessments_join",
                columns: new[] { "assessment", "learning_outcome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_level_level_category",
                table: "level",
                column: "level_category");

            migrationBuilder.CreateIndex(
                name: "IX_level_assessments_join_level",
                table: "level_assessments_join",
                column: "level");

            migrationBuilder.CreateIndex(
                name: "IX_level_assessments_join_node",
                table: "level_assessments_join",
                column: "node");

            migrationBuilder.CreateIndex(
                name: "IX_level_assessments_join_assessment_level_node",
                table: "level_assessments_join",
                columns: new[] { "assessment", "level", "node" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_level_category_nodes_join_node",
                table: "level_category_nodes_join",
                column: "node");

            migrationBuilder.CreateIndex(
                name: "IX_level_category_nodes_join_level_category_node",
                table: "level_category_nodes_join",
                columns: new[] { "level_category", "node" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_level_courses_join_level",
                table: "level_courses_join",
                column: "level");

            migrationBuilder.CreateIndex(
                name: "IX_level_courses_join_node",
                table: "level_courses_join",
                column: "node");

            migrationBuilder.CreateIndex(
                name: "IX_level_courses_join_course_level_node",
                table: "level_courses_join",
                columns: new[] { "course", "level", "node" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_level_programs_join_level",
                table: "level_programs_join",
                column: "level");

            migrationBuilder.CreateIndex(
                name: "IX_level_programs_join_node",
                table: "level_programs_join",
                column: "node");

            migrationBuilder.CreateIndex(
                name: "IX_level_programs_join_program_level_node",
                table: "level_programs_join",
                columns: new[] { "program", "level", "node" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_node_framework",
                table: "node",
                column: "framework");

            migrationBuilder.CreateIndex(
                name: "IX_node_parent_node",
                table: "node",
                column: "parent_node");

            migrationBuilder.CreateIndex(
                name: "IX_node_assessments_join_node",
                table: "node_assessments_join",
                column: "node");

            migrationBuilder.CreateIndex(
                name: "IX_node_assessments_join_assessment_node",
                table: "node_assessments_join",
                columns: new[] { "assessment", "node" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_node_courses_join_node",
                table: "node_courses_join",
                column: "node");

            migrationBuilder.CreateIndex(
                name: "IX_node_courses_join_course_node",
                table: "node_courses_join",
                columns: new[] { "course", "node" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_node_programs_join_node",
                table: "node_programs_join",
                column: "node");

            migrationBuilder.CreateIndex(
                name: "IX_node_programs_join_program_node",
                table: "node_programs_join",
                columns: new[] { "program", "node" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_program_campus",
                table: "program",
                column: "campus");

            migrationBuilder.CreateIndex(
                name: "IX_program_field_of_education",
                table: "program",
                column: "field_of_education");

            migrationBuilder.CreateIndex(
                name: "IX_program_program_career",
                table: "program",
                column: "program_career");

            migrationBuilder.CreateIndex(
                name: "IX_project_framework",
                table: "project",
                column: "framework");

            migrationBuilder.CreateIndex(
                name: "IX_project_programs_join_project",
                table: "project_programs_join",
                column: "project");

            migrationBuilder.CreateIndex(
                name: "IX_project_programs_join_program_project",
                table: "project_programs_join",
                columns: new[] { "program", "project" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_user_groups_join_project",
                table: "project_user_groups_join",
                column: "project");

            migrationBuilder.CreateIndex(
                name: "IX_project_user_groups_join_user_group_project",
                table: "project_user_groups_join",
                columns: new[] { "user_group", "project" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_login",
                table: "user",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_group_users_join_user_group",
                table: "user_group_users_join",
                column: "user_group");

            migrationBuilder.CreateIndex(
                name: "IX_user_group_users_join_user_user_group",
                table: "user_group_users_join",
                columns: new[] { "user", "user_group" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_list_courses_join");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "learning_outcome_assessments_join");

            migrationBuilder.DropTable(
                name: "level_assessments_join");

            migrationBuilder.DropTable(
                name: "level_category_nodes_join");

            migrationBuilder.DropTable(
                name: "level_courses_join");

            migrationBuilder.DropTable(
                name: "level_programs_join");

            migrationBuilder.DropTable(
                name: "node_assessments_join");

            migrationBuilder.DropTable(
                name: "node_courses_join");

            migrationBuilder.DropTable(
                name: "node_programs_join");

            migrationBuilder.DropTable(
                name: "project_programs_join");

            migrationBuilder.DropTable(
                name: "project_user_groups_join");

            migrationBuilder.DropTable(
                name: "user_group_users_join");

            migrationBuilder.DropTable(
                name: "course_list");

            migrationBuilder.DropTable(
                name: "learning_outcome");

            migrationBuilder.DropTable(
                name: "level");

            migrationBuilder.DropTable(
                name: "assessment");

            migrationBuilder.DropTable(
                name: "node");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "user_group");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "program");

            migrationBuilder.DropTable(
                name: "level_category");

            migrationBuilder.DropTable(
                name: "assessment_type");

            migrationBuilder.DropTable(
                name: "course_instance");

            migrationBuilder.DropTable(
                name: "framework");

            migrationBuilder.DropTable(
                name: "program_career");

            migrationBuilder.DropTable(
                name: "campus");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "academic_org");

            migrationBuilder.DropTable(
                name: "field_of_education");
        }
    }
}
