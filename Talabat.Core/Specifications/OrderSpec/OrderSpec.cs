using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpec
{
    public class OrderSpec : Specifications<Models.Order_Aggregate.Order>
    {
        // Used to get the orders for user by email
        public OrderSpec(string email):base(o => o.BuyerEmail == email)
        {
            Includes.Add(o=>o.DeliveryMethod);
            Includes.Add(o=>o.Items);
            AddOrderByDesc(o=>o.OrderDate);
        }
        // Used to get the order for user by email and id
        public OrderSpec(string email , int id) : base(o => o.BuyerEmail == email && o.Id==id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }

    }
}
