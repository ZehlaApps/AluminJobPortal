using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.Data.Migrations
{
    public partial class JobRelationUpdated10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_JobListingId",
                table: "JobApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListings_JobListingId",
                table: "JobApplications",
                column: "JobListingId",
                principalTable: "JobListings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobListings_JobListingId",
                table: "JobApplications");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobListings_JobListingId",
                table: "JobApplications",
                column: "JobListingId",
                principalTable: "JobListings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
