using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data.Config.OrderConfig
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O=>O.Status)
                .HasConversion( OS => OS.ToString(), Os => (OrderStatus)Enum.Parse(typeof(OrderStatus), Os) );
            builder.Property(O=>O.Subtotal)
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(O => O.ShippingAddress, A => A.WithOwner());
            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
