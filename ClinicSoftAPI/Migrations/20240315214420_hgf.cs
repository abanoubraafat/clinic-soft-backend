using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicSoftAPI.Migrations
{
    /// <inheritdoc />
    public partial class hgf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patient_National_Id",
                table: "Patient",
                column: "National_Id",
                unique: true,
                filter: "[National_Id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patient_National_Id",
                table: "Patient");
        }
    }
}
