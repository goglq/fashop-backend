using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashopBackend.Core.Aggregate.CommercialAggregate;

[Table("commercial_types")]
public class CommercialType
{
    [Key, Column("id")]
    public CommercialTypeId Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    public List<Commercial> Commercials { get; set; }
}