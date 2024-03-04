using System.ComponentModel.DataAnnotations;

namespace Cooking.Service.DAL.Models
{
    public class Recipe : TableBase
    {
        public Recipe()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Portions { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}