using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace taxcalculator.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Progressives",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    ValueFrom = table.Column<double>(type: "float", nullable: false),
                    ValueTo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressives", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaxTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalcType = table.Column<int>(type: "int", nullable: false),
                    DefaultRate = table.Column<double>(type: "float", nullable: false),
                    DefaultValue = table.Column<double>(type: "float", nullable: false),
                    MaxEarns = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Calcs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InValue = table.Column<double>(type: "float", nullable: false),
                    OutValue = table.Column<double>(type: "float", nullable: false),
                    calculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calcs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Calcs_TaxTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TaxTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Progressives",
                columns: new[] { "ID", "Rate", "ValueFrom", "ValueTo" },
                values: new object[,]
                {
                    { new Guid("6a5d39be-bd14-4e9c-bd9b-d9873d982eb2"), 10.0, 0.0, 8350.0 },
                    { new Guid("2044cb23-1c10-4b41-a324-ae43efbf7965"), 15.0, 8351.0, 33950.0 },
                    { new Guid("ac2b6f59-aff4-4ab9-9d54-bb270646a268"), 25.0, 33951.0, 82250.0 },
                    { new Guid("8fff718a-6893-4d79-98d3-f898b54464f5"), 28.0, 82251.0, 171550.0 },
                    { new Guid("accec7ac-6dbc-4e66-884d-2a2a1bf1a618"), 33.0, 171551.0, 372950.0 },
                    { new Guid("6f8cec65-8720-4673-82bb-6ed79e866283"), 35.0, 372951.0, 1.7976931348623157E+308 }
                });

            migrationBuilder.InsertData(
                table: "TaxTypes",
                columns: new[] { "ID", "CalcType", "DefaultRate", "DefaultValue", "MaxEarns", "PostalCode" },
                values: new object[,]
                {
                    { new Guid("e2a1a8c9-98dc-48cc-aaaf-8bd9c1c196f3"), 0, 0.0, 0.0, 0.0, "7441" },
                    { new Guid("2f62f38a-cd04-48bc-b447-2870a216d6db"), 1, 5.0, 10000.0, 200000.0, "A100" },
                    { new Guid("3ed63200-0c9b-432a-b55c-01af3f079c83"), 2, 17.5, 0.0, 0.0, "7000" },
                    { new Guid("ce5271af-1cf9-42fc-89c3-82c8adc76bfa"), 0, 0.0, 0.0, 0.0, "1000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calcs_TypeId",
                table: "Calcs",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calcs");

            migrationBuilder.DropTable(
                name: "Progressives");

            migrationBuilder.DropTable(
                name: "TaxTypes");
        }
    }
}
