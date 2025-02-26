using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.Order_Aggregate
{
    public class OrderItem : BaseModel
    {
        public OrderItem()
        {
        }
        public OrderItem(ProductItemOrderd productItemOrdered, decimal price, int quantity)
        {
            ProductItemOrdered = productItemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrderd ProductItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
       
    }
}
