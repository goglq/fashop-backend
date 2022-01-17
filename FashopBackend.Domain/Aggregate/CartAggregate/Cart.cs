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
using FashopBackend.Core.Aggregate.OrderAggregate;

namespace FashopBackend.Core.Aggregate.CartAggregate
{
    [Table("carts")]
    public class Cart : IAggregateRoot
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public User User { get; set; }

        [Column("product_id"), Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }
        
        [Column("count")]
        public int Count { get; set; }
        
        [Column("order_id")]
        public int? OrderId { get; set; }
        
        public Order Order { get; set; }
    }
}
