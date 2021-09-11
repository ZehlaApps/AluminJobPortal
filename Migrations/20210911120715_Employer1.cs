using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.Migrations
{
    public partial class Employer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobListings_AspNetUsers_EmployerId",
                table: "JobListings");

            migrationBuilder.DropIndex(
                name: "IX_JobListings_EmployerId",
                table: "JobListings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobListings_EmployerId",
                table: "JobListings",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobListings_AspNetUsers_EmployerId",
                table: "JobListings",
                column: "EmployerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
