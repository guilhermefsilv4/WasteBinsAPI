using System.ComponentModel.DataAnnotations;

namespace WasteBinsAPI.ViewModel;

public class WasteBinCreateViewModel
{
    [Required] public string Location { get; set; }
    [Required] public int Capacity { get; set; }

    [Required]
    [Range(0, 100, ErrorMessage = "FillLevel deve estar entre 0 e 100.")]
    public int FillLevel { get; set; }
}