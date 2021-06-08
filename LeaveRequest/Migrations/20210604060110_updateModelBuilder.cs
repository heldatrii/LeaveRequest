using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class updateModelBuilder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_TipeId",
                table: "tb_m_requestType");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_RequestId",
                table: "tb_m_requestType",
                column: "RequestId",
                principalTable: "tb_m_request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_RequestId",
                table: "tb_m_requestType");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_TipeId",
                table: "tb_m_requestType",
                column: "TipeId",
                principalTable: "tb_m_request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
