using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.BrandAggregate;

namespace FashopBackend.Core.Aggregate.ProductAggregate
{
    [Table("products")]
    public class Product : IAggregateRoot
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("name")]
        public string Name { get; set; }
        
        [Column("brand_id")]
        public int BrandId { get; set; }
        
        public Brand Brand { get; set; }

        public List<Category> Categories { get; set; } = new();
    }
}
