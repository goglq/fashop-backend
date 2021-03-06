using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.CommercialAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;
using FashopBackend.Core.Aggregate.OrderedProductAggregate;
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
        
        public DbSet<Cart> Carts { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderedProduct> OrderedProducts { get; set; }

        public DbSet<Commercial> Commercials { get; set; }
        
        public DbSet<CommercialType> CommercialTypes { get; set; }

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

            modelBuilder
                .Entity<OrderedProduct>()
                .HasKey(table => new {table.OrderId, table.ProductId});

            modelBuilder.Entity<Category>()
                .HasData(new Category() {Id = 1, Name = "Одежда"});
            modelBuilder.Entity<Category>()
                .HasData(new Category() {Id = 2, Name = "Техника"});
            modelBuilder.Entity<Category>()
                .HasData(new Category() {Id = 3, Name = "Для кухни"});

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

            modelBuilder
                .Entity<Order>()
                .Property(_ => _.OrderStatusId)
                .HasConversion<int>();
            
            modelBuilder
                .Entity<OrderStatus>()
                .Property(_ => _.Id)
                .HasConversion<int>();

            modelBuilder
                .Entity<OrderStatus>()
                .HasData(
                    Enum.GetValues(typeof(OrderStatusId))
                        .Cast<OrderStatusId>()
                        .Select(e => new OrderStatus()
                        {
                            Id = e,
                            Name = e.ToString()
                        }));
            
            modelBuilder
                .Entity<Commercial>()
                .Property(e => e.CommercialTypeId)
                .HasConversion<int>();
            
            modelBuilder
                .Entity<CommercialType>()
                .Property(e => e.Id)
                .HasConversion<int>();

            modelBuilder
                .Entity<CommercialType>()
                .HasData(
                    Enum.GetValues(typeof(CommercialTypeId))
                        .Cast<CommercialTypeId>()
                        .Select(e => new CommercialType()
                        {
                            Id = e,
                            Name = e.ToString()
                        }));
        }

        public override void Dispose()
        {
            Console.WriteLine("Disposed");
            base.Dispose();
        }
    }
}
