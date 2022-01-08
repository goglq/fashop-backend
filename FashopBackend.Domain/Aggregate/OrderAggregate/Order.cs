using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.OrderAggregate;

[Table("orders")]
public class Order : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("count")] 
    public int Count { get; set; } = 1;
    
    [Column("address")]
    public string Address { get; set; }
    
    [Column("status")]
    public string Status { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    public User user { get; set; }
    
    [Column("product_id")]
    public int ProductId { get; set; }
    
    public Product Product { get; set; }
}