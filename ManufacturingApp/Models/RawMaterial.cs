using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Models
{
    public class RawMaterial
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
    }
}