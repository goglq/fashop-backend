using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.BrandImageAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.BrandAggregate;

[Table("brands")]
public class Brand : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }
    
    [Required, Column("name")]
    public string Name { get; set; }

    public List<Product> Products { get; set; } = new();
    
    [Column("brand_image_id")]
    public int BrandImageId { get; set; }

    public BrandImage BrandImage { get; set; } = new BrandImage();
}