using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T>where T : BaseModel
    {
        // _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();

        //sign for properties for where condition [where(p => p.ProductBrand)]
        public Expression<Func<T, bool>> Criteria { set; get; } //this is a signuture of property   

        //sign for list of include properties [Include(p => p.ProductBrand).Include(p=>p.ProductType)]
        public List<Expression<Func<T,object>>> Includes { set; get; }

        // sign for properties for Sort by asc condition [orderBy(p=>p.price)]
        public Expression<Func<T,object>> OrderBy { set; get; }

        //sign for properties for Sort by desc condition [orderByDesc(p=>p.price)]
        public Expression<Func<T, object>> OrderByDesc { set; get; }

        //sign fir skip
        public int Skip { set; get; }

        //sign fir Take
        public int Take { set; get; }

        public bool IsPaginationEnabled { set; get; }
    }
}
