namespace WasteBinsAPI.ViewModel;

public class TokenViewModel
{
    public string Token { get; set; }

    public TokenViewModel(string token)
    {
        Token = token;
    }
}