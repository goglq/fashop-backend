using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.UserAggregate;

[Table("users")]
public class User : IAggregateRoot
{
    [Key, Column("id")]
    public int Id { get; set; }
    
    [Required, Column("email")]
    public string Email { get; set; }
    
    [Required, Column("password")]
    public string Password { get; set; }
    
    [Required, Column("email_verified")]
    public bool IsEmailVerified { get; set; }
}