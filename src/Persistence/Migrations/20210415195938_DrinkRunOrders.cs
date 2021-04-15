using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class DrinkRunOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrinkOrderDbModelDrinkRunDbModel",
                columns: table => new
                {
                    DrinkOrdersId = table.Column<Guid>(type: "uuid", nullable: false),
                    DrinkRunsDrinkMakerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkOrderDbModelDrinkRunDbModel", x => new { x.DrinkOrdersId, x.DrinkRunsDrinkMakerId });
                    table.ForeignKey(
                        name: "FK_DrinkOrderDbModelDrinkRunDbModel_DrinkOrder_DrinkOrdersId",
                        column: x => x.DrinkOrdersId,
                        principalTable: "DrinkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkOrderDbModelDrinkRunDbModel_DrinkRun_DrinkRunsDrinkMak~",
                        column: x => x.DrinkRunsDrinkMakerId,
                        principalTable: "DrinkRun",
                        principalColumn: "DrinkMakerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkOrderDbModelDrinkRunDbModel_DrinkRunsDrinkMakerId",
                table: "DrinkOrderDbModelDrinkRunDbModel",
                column: "DrinkRunsDrinkMakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkOrderDbModelDrinkRunDbModel");
        }
    }
}
