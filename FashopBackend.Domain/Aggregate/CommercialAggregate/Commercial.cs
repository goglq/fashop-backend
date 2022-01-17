using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.CommercialAggregate;

[Table("commercials")]
public class Commercial : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }
    
    [Column("url")]
    public string Url { get; set; }
    
    [Column("commercial_type_id")]
    public CommercialTypeId CommercialTypeId { get; set; }
    
    public CommercialType CommercialType { get; set; }
}