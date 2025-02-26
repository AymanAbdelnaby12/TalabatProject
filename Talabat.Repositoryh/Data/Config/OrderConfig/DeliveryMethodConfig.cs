﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data.Config.OrderConfig
{
    class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
           builder.Property(DM => DM.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
