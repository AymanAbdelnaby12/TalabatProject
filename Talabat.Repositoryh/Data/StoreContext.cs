using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;
using Talabat.Repository.Data.Config;

namespace Talabat.Repository.Data
{
    public class StoreContext : DbContext
    {

        // this is constructor to pass options to base class and call base class constructor
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply configurations by three model configurations ProductConfig, ProductBrandConfig, ProductTypeConfig
            // this is old way to apply configurations
            //modelBuilder.ApplyConfiguration(new ProductConfig());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfig());
            //modelBuilder.ApplyConfiguration(new ProductTypeConfig());

            // apply configurations by assembly new way
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Core.Models.Order_Aggregate.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> deliveryMethods { get; set; }

    }
}
