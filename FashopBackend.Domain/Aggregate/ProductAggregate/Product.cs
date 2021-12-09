using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashopBackend.Core.Aggregate.ProductAggregate
{
    [Table("products")]
    public class Product : IAggregateRoot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Category> Categories { get; set; } = new();
    }
}
