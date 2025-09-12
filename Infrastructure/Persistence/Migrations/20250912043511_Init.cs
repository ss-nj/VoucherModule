using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Masters_Persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsidiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DebitAmount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreditAmount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    IsLastLevel = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    ParentSubsidiaryId = table.Column<int>(type: "int", nullable: true),
                    ParentPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsidiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_Persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_Subsidiaries_ParentSubsidiaryId",
                        column: x => x.ParentSubsidiaryId,
                        principalTable: "Subsidiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Masters_Code",
                table: "Masters",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Masters_OwnerId",
                table: "Masters",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_NationalId",
                table: "Persons",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PhoneNumber",
                table: "Persons",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_Code",
                table: "Subsidiaries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_MasterId",
                table: "Subsidiaries",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_OwnerId",
                table: "Subsidiaries",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_ParentSubsidiaryId",
                table: "Subsidiaries",
                column: "ParentSubsidiaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subsidiaries");

            migrationBuilder.DropTable(
                name: "Masters");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
