using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.Migrations
{
    public partial class MoreResumeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobExperience");

            migrationBuilder.DropColumn(
                name: "College",
                table: "JobResume");

            migrationBuilder.DropColumn(
                name: "GraduationDate",
                table: "JobResume");

            migrationBuilder.DropColumn(
                name: "HighSchool",
                table: "JobResume");

            migrationBuilder.DropColumn(
                name: "Intermediate",
                table: "JobResume");

            migrationBuilder.CreateTable(
                name: "AcademicDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Institute = table.Column<string>(type: "text", nullable: true),
                    University = table.Column<string>(type: "text", nullable: true),
                    Specialization = table.Column<string>(type: "text", nullable: true),
                    Marks = table.Column<float>(type: "real", nullable: false),
                    MarksType = table.Column<int>(type: "integer", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    JobResumeId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicDetail_JobResume_JobResumeId",
                        column: x => x.JobResumeId,
                        principalTable: "JobResume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    JobResumeId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_JobResume_JobResumeId",
                        column: x => x.JobResumeId,
                        principalTable: "JobResume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    Contribution = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    JobResumeId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_JobResume_JobResumeId",
                        column: x => x.JobResumeId,
                        principalTable: "JobResume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    JobResumeId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_JobResume_JobResumeId",
                        column: x => x.JobResumeId,
                        principalTable: "JobResume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicDetail_JobResumeId",
                table: "AcademicDetail",
                column: "JobResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_JobResumeId",
                table: "Experience",
                column: "JobResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_JobResumeId",
                table: "Project",
                column: "JobResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_JobResumeId",
                table: "Skill",
                column: "JobResumeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicDetail");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.AddColumn<string>(
                name: "College",
                table: "JobResume",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GraduationDate",
                table: "JobResume",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "HighSchool",
                table: "JobResume",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Intermediate",
                table: "JobResume",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobExperience",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    JobName = table.Column<string>(type: "text", nullable: true),
                    JobResumeId = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobExperience_JobResume_JobResumeId",
                        column: x => x.JobResumeId,
                        principalTable: "JobResume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobExperience_JobResumeId",
                table: "JobExperience",
                column: "JobResumeId");
        }
    }
}
