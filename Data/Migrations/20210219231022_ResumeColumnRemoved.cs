using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.Data.Migrations
{
    public partial class ResumeColumnRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobResume_AspNetUsers_UserId",
                table: "JobResume");

            migrationBuilder.DropIndex(
                name: "IX_JobResume_UserId",
                table: "JobResume");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "JobResume",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ResumeId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ResumeId",
                table: "AspNetUsers",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JobResume_ResumeId",
                table: "AspNetUsers",
                column: "ResumeId",
                principalTable: "JobResume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JobResume_ResumeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ResumeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "JobResume",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobResume_UserId",
                table: "JobResume",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobResume_AspNetUsers_UserId",
                table: "JobResume",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
