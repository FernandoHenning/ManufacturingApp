using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Models
{
    public class Supplier
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<SupplierRawMaterial> SupplierRawMaterials { get; set; }
    }
}