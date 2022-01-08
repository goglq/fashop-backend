using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.RoleAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;

namespace FashopBackend.Infrastructure.Data
{
    public class FashopContext : DbContext
    {
        public DbSet<Product> Products { get; set; } 

        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Brand> Brands { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }

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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.IsEmailVerified)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .Property(u => u.Token)
                .IsRequired(false);

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Role>().HasData(new Role() { Id = 1, Name = "admin"});
            modelBuilder.Entity<Role>().HasData(new Role() { Id = 2, Name = "user"});
            //modelBuilder.Entity<Role>().HasData(new Role() {Id = 3, Name = "seller"});
        }

        public override void Dispose()
        {
            Console.WriteLine("Disposed");
            base.Dispose();
        }
    }
}
