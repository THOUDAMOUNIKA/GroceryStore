using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using GroceryStoreAPI.DTOs;
using System.Collections.Generic;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly RazorpayClient _razorpayClient;

        public PaymentController(IConfiguration configuration)
        {
            var keyId = configuration["Razorpay:KeyId"];
            var keySecret = configuration["Razorpay:KeySecret"];
            _razorpayClient = new RazorpayClient(keyId, keySecret);
        }

        [HttpPost("create-order")]
        public ActionResult CreateRazorpayOrder(CreatePaymentDto paymentDto)
        {
            var options = new Dictionary<string, object>
            {
                { "amount", (int)(paymentDto.Amount * 100) }, // Convert to paise
                { "currency", "INR" },
                { "receipt", $"order_{paymentDto.OrderId}" },
                { "notes", new Dictionary<string, string> { { "order_id", paymentDto.OrderId.ToString() } } }
            };

            var order = _razorpayClient.Order.Create(options);
            
            return Ok(new 
            { 
                OrderId = order["id"].ToString(),
                Amount = order["amount"],
                Currency = order["currency"]
            });
        }

        [HttpPost("verify-payment")]
        public ActionResult VerifyPayment(VerifyPaymentDto verifyDto)
        {
            var attributes = new Dictionary<string, string>
            {
                { "razorpay_order_id", verifyDto.OrderId },
                { "razorpay_payment_id", verifyDto.PaymentId },
                { "razorpay_signature", verifyDto.Signature }
            };

            try
            {
                Utils.verifyPaymentSignature(attributes);
                return Ok(new { Status = "Payment verified successfully" });
            }
            catch
            {
                return BadRequest(new { Status = "Payment verification failed" });
            }
        }
    }
}