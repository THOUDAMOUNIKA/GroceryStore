namespace GroceryStoreAPI.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}