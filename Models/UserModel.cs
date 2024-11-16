using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WasteBinsAPI.Models
{
    [Table("Users")]
    [Index(nameof(Username), IsUnique = true)]
    public class UserModel
    {
        [Key] public int UserId { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string? Role { get; set; }
    }
}