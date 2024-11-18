namespace WasteBinsAPI.ViewModel;

public class WasteBinPaginationViewModel
{
    public IEnumerable<WasteBinViewModel> WasteBins { get; set; }
    public int PageSize { get; set; }
    public int Ref { get; set; }
    public int NextRef { get; set; }
    public string PreviousPageUrl => $"/WasteBin?referencia={Ref}&tamanho={PageSize}";
    public string NextPageUrl => (Ref < NextRef) ? $"/WasteBin?referencia={NextRef}&tamanho={PageSize}" : "";
}