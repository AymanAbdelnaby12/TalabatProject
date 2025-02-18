using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models
{
    public class BasketItem
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string PictureUrl { set; get; }
        public string Brand { set; get; }
        public string Type { set; get; }
        public decimal Price { set; get; }
        public int Quantity { set; get; }
    }
}
