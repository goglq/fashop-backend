using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.RoleAggregate;

[Table("roles")]
public class Role : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }
    
    [Required, Column("name")]
    public string Name { get; set; }
}