using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationshipManyToManyBetweenEmpProj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeEntityProjectEntity",
                columns: table => new
                {
                    AssignedProjectsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEntityProjectEntity", x => new { x.AssignedProjectsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_EmployeeEntityProjectEntity_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEntityProjectEntity_Projects_AssignedProjectsId",
                        column: x => x.AssignedProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEntityProjectEntity_EmployeesId",
                table: "EmployeeEntityProjectEntity",
                column: "EmployeesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEntityProjectEntity");
        }
    }
}
