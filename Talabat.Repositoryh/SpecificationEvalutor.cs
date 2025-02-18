using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    // This Method in this class to build Query for Critira and Includes
    public static class SpecificationEvalutor<T> where T : BaseModel 
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery ,ISpecifications<T> Spec)
        {
            var query = inputQuery;
            if(Spec.Criteria is not null)
            {
                query = query.Where(Spec.Criteria);
            }
            if(Spec.OrderBy is not null)
            {
                query = query.OrderBy(Spec.OrderBy);
            }
            if (Spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(Spec.OrderByDesc);
            }
            if (Spec.IsPaginationEnabled)
            {
                query = query.Skip(Spec.Skip).Take(Spec.Take);
            }
            query = Spec.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return query;
        }
    }
}
