using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Infrastructure.Data
{
    public class FashopContext : DbContext
    {
        public DbSet<Product> Products { get; set; } 

        public DbSet<Category> Categories { get; set; }


        public FashopContext(DbContextOptions<FashopContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                    .HasMany(c => c.Products)
                    .WithMany(p => p.Categories)
                    .UsingEntity<Dictionary<string, object>>(
                "products_categories",
                j => j.HasOne<Product>().WithMany().HasForeignKey("product_id"),
                j => j.HasOne<Category>().WithMany().HasForeignKey("category_id"));
        }

        public override void Dispose()
        {
            Console.WriteLine("Disposed");
            base.Dispose();
        }
    }
}
