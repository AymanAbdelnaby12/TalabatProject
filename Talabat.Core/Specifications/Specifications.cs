using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    //this class is used to do implementaion of Interface of ISpecifications 
    public class Specifications<T> : ISpecifications<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { set; get; } //automatically implemented property
        public List<Expression<Func<T, object>>> Includes { set; get; }
        public Expression<Func<T, object>> OrderBy { set; get; }
        public Expression<Func<T, object>> OrderByDesc { set; get; }
        public int Skip { set; get; }
        public int Take { set; get; }
        public bool IsPaginationEnabled { set; get; }


        //constructor for GetAllAsync method
        public Specifications()
        {
            Includes= new List<Expression<Func<T, object>>>();
        }
        //constructor for GetByIdAsync method
        public Specifications(Expression<Func<T,bool>> criterisExpression)
        {
            Criteria = criterisExpression;
            Includes = new List<Expression<Func<T, object>>>();
        }
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }
        
        public void ApplyPagination(int skip,int take)
        {
           IsPaginationEnabled=true;
            Skip = skip;
            Take = take;
        }

    }
}
