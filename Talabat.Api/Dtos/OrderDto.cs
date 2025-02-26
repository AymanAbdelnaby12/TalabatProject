using System.ComponentModel.DataAnnotations;
using Talabat.Api.Error;
using Talabat.Core.Models.Identity;

namespace Talabat.Api.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }
    }
}
