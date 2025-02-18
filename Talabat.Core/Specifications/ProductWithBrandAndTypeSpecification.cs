using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification:Specifications<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams Param) 
            :base(p =>
            (String.IsNullOrEmpty(Param.Search) || p.Name.ToLower().Contains(Param.Search))
            &&
            (!Param.BrandId.HasValue || p.ProductBrandId==Param.BrandId)
            &&
            (!Param.TypeId.HasValue || p.ProductTypeId == Param.TypeId)
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!String.IsNullOrEmpty(Param.Sort))
            {
                switch (Param.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            ApplyPagination(Param.PageSize * (Param.PageIndex - 1), Param.PageSize);
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            
        }
    }
}
