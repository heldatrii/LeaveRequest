using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class deleteDuplicateDepartementId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_person_tb_m_department_DepartementId",
                table: "tb_m_person");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_person_DepartementId",
                table: "tb_m_person");

            migrationBuilder.DropColumn(
                name: "DepartementId",
                table: "tb_m_person");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_person_IdDepartement",
                table: "tb_m_person",
                column: "IdDepartement");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_person_tb_m_department_IdDepartement",
                table: "tb_m_person",
                column: "IdDepartement",
                principalTable: "tb_m_department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_person_tb_m_department_IdDepartement",
                table: "tb_m_person");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_person_IdDepartement",
                table: "tb_m_person");

            migrationBuilder.AddColumn<int>(
                name: "DepartementId",
                table: "tb_m_person",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_person_DepartementId",
                table: "tb_m_person",
                column: "DepartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_person_tb_m_department_DepartementId",
                table: "tb_m_person",
                column: "DepartementId",
                principalTable: "tb_m_department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
