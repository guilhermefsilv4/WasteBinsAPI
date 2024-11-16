namespace WasteBinsAPI.ViewModel;

public class WasteBinViewModel
{
    public int Id { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
    public int FillLevel { get; set; }
}