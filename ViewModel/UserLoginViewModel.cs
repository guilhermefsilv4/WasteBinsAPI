using System.ComponentModel.DataAnnotations;

namespace WasteBinsAPI.ViewModel;

public class UserLoginViewModel
{
    [Required] [MinLength(1)] public string Username { get; set; }
    [Required] [MinLength(1)] public string Password { get; set; }
}