using Microsoft.Build.Framework;

namespace WasteBinsAPI.ViewModel;

public class UserLoginViewModel
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
}