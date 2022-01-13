using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Aggregate.CartAggregate
{
    [Table("cart")]
    public class Cart : IAggregateRoot
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
