
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.OrderSpec
{
    public class OrderWithPaymentIntentSpec:Specifications<Models.Order_Aggregate.Order>
    {
        public OrderWithPaymentIntentSpec(string paymentIntentId): base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
