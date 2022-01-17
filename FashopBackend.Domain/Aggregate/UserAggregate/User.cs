using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using FashopBackend.Core.Aggregate.RoleAggregate;
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
    
    [Required, Column("token")]
    public string Token { get; set; }
    
    [Required, Column("role_id")]
    public int RoleId { get; set; }
    
    public Role Role { get; set; }
}