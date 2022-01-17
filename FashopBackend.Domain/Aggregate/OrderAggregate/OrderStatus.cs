using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashopBackend.Core.Aggregate.OrderAggregate;

[Table("order_statuses")]
public class OrderStatus
{
    [Key, Column("id")]
    public OrderStatusId Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
}