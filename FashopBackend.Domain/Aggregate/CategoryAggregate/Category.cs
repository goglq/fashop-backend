using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Aggregate.CategoryAggregate
{
    [Table("categories")]
    public class Category : IAggregateRoot
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
