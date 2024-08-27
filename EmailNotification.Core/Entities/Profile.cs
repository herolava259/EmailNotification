using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmailNotification.Core.Entities;

public class Profile : BaseEntity
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(256)]
    public string Address { get; set; }

    [ForeignKey("UserAccount")]
    public Guid UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; } = null!;
}
