using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GroceryStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroceryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverySlot = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    GroceryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_GroceryItems_GroceryItemId",
                        column: x => x.GroceryItemId,
                        principalTable: "GroceryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GroceryItems",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Fruits", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3628), "Fresh red apples", "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?w=300", true, "Apples", 2.99m, 100 },
                    { 2, "Fruits", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3637), "Ripe yellow bananas", "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=300", true, "Bananas", 1.99m, 150 },
                    { 3, "Dairy", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3649), "Fresh whole milk", "https://images.unsplash.com/photo-1550583724-b2692b85b150?w=300", true, "Milk", 3.49m, 50 },
                    { 4, "Bakery", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3723), "Whole wheat bread", "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=300", true, "Bread", 2.49m, 75 },
                    { 5, "Dairy", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3652), "Farm fresh eggs", "https://images.unsplash.com/photo-1582722872445-44dc5f7e3c8f?w=300", true, "Eggs", 4.99m, 80 },
                    { 6, "Meat", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3734), "Boneless chicken breast", "https://images.unsplash.com/photo-1604503468506-a8da13d82791?w=300", true, "Chicken Breast", 8.99m, 30 },
                    { 7, "Grains", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3743), "Basmati rice", "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300", true, "Rice", 5.99m, 60 },
                    { 8, "Vegetables", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3751), "Fresh tomatoes", "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=300", true, "Tomatoes", 3.99m, 90 },
                    { 9, "Fruits", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3640), "Juicy navel oranges", "https://images.unsplash.com/photo-1547036967-23d11aacaee0?w=300", true, "Oranges", 3.49m, 80 },
                    { 10, "Fruits", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3645), "Sweet green grapes", "https://images.unsplash.com/photo-1537640538966-79f369143f8f?w=300", true, "Grapes", 4.99m, 60 },
                    { 11, "Fruits", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3647), "Fresh strawberries", "https://images.unsplash.com/photo-1464965911861-746a04b4bca6?w=300", true, "Strawberries", 5.99m, 40 },
                    { 12, "Dairy", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3654), "Cheddar cheese block", "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=300", true, "Cheese", 6.99m, 45 },
                    { 13, "Dairy", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3718), "Greek vanilla yogurt", "https://images.unsplash.com/photo-1571212515416-fca88c2d2c90?w=300", true, "Yogurt", 1.99m, 70 },
                    { 14, "Dairy", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3721), "Unsalted butter", "https://images.unsplash.com/photo-1589985270826-4b7bb135bc9d?w=300", true, "Butter", 4.49m, 35 },
                    { 15, "Bakery", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3725), "Buttery croissants", "https://images.unsplash.com/photo-1555507036-ab794f4afe5e?w=300", true, "Croissants", 3.99m, 25 },
                    { 16, "Bakery", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3728), "Everything bagels", "https://images.unsplash.com/photo-1551024506-0bccd828d307?w=300", true, "Bagels", 4.49m, 30 },
                    { 17, "Bakery", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3730), "Blueberry muffins", "https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=300", true, "Muffins", 5.99m, 20 },
                    { 18, "Bakery", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3732), "Glazed donuts", "https://images.unsplash.com/photo-1551024601-bec78aea704b?w=300", true, "Donuts", 6.99m, 18 },
                    { 19, "Meat", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3736), "Lean ground beef", "https://images.unsplash.com/photo-1588347818481-c7c1b8b8e8b8?w=300", true, "Ground Beef", 7.99m, 25 },
                    { 20, "Meat", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3739), "Fresh Atlantic salmon", "https://images.unsplash.com/photo-1544943910-4c1dc44aab44?w=300", true, "Salmon", 12.99m, 15 },
                    { 21, "Meat", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3741), "Bone-in pork chops", "https://images.unsplash.com/photo-1602470520998-f4a52199a3d6?w=300", true, "Pork Chops", 9.99m, 20 },
                    { 22, "Grains", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3745), "Spaghetti pasta", "https://images.unsplash.com/photo-1551892374-ecf8754cf8b0?w=300", true, "Pasta", 2.99m, 85 },
                    { 23, "Grains", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3747), "Organic quinoa", "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300", true, "Quinoa", 7.99m, 40 },
                    { 24, "Grains", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3749), "Rolled oats", "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=300", true, "Oats", 3.99m, 55 },
                    { 25, "Vegetables", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3753), "Fresh carrots", "https://images.unsplash.com/photo-1598170845058-32b9d6a5da37?w=300", true, "Carrots", 2.49m, 75 },
                    { 26, "Vegetables", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3755), "Fresh broccoli", "https://images.unsplash.com/photo-1459411621453-7b03977f4bfc?w=300", true, "Broccoli", 3.49m, 50 },
                    { 27, "Vegetables", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3757), "Baby spinach leaves", "https://images.unsplash.com/photo-1576045057995-568f588f82fb?w=300", true, "Spinach", 2.99m, 65 },
                    { 28, "Vegetables", new DateTime(2025, 10, 18, 16, 11, 6, 130, DateTimeKind.Utc).AddTicks(3759), "Mixed bell peppers", "https://images.unsplash.com/photo-1563565375-f3fdfdbefa83?w=300", true, "Bell Peppers", 4.99m, 45 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GroceryItemId",
                table: "OrderItems",
                column: "GroceryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "GroceryItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
