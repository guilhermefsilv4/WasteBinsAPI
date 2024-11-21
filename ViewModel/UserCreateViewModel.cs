using System.ComponentModel.DataAnnotations;

namespace WasteBinsAPI.ViewModel;

public class UserCreateViewModel
{
    [Required] [MinLength(3)] public string Username { get; set; }

    [Required] [MinLength(5)] public string Password { get; set; }
    [Required] [MinLength(1)] public string? Role { get; set; }
}