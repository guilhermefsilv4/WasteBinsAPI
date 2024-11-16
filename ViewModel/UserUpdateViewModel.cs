using Microsoft.Build.Framework;

namespace WasteBinsAPI.ViewModel;

public class UserUpdateViewModel
{
    [Required] public int UserId { get; set; }
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string? Role { get; set; }
}