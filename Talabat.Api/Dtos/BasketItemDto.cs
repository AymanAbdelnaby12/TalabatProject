using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string PictureUrl { set; get; }
        [Required]
        public string Brand { set; get; }
        [Required]
        public string Type { set; get; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be at least 0.1")]
        public decimal Price { set; get; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { set; get; }

    }
}