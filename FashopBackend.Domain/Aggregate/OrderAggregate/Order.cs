using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.OrderedProductAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.OrderAggregate;

[Table("orders")]
public class Order : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("address")]
    public string Address { get; set; }
    
    [Column("total_price", TypeName = "money")]
    public decimal TotalPrice { get; set; }
    
    [Column("status_id")]
    public OrderStatusId OrderStatusId { get; set; }
    
    public OrderStatus Status { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    public List<OrderedProduct> OrderedProducts { get; set; }
}