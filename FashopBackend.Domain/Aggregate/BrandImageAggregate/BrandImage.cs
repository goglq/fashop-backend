using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.BrandImageAggregate;

[Table("brand_images")]
public class BrandImage : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }
    
    [Column("thumbnail")]
    public string Thumbnail { get; set; }
    
    [Column("header")]
    public string Header { get; set; }

    [Required, Column("brand_id")]
    public int BrandId;
    
    public Brand Brand;
}