namespace WasteBinsAPI.ViewModel;

public class UserPaginationViewModel
{
    public IEnumerable<UserViewModel> Users { get; set; }
    public int PageSize { get; set; }
    public int Ref { get; set; }
    public int NextRef { get; set; }
    public string PreviousPageUrl => $"/User?referencia={Ref}&tamanho={PageSize}";
    public string NextPageUrl => (Ref < NextRef) ? $"/User?referencia={NextRef}&tamanho={PageSize}" : "";
}