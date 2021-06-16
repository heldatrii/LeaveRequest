using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class AddFKInRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_request_tb_m_person_PersonNIK",
                table: "tb_m_request");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_request_PersonNIK",
                table: "tb_m_request");

            migrationBuilder.DropColumn(
                name: "PersonNIK",
                table: "tb_m_request");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_m_request",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_request_NIK",
                table: "tb_m_request",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_request_tb_m_person_NIK",
                table: "tb_m_request",
                column: "NIK",
                principalTable: "tb_m_person",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_request_tb_m_person_NIK",
                table: "tb_m_request");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_request_NIK",
                table: "tb_m_request");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_m_request",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonNIK",
                table: "tb_m_request",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_request_PersonNIK",
                table: "tb_m_request",
                column: "PersonNIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_request_tb_m_person_PersonNIK",
                table: "tb_m_request",
                column: "PersonNIK",
                principalTable: "tb_m_person",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
