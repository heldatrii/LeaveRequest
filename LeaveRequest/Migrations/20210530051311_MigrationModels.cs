using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaveRequest.Migrations
{
    public partial class MigrationModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_person",
                columns: table => new
                {
                    NIK = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDepartement = table.Column<int>(nullable: false),
                    ManagerId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_person", x => x.NIK);
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
                name: "tb_m_department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_m_department_tb_m_person_Id",
                        column: x => x.Id,
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
                    IdPerson = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequestS = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    PersonNIK = table.Column<int>(nullable: true),
                    RequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_requestStatus", x => x.IdPerson);
                    table.ForeignKey(
                        name: "FK_tb_m_requestStatus_tb_m_person_PersonNIK",
                        column: x => x.PersonNIK,
                        principalTable: "tb_m_person",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_requestStatus_tb_m_request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "tb_m_request",
                        principalColumn: "Id",
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
                name: "tb_m_roleaccount",
                columns: table => new
                {
                    IdAccount = table.Column<int>(nullable: false),
                    IdRole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roleaccount", x => new { x.IdAccount, x.IdRole });
                    table.ForeignKey(
                        name: "FK_tb_m_roleaccount_tb_m_account_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "tb_m_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_roleaccount_tb_m_role_IdRole",
                        column: x => x.IdRole,
                        principalTable: "tb_m_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_requestStatus_PersonNIK",
                table: "tb_m_requestStatus",
                column: "PersonNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_requestStatus_RequestId",
                table: "tb_m_requestStatus",
                column: "RequestId");

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
                name: "tb_m_department");

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
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_m_person");
        }
    }
}
