namespace GroceryStoreAPI.DTOs
{
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public int OrderId { get; set; }
    }

    public class VerifyPaymentDto
    {
        public string OrderId { get; set; } = string.Empty;
        public string PaymentId { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;
    }
}