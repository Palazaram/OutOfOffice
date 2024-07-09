using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedEmployeeRelationshipV12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                unique: true,
                filter: "[PeoplePartnerId] IS NOT NULL");
        }
    }
}
