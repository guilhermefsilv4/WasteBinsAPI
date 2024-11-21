using System.ComponentModel.DataAnnotations;

namespace WasteBinsAPI.ViewModel;

public class WasteBinCreateViewModel
{
    [Required] [MinLength(3)] public string Location { get; set; }
    [Range(0, 100)] [Required] public int Capacity { get; set; }

    [Required] [Range(0, 100)] public int FillLevel { get; set; }
}