using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;
using FashopBackend.Core.Aggregate.OrderedProductAggregate;
using FashopBackend.Core.Aggregate.ProductImageAggregate;

namespace FashopBackend.Core.Aggregate.ProductAggregate
{
    [Table("products")]
    public class Product : IAggregateRoot
    {
        [Key, Column("id")] public int Id { get; set; }

        [Required, Column("name")] public string Name { get; set; }

        [Required, Column("description")] public string Description { get; set; }

        [Required, Column("price", TypeName = "money")]
        public decimal Price { get; set; }

        [Required, Column("sale", TypeName = "decimal"), Range(0, 100)]
        public int Sale { get; set; } = 0;

        [Column("brand_id")] public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public List<Category> Categories { get; set; } = new();

        public List<ProductImage> ProductImages { get; set; } = new();

        public List<OrderedProduct> OrderedProducts { get; set; }
    }
}
