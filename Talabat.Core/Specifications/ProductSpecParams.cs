using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
        public string? Sort { set; get; } 
        public int? BrandId { set; get; }
        public int? TypeId { set; get; }
        public int Count { set; get; }

        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int PageIndex { set; get; } = 1;

        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

    }
}
