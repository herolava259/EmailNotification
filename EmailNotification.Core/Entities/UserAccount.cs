
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EStatus = EmailNotification.Core.Enums.UserAccountEnum.EStatus;


namespace EmailNotification.Core.Entities;

public class UserAccount: BaseEntity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public EStatus Status { get; set; }

    [Required]
    public DateTimeOffset LastUpdatePassword { get; set; }

    public Profile? Profile { get; set; } 
}
