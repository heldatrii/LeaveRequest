using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_request", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_tipe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTipe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_tipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_person",
                columns: table => new
                {
                    NIK = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDepartement = table.Column<int>(nullable: false),
                    ManagerId = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    DepartementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_person", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_person_tb_m_department_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "tb_m_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_person_tb_m_person_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "tb_m_person",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_requestType",
                columns: table => new
                {
                    IdRequest = table.Column<int>(nullable: false),
                    IdType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_requestType", x => new { x.IdRequest, x.IdType });
                    table.ForeignKey(
                        name: "FK_tb_m_requestType_tb_m_request_IdType",
                        column: x => x.IdType,
                        principalTable: "tb_m_request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_requestType_tb_m_tipe_IdType",
                        column: x => x.IdType,
                        principalTable: "tb_m_tipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    NIK = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_person_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_person",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_leaveAllowance",
                columns: table => new
                {
                    NIK = table.Column<int>(nullable: false),
                    LeaveAllow = table.Column<int>(nullable: false),
                    UsedLeaveAllow = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_leaveAllowance", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_leaveAllowance_tb_m_person_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_person",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_requestStatus",
                columns: table => new
                {
                    NIK = table.Column<int>(nullable: false),
                    IdRequest = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_requestStatus", x => new { x.IdRequest, x.NIK });
                    table.ForeignKey(
                        name: "FK_tb_m_requestStatus_tb_m_request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "tb_m_request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_requestStatus_tb_m_person_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_person",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roleaccount",
                columns: table => new
                {
                    NIK = table.Column<int>(nullable: false),
                    IdRole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roleaccount", x => new { x.NIK, x.IdRole });
                    table.ForeignKey(
                        name: "FK_tb_m_roleaccount_tb_m_role_IdRole",
                        column: x => x.IdRole,
                        principalTable: "tb_m_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_roleaccount_tb_m_account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_person_DepartementId",
                table: "tb_m_person",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_person_ManagerId",
                table: "tb_m_person",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_requestStatus_NIK",
                table: "tb_m_requestStatus",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_requestType_IdType",
                table: "tb_m_requestType",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_roleaccount_IdRole",
                table: "tb_m_roleaccount",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_leaveAllowance");

            migrationBuilder.DropTable(
                name: "tb_m_requestStatus");

            migrationBuilder.DropTable(
                name: "tb_m_requestType");

            migrationBuilder.DropTable(
                name: "tb_m_roleaccount");

            migrationBuilder.DropTable(
                name: "tb_m_request");

            migrationBuilder.DropTable(
                name: "tb_m_tipe");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_person");

            migrationBuilder.DropTable(
                name: "tb_m_department");
        }
    }
}
