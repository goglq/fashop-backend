using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.ProductImageAggregate;

[Table("product_images")]
public class ProductImage : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }
    
    [Required, Column("url")]
    public string Url { get; set; }
    
    [Required, Column("product_id")]
    public int ProductId { get; set; }
    
    public Product Product { get; set; }
}