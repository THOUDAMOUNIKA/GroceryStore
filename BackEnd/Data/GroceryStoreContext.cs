using Microsoft.EntityFrameworkCore;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Data
{
    public class GroceryStoreContext : DbContext
    {
        public GroceryStoreContext(DbContextOptions<GroceryStoreContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<GroceryItem> GroceryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<GroceryItem>()
                .Property(g => g.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroceryItem>().HasData(
                // Fruits
                new GroceryItem { Id = 1, Name = "Apples", Description = "Fresh red apples", Price = 2.99m, Quantity = 100, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1560806887-1e4cd0b6cbd6?w=300" },
                new GroceryItem { Id = 2, Name = "Bananas", Description = "Ripe yellow bananas", Price = 1.99m, Quantity = 150, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=300" },
                new GroceryItem { Id = 9, Name = "Oranges", Description = "Juicy navel oranges", Price = 3.49m, Quantity = 80, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1547036967-23d11aacaee0?w=300" },
                new GroceryItem { Id = 10, Name = "Grapes", Description = "Sweet green grapes", Price = 4.99m, Quantity = 60, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1537640538966-79f369143f8f?w=300" },
                new GroceryItem { Id = 11, Name = "Strawberries", Description = "Fresh strawberries", Price = 5.99m, Quantity = 40, Category = "Fruits", ImageUrl = "https://images.unsplash.com/photo-1464965911861-746a04b4bca6?w=300" },
                
                // Dairy
                new GroceryItem { Id = 3, Name = "Milk", Description = "Fresh whole milk", Price = 3.49m, Quantity = 50, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1550583724-b2692b85b150?w=300" },
                new GroceryItem { Id = 5, Name = "Eggs", Description = "Farm fresh eggs", Price = 4.99m, Quantity = 80, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1582722872445-44dc5f7e3c8f?w=300" },
                new GroceryItem { Id = 12, Name = "Cheese", Description = "Cheddar cheese block", Price = 6.99m, Quantity = 45, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1486297678162-eb2a19b0a32d?w=300" },
                new GroceryItem { Id = 13, Name = "Yogurt", Description = "Greek vanilla yogurt", Price = 1.99m, Quantity = 70, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1571212515416-fca88c2d2c90?w=300" },
                new GroceryItem { Id = 14, Name = "Butter", Description = "Unsalted butter", Price = 4.49m, Quantity = 35, Category = "Dairy", ImageUrl = "https://images.unsplash.com/photo-1589985270826-4b7bb135bc9d?w=300" },
                
                // Bakery
                new GroceryItem { Id = 4, Name = "Bread", Description = "Whole wheat bread", Price = 2.49m, Quantity = 75, Category = "Bakery", ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=300" },
                new GroceryItem { Id = 15, Name = "Croissants", Description = "Buttery croissants", Price = 3.99m, Quantity = 25, Category = "Bakery", ImageUrl = "https://images.unsplash.com/photo-1555507036-ab794f4afe5e?w=300" },
                new GroceryItem { Id = 16, Name = "Bagels", Description = "Everything bagels", Price = 4.49m, Quantity = 30, Category = "Bakery", ImageUrl = "https://images.unsplash.com/photo-1551024506-0bccd828d307?w=300" },
                new GroceryItem { Id = 17, Name = "Muffins", Description = "Blueberry muffins", Price = 5.99m, Quantity = 20, Category = "Bakery", ImageUrl = "https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=300" },
                new GroceryItem { Id = 18, Name = "Donuts", Description = "Glazed donuts", Price = 6.99m, Quantity = 18, Category = "Bakery", ImageUrl = "https://images.unsplash.com/photo-1551024601-bec78aea704b?w=300" },
                
                // Meat
                new GroceryItem { Id = 6, Name = "Chicken Breast", Description = "Boneless chicken breast", Price = 8.99m, Quantity = 30, Category = "Meat", ImageUrl = "https://images.unsplash.com/photo-1604503468506-a8da13d82791?w=300" },
                new GroceryItem { Id = 19, Name = "Ground Beef", Description = "Lean ground beef", Price = 7.99m, Quantity = 25, Category = "Meat", ImageUrl = "https://images.unsplash.com/photo-1588347818481-c7c1b8b8e8b8?w=300" },
                new GroceryItem { Id = 20, Name = "Salmon", Description = "Fresh Atlantic salmon", Price = 12.99m, Quantity = 15, Category = "Meat", ImageUrl = "https://images.unsplash.com/photo-1544943910-4c1dc44aab44?w=300" },
                new GroceryItem { Id = 21, Name = "Pork Chops", Description = "Bone-in pork chops", Price = 9.99m, Quantity = 20, Category = "Meat", ImageUrl = "https://images.unsplash.com/photo-1602470520998-f4a52199a3d6?w=300" },
                
                // Grains
                new GroceryItem { Id = 7, Name = "Rice", Description = "Basmati rice", Price = 5.99m, Quantity = 60, Category = "Grains", ImageUrl = "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300" },
                new GroceryItem { Id = 22, Name = "Pasta", Description = "Spaghetti pasta", Price = 2.99m, Quantity = 85, Category = "Grains", ImageUrl = "https://images.unsplash.com/photo-1551892374-ecf8754cf8b0?w=300" },
                new GroceryItem { Id = 23, Name = "Quinoa", Description = "Organic quinoa", Price = 7.99m, Quantity = 40, Category = "Grains", ImageUrl = "https://images.unsplash.com/photo-1586201375761-83865001e31c?w=300" },
                new GroceryItem { Id = 24, Name = "Oats", Description = "Rolled oats", Price = 3.99m, Quantity = 55, Category = "Grains", ImageUrl = "https://images.unsplash.com/photo-1574323347407-f5e1ad6d020b?w=300" },
                
                // Vegetables
                new GroceryItem { Id = 8, Name = "Tomatoes", Description = "Fresh tomatoes", Price = 3.99m, Quantity = 90, Category = "Vegetables", ImageUrl = "https://images.unsplash.com/photo-1592924357228-91a4daadcfea?w=300" },
                new GroceryItem { Id = 25, Name = "Carrots", Description = "Fresh carrots", Price = 2.49m, Quantity = 75, Category = "Vegetables", ImageUrl = "https://images.unsplash.com/photo-1598170845058-32b9d6a5da37?w=300" },
                new GroceryItem { Id = 26, Name = "Broccoli", Description = "Fresh broccoli", Price = 3.49m, Quantity = 50, Category = "Vegetables", ImageUrl = "https://images.unsplash.com/photo-1459411621453-7b03977f4bfc?w=300" },
                new GroceryItem { Id = 27, Name = "Spinach", Description = "Baby spinach leaves", Price = 2.99m, Quantity = 65, Category = "Vegetables", ImageUrl = "https://images.unsplash.com/photo-1576045057995-568f588f82fb?w=300" },
                new GroceryItem { Id = 28, Name = "Bell Peppers", Description = "Mixed bell peppers", Price = 4.99m, Quantity = 45, Category = "Vegetables", ImageUrl = "https://images.unsplash.com/photo-1563565375-f3fdfdbefa83?w=300" }
            );
        }
    }
}