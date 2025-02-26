using Talabat.Core.Models.Identity;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Api.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } 
        public Core.Models.Order_Aggregate.Address ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }

    }
}
