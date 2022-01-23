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

    [Column("city"), Required]
    public string City { get; set; }
    
    [Column("street"), Required]
    public string Street { get; set; }
    
    [Column("building"), Required]
    public string Building { get; set; }
    
    [Column("section")]
    public string Section { get; set; }
    
    [Column("housing")]
    public string Housing { get; set; }
    
    [Column("post_index"), Required]
    public string PostIndex { get; set; }
    
    [Column("name"), Required]
    public string Name { get; set; }
    
    [Column("surname"), Required]
    public string Surname { get; set; }
    
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