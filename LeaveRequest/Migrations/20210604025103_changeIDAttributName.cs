using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class changeIDAttributName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestStatus_tb_m_request_IdRequest",
                table: "tb_m_requestStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_IdType",
                table: "tb_m_requestType");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestType_tb_m_tipe_IdType",
                table: "tb_m_requestType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_tipe",
                table: "tb_m_tipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_requestType_IdType",
                table: "tb_m_requestType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_requestStatus",
                table: "tb_m_requestStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_request",
                table: "tb_m_request");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tb_m_tipe");

            migrationBuilder.DropColumn(
                name: "IdType",
                table: "tb_m_requestType");

            migrationBuilder.DropColumn(
                name: "IdRequest",
                table: "tb_m_requestStatus");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "tb_m_request");

            migrationBuilder.AddColumn<int>(
                name: "TipeId",
                table: "tb_m_tipe",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TipeId",
                table: "tb_m_requestType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "tb_m_requestStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "tb_m_request",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_tipe",
                table: "tb_m_tipe",
                column: "TipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType",
                columns: new[] { "IdRequest", "TipeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_requestStatus",
                table: "tb_m_requestStatus",
                columns: new[] { "RequestId", "NIK" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_request",
                table: "tb_m_request",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_requestType_TipeId",
                table: "tb_m_requestType",
                column: "TipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestStatus_tb_m_request_RequestId",
                table: "tb_m_requestStatus",
                column: "RequestId",
                principalTable: "tb_m_request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_TipeId",
                table: "tb_m_requestType",
                column: "TipeId",
                principalTable: "tb_m_request",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestType_tb_m_tipe_TipeId",
                table: "tb_m_requestType",
                column: "TipeId",
                principalTable: "tb_m_tipe",
                principalColumn: "TipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestStatus_tb_m_request_RequestId",
                table: "tb_m_requestStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_TipeId",
                table: "tb_m_requestType");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_requestType_tb_m_tipe_TipeId",
                table: "tb_m_requestType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_tipe",
                table: "tb_m_tipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_requestType_TipeId",
                table: "tb_m_requestType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_requestStatus",
                table: "tb_m_requestStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_request",
                table: "tb_m_request");

            migrationBuilder.DropColumn(
                name: "TipeId",
                table: "tb_m_tipe");

            migrationBuilder.DropColumn(
                name: "TipeId",
                table: "tb_m_requestType");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "tb_m_requestStatus");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "tb_m_request");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tb_m_tipe",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IdType",
                table: "tb_m_requestType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdRequest",
                table: "tb_m_requestStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "tb_m_request",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_tipe",
                table: "tb_m_tipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_requestType",
                table: "tb_m_requestType",
                columns: new[] { "IdRequest", "IdType" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_requestStatus",
                table: "tb_m_requestStatus",
                columns: new[] { "IdRequest", "NIK" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_request",
                table: "tb_m_request",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_requestType_IdType",
                table: "tb_m_requestType",
                column: "IdType");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestStatus_tb_m_request_IdRequest",
                table: "tb_m_requestStatus",
                column: "IdRequest",
                principalTable: "tb_m_request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestType_tb_m_request_IdType",
                table: "tb_m_requestType",
                column: "IdType",
                principalTable: "tb_m_request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_requestType_tb_m_tipe_IdType",
                table: "tb_m_requestType",
                column: "IdType",
                principalTable: "tb_m_tipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
