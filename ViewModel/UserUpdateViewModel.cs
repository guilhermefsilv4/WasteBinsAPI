using System.ComponentModel.DataAnnotations;

namespace WasteBinsAPI.ViewModel;

public class UserUpdateViewModel
{
    [Required] public int UserId { get; set; }
    [Required] [MinLength(1)] public string Username { get; set; }
    [Required] [MinLength(5)] public string Password { get; set; }
    [Required] [MinLength(1)] public string? Role { get; set; }
}