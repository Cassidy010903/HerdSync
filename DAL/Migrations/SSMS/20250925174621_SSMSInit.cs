using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.SSMS
{
    /// <inheritdoc />
    public partial class SSMSInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PregnanciesHistory",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    prg_Pregnancy_Spot_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prg_Pregnancy_End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prg_Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    spd_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PregnanciesHistory", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    spd_Number = table.Column<int>(type: "int", nullable: false),
                    spd_Tag_Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spd_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    spd_Age = table.Column<int>(type: "int", nullable: false),
                    spd_AgeGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    spd_Mother = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    spd_Father = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    spd_Est_Years_Left = table.Column<int>(type: "int", nullable: true),
                    spd_Medical_Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    spd_Last_Pregnancy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    spd_Total_Pregnancies = table.Column<int>(type: "int", nullable: true),
                    spd_Total_Offspring = table.Column<int>(type: "int", nullable: true),
                    spd_Branded = table.Column<bool>(type: "bit", nullable: false),
                    spd_Species = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    spd_Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    prg_Pregnancy_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    spd_Born_Or_Buy = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeciesHistory",
                columns: table => new
                {
                    HistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    spd_Number = table.Column<int>(type: "int", nullable: false),
                    spd_Tag_Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spd_Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    spd_Age = table.Column<int>(type: "int", nullable: false),
                    spd_AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spd_Mother = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spd_Father = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spd_Est_Years_Left = table.Column<int>(type: "int", nullable: true),
                    spd_Medical_Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spd_Last_Pregnancy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    spd_Total_Pregnancies = table.Column<int>(type: "int", nullable: true),
                    spd_Total_Offspring = table.Column<int>(type: "int", nullable: true),
                    spd_Branded = table.Column<bool>(type: "bit", nullable: false),
                    spd_Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spd_Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prg_Pregnancy_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    spd_Born_Or_Buy = table.Column<bool>(type: "bit", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciesHistory", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "Pregnancies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    prg_Pregnancy_Spot_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prg_Pregnancy_End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prg_Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    spd_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregnancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pregnancies_Species_spd_Id",
                        column: x => x.spd_Id,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pregnancies_spd_Id",
                table: "Pregnancies",
                column: "spd_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pregnancies");

            migrationBuilder.DropTable(
                name: "PregnanciesHistory");

            migrationBuilder.DropTable(
                name: "SpeciesHistory");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
