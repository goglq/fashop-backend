using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.OrderAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.OrderedProductAggregate;

[Table("ordered_products")]
public class OrderedProduct : IAggregateRoot
{
    [Required, Column("product_id")]
    public int ProductId { get; set; }
    
    public Product Product { get; set; }
    
    [Required, Column("order_id")]
    public int OrderId { get; set; }
    
    public Order Order { get; set; }

    [Required, Column("count")] 
    public int Count { get; set; } = 0;
}