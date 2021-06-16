using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class changeIdRequestToRequestId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType");

            migrationBuilder.DropColumn(
                name: "IdRequest",
                table: "tb_m_requestType");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "tb_m_requestType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType",
                columns: new[] { "RequestId", "TipeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "tb_m_requestType");

            migrationBuilder.AddColumn<int>(
                name: "IdRequest",
                table: "tb_m_requestType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType",
                columns: new[] { "IdRequest", "TipeId" });
        }
    }
}
