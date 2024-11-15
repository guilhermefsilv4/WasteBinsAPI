using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteBinsAPI.Models;

[Table("WasteBins")]
public class WasteBin
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    [Range(0, 100, ErrorMessage = "FillLevel deve estar entre 0 e 100.")]
    public int FillLevel { get; set; }
}