using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cooking.Service.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Portions = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsDeleted", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { 1, 2, null, false, null, "Cucumber" },
                    { 2, 2, null, false, null, "Olives" },
                    { 3, 3, null, false, null, "Lettuce" },
                    { 4, 6, null, false, null, "Meat" },
                    { 5, 6, null, false, null, "Tomato" },
                    { 6, 8, null, false, null, "Cheese" },
                    { 7, 10, null, false, null, "Dough" }
                });

            migrationBuilder.InsertData(
                table: "Recipe",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "LastUpdatedAt", "Name", "Portions" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2779), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2792), "Burger", 1 },
                    { 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2851), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2851), "Pie", 1 },
                    { 3, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2868), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2868), "Sandwich", 1 },
                    { 4, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2886), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2887), "Pasta", 2 },
                    { 5, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2954), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2954), "Salad", 3 },
                    { 6, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2990), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2990), "Pizza", 4 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "CreatedAt", "IsDeleted", "LastUpdatedAt", "Name", "RecipeId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2816), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2816), "Meat", 1 },
                    { 2, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2824), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2825), "Lettuce", 1 },
                    { 3, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2831), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2832), "Tomato", 1 },
                    { 4, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2837), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2838), "Cheese", 1 },
                    { 5, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2843), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2844), "Dough", 1 },
                    { 6, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2857), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2857), "Dough", 2 },
                    { 7, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2862), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2863), "Meat", 2 },
                    { 8, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2874), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2874), "Dough", 3 },
                    { 9, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2880), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2881), "Cucumber", 3 },
                    { 10, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2892), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2893), "Dough", 4 },
                    { 11, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2898), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2898), "Tomato", 4 },
                    { 12, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2903), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2904), "Cheese", 4 },
                    { 13, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2946), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2947), "Meat", 4 },
                    { 14, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2960), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2961), "Lettuce", 5 },
                    { 15, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2966), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2967), "Tomato", 5 },
                    { 16, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2972), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2973), "Cucumber", 5 },
                    { 17, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2978), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2978), "Cheese", 5 },
                    { 18, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2984), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2985), "Olives", 5 },
                    { 19, 3, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2996), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(2997), "Dough", 6 },
                    { 20, 2, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(3002), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(3003), "Tomato", 6 },
                    { 21, 3, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(3008), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(3008), "Cheese", 6 },
                    { 22, 1, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(3013), false, new DateTime(2024, 3, 4, 11, 6, 42, 55, DateTimeKind.Local).AddTicks(3014), "Olives", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Recipe");
        }
    }
}
