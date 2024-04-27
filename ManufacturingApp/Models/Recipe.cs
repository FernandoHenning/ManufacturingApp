using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Models
{
    public class Recipe
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RecipeRawMaterial> RecipeRawMaterials { get; set; }
        public virtual ICollection<RecipeProduct> RecipeProducts { get; set; }
    }
}